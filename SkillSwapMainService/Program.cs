using JsonPatchSample;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SkillSwapMainService.Interfaces;
using SkillSwapMainService.Middleware;
using SkillSwapMainService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure DbContext 
builder.Services.AddDbContext<SkillSwapDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // Add JWT Bearer authentication information for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Include the JWT token in the Swagger UI
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

builder.Services.AddSwaggerGen();

// Register TokenValidationService
builder.Services.AddHttpClient<ITokenValidationService, TokenValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Use the TokenValidationMiddleware
app.UseMiddleware<TokenValidationMiddleware>();

app.MapControllers();

app.Run();

