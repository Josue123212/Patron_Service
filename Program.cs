using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;
using LAB05_JosueChoquepuma.Repositories.Implements;
using LAB05_JosueChoquepuma.Services;
using LAB05_JosueChoquepuma.Services.Implements;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger básico (sin complicaciones)
builder.Services.AddSwaggerGen();

// 1. DbContext con PostgreSQL (MatriculasDB)
builder.Services.AddDbContext<MatriculasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. UnitOfWork Dinámico
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registro de Servicios de Entidades
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IAsistenciaService, AsistenciaService>();
builder.Services.AddScoped<IEvaluacionService, EvaluacionService>();

// AGREGADO: Registro del Servicio de Autenticación
builder.Services.AddScoped<IAuthService, AuthService>();

// AGREGADO: Configuración del Middleware de Autenticación JWT
var secretKey = builder.Configuration["JwtSettings:SecretKey"] 
    ?? "SqwHEWmdAJOqxG3cp7xgetBHLRoWskIJPBd8lb1C77J36vGvMCBpYTvSzwJ74R1SY3VbaUY";
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LAB05_JosueChoquepuma V1");
        c.RoutePrefix = string.Empty; // Carga Swagger directo al iniciar
    });
}

app.UseHttpsRedirection();

// AGREGADO: Middlewares de seguridad (orden obligatorio)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();