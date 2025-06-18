using Estimatz.API.Commands.Mapping;
using Estimatz.API.Commands.SaveRoom;
using Estimatz.API.CosmosDB.CosmosDB;
using Estimatz.API.Data.RoomRepository;
using Estimatz.API.Data.StoryRepository;
using Estimatz.API.Data.UserRepository;
using Estimatz.API.Data.UserRepository.Cache;
using Estimatz.API.Entities.Settings;
using Estimatz.API.Hubs;
using Estimatz.API.Notifications;
using Estimatz.API.Queries.GetAllRooms;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "planning",
        builder =>
        {            
            builder.WithOrigins("https://localhost:7098") 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(RoomMapping));
builder.Services.AddScoped<INotificator, NotificationsService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserCache, UserCache>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaveRoomCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetSimpleRoomsQueryHandler).Assembly));


var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var cosmosConfig = config.GetSection("CosmosConfig");
builder.Services.Configure<CosmosConfig>(cosmosConfig);
builder.Services.AddSingleton<ICosmosDBClient, CosmosDBClient>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IStoryRepository, StoryRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estimatz.Api");
    });
// }
app.UseWebSockets();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("planning");

app.UseEndpoints(routes =>
{
    routes.MapHub<PlanningHub>("/planning");
    routes.MapControllers();
});

app.Run();
