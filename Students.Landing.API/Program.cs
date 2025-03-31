using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Students.Landing.Core.Data;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Services;
using Students.Landing.Infrastructure.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1) Подключение к PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Регистрация репозиториев
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentApplicationRepository, StudentApplicationRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();

// 3) Регистрация сервисов
builder.Services.AddScoped<ICompanyDirectionService, CompanyDirectionService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<ISpecializationDirectionService, SpecializationDirectionService>();
builder.Services.AddScoped<IStudentApplicationService, StudentApplicationService>();
builder.Services.AddScoped<IMajorService, MajorService>();
builder.Services.AddScoped<IUniversityMajorService, UniversityMajorService>();

// 4) Добавляем контроллеры + настройки JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// 5) Настройка Keycloak
var keycloakSettings = builder.Configuration.GetSection("Keycloak");
string realm = keycloakSettings["Realm"] ?? "Google-Auth";
string authority = $"https://kc.umto.kz/realms/{realm}";
string audience = keycloakSettings["ClientId"] ?? "students-admin-api";

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://kc.umto.kz/realms/{realm}";
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authority,
            ValidateAudience = false, // <=== ЭТО ОТКЛЮЧИ
            ValidateLifetime = true,
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role
        };
    });


// 7) Swagger с JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentsBook API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введите токен в формате: Bearer {ВАШ_ТОКЕН}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// 8) HttpClientFactory
builder.Services.AddHttpClient();

// === NEW: CORS =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


// ===================================

// Собираем приложение
var app = builder.Build();

// Swagger (только в DEV)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentsBook API v1");
        c.RoutePrefix = string.Empty;
    });
}

// Middleware
app.UseHttpsRedirection();

// Подключаем CORS
app.UseCors("AllowAngular");

// ВАЖНО: порядок Auth -> Controllers
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
