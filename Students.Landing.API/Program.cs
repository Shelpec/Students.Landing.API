using Microsoft.EntityFrameworkCore;
using Students.Landing.Core.Data;
using Students.Landing.Core.Interfaces;
using Students.Landing.Core.Services;
using Students.Landing.Infrastructure.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);


// ������������ DbContext � ����� ������� �����������
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ����������� ������������
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentApplicationRepository, StudentApplicationRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();

// ����������� ��������
builder.Services.AddScoped<ICompanyDirectionService, CompanyDirectionService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<ISpecializationDirectionService, SpecializationDirectionService>();
builder.Services.AddScoped<IStudentApplicationService, StudentApplicationService>();
builder.Services.AddScoped<IMajorService, MajorService>();
builder.Services.AddScoped<IUniversityMajorService, UniversityMajorService>();

// ����������� ������������ � ����������� JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true; // ��� ��������� ��������������
    });

// ����������� � Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentsBook API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
