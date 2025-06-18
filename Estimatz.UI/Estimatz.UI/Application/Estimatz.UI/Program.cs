using Estimatz.UI.Commands.SignIn;
using Estimatz.UI.Entities.AppConfig;
using Estimatz.UI.ExternalServices.EstimatzLoginApi;
using Estimatz.UI.Mapping;
using Estimatz.UI.Queries.Login;
using Estimatz.UI.Resources;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Estimatz.UI.ExternalServices.EstimatzApi;

var builder = WebApplication.CreateBuilder(args);
Language.SetCulture("pt-br"); //Portugu�s do Brasil como padr�o

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(AccountMappingProfile));
builder.Services.AddAutoMapper(typeof(RoomMappingProfile));
builder.Services.AddAutoMapper(typeof(DashboardMappingProfile));
builder.Services.AddAutoMapper(typeof(Estimatz.UI.Commands.Mapping.MappingProfile));
builder.Services.AddAutoMapper(typeof(Estimatz.UI.Queries.Mapping.MappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SignInCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginQueryHandler).Assembly));
builder.Services.AddScoped<ILoginApiClient, LoginApiClient>();
builder.Services.AddScoped<IEstimatzApiClient, EstimatzApiClient>();

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddHttpClient("LoginApi", httpClient =>
{
    var apiConfig = config.GetSection("ExternalServices").GetSection("Estimatz.Login.API").Get<ExternalServicesConfig>();

    httpClient.BaseAddress = new Uri(apiConfig.Url);
    httpClient.Timeout = TimeSpan.FromSeconds(apiConfig.TimeOut);
});

builder.Services.AddHttpClient("EstimatzApi", httpClient =>
{
    var apiConfig = config.GetSection("ExternalServices").GetSection("Estimatz.API").Get<ExternalServicesConfig>();

    httpClient.BaseAddress = new Uri(apiConfig.Url);
    httpClient.Timeout = TimeSpan.FromSeconds(apiConfig.TimeOut);
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true; // Define se � necess�rio obter consentimento do usu�rio para o uso de cookies
    options.MinimumSameSitePolicy = SameSiteMode.Strict; // Define a pol�tica de mesmo site (SameSite) como estrita

    // Configura��o dos atributos de seguran�a do cookie
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});

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
    options.Cookie.HttpOnly = true; // Acess�vel apenas via HTTP
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tempo de expira��o do cookie
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Base}/{action=Index}/{id?}");
});

app.MapRazorPages();
app.Run();