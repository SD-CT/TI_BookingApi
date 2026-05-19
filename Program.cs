using TI_BookingApi.Application.Interfaces;
using TI_BookingApi.Application.Services;
using TI_BookingApi.Domain.Interfaces;
using TI_BookingApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IActivityRepository, InMemoryActivityRepository>();
builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();

builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<StatisticsService>();
builder.Services.AddScoped<ReportingService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TI Booking API - Test Technique",
        Version = "v1",
        Description = "API de réservation d'activités pour test technique Senior Developer",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Test Technique",
            Email = "test@example.com"
        }
    });
    
    // Ajouter des exemples et descriptions pour améliorer la documentation
    c.EnableAnnotations();
    
    // Inclure les commentaires XML si disponibles
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TI Booking API v1");
        c.RoutePrefix = "swagger"; // Swagger sera accessible à /swagger
        c.DocumentTitle = "TI Booking API - Test Technique";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.DisplayRequestDuration();
        c.EnableTryItOutByDefault();
    });
}
else
{
    // En production, on peut aussi exposer Swagger si nécessaire
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();