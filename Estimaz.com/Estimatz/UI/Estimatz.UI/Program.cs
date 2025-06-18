using Estimatz.API.Commands.Mapping;
using Estimatz.API.Queries.GetAllRooms;
using Estimatz.Cache.TokenCache;
using Estimatz.Cache.UserCache;
using Estimatz.Commands.Room.SaveRoom;
using Estimatz.CosmosDB.CosmosDB;
using Estimatz.Data.RoomRepository;
using Estimatz.Data.StoryRepository;
using Estimatz.Data.UserRepository;
using Estimatz.Entities.AppConfig;
using Estimatz.Entities.User;
using Estimatz.Events.NewUserConfirmationEmail;
using Estimatz.Infra.Services.EmailService;
using Estimatz.Logger;
using Estimatz.Notifications;
using Estimatz.Resources;
using Estimatz.Services.Token;
using Estimatz.UI.DbContext;
using Estimatz.UI.Hubs;
using Estimatz.UI.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog.Events;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
Language.SetCulture("pt-br"); //Português do Brasil como padrão

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()
	.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
	.Enrich.FromLogContext()
	.WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Minute)
	.CreateLogger();

builder.Logging.ClearProviders(); // Remova os provedores de log padrão
builder.Logging.AddSerilog(); // Adicione o Serilog como provedor de log

builder.Services.AddOptions();
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

builder.Services.AddAutoMapper(typeof(RoomMapping));
builder.Services.AddAutoMapper(typeof(AccountMappingProfile));
builder.Services.AddAutoMapper(typeof(RoomMappingProfile));
builder.Services.AddAutoMapper(typeof(DashboardMappingProfile));

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddScoped<INotificator, NotificationsService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserCache, UserCache>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IEmailService, MailKitEmailService>();
builder.Services.AddSingleton<ITokenMemoryCache, TokenMemoryCache>();
builder.Services.AddScoped<INotificator, NotificationsService>();
builder.Services.AddHostedService<TokenCacheCleaner>();
builder.Services.AddSingleton<ICosmosDBClient, CosmosDBClient>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IStoryRepository, StoryRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaveRoomCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSimpleRoomsQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(NewUserConfirmationEmailEventHandler).Assembly));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.Build();

var connectionString = config.GetConnectionString("IdentityConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

var cosmosConfig = config.GetSection("CosmosConfig");
builder.Services.Configure<CosmosConfig>(cosmosConfig);

var appSettingsSections = config.GetSection("Token");
builder.Services.Configure<TokenConfiguration>(appSettingsSections);

var tokenSettings = appSettingsSections.Get<TokenConfiguration>();
var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
	options.Cookie.Name = "accessToken"; // Nome do cookie a ser criado
	options.Cookie.HttpOnly = true; // Acessível apenas via HTTP
	options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tempo de expiração do cookie
})
.AddJwtBearer(x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidAudience = tokenSettings.Validate,
		ValidIssuer = tokenSettings.Emissor
	};
});




builder.Services.Configure<CookiePolicyOptions>(options =>
{
	options.CheckConsentNeeded = context => true; // Define se é necessário obter consentimento do usuário para o uso de cookies
	options.MinimumSameSitePolicy = SameSiteMode.Strict; // Define a política de mesmo site (SameSite) como estrita

	// Configuração dos atributos de segurança do cookie
	options.HttpOnly = HttpOnlyPolicy.Always;
	options.Secure = CookieSecurePolicy.Always;
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
	
	app.UseExceptionHandler("/Error");    
    app.UseHsts();
}

app.UseSession();
app.UseWebSockets();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Base}/{action=Index}/{id?}");
});

app.UseEndpoints(routes =>
{
	routes.MapHub<PlanningHub>("/planning");
	routes.MapControllers();
});

app.MapRazorPages();
app.Run();
