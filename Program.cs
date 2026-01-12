using MeetingTaskManagement.DataLayer;
using MeetingTaskManagement.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using MeetingTaskManagement.DataLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var options = new DbContextOptionsBuilder<InMemoryDbContext>()
                      .UseInMemoryDatabase(databaseName: "MeetingTaskDb")
                      .Options;
var context = new InMemoryDbContext(options);
builder.Services.AddSingleton<InMemoryDbContext>(context);
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Meeting & Task Management API", 
        Version = "v1",
        Description = "A simple example of ASP.NET Core Web API"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
