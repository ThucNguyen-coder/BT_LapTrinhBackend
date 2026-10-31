using AspNetCoreRateLimit;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
using LHU_124000698_NguyenHuynhDuyThuc.Api.IServices;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Middlewares;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration()
  .MinimumLevel.Information()
  .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
  .Enrich.FromLogContext()
  .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}")
  .WriteTo.File(
    path: "logs/lhu-academic-.log",
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 30,
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}"
  )
  .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hệ thống Quản lý Học vụ API – LHU 24CT111",
        Version = "v1",
        Description = "API Quản lý học vụ tích hợp xác thực JWT và phân quyền theo vai trò RBAC"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Dán trực tiếp Access Token (chuỗi JWT eyJ...):",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

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

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(connectionString);
}
);

builder.Services.AddScoped<IMonHocService, MonHocService>();

// Đăng ký dịch vụ cấp phát token
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true, // Từ chối nếu Token đã hết hạn
        ValidateIssuerSigningKey = true, // Kiểm tra chữ ký bằng SecretKey
        IssuerSigningKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
    };
});

var allowedOrigins = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>()
?? new[] { "http://localhost:3000", "http://localhost:5173" };
builder.Services.AddCors(options =>
{
    options.AddPolicy("LhuFrontendPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
        .WithHeaders("Content-Type", "Authorization", "X-Correlation-ID")
        .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//builder.Services.AddProblemDetails();

builder.Services.AddScoped<SinhVienService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CorrelationIdMiddleware>();

// 2. Ghi nhận HTTP Request với Serilog
app.UseSerilogRequestLogging();
// 3. Xử lý ngoại lệ
//app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Học Vụ API v1"));

// 5. Kích hoạt CORS (đặt trước Authentication để xử lý OPTIONS)
app.UseCors("LhuFrontendPolicy");


// THỨ TỰ SỐNG CÒN TRONG PIPELINE:
//app.UseRateLimiter();
app.UseIpRateLimiting();

app.UseAuthentication(); // 1. Xác thực: Bạn là ai? (401 Unauthorized)

app.UseAuthorization(); // 2. Phân quyền: Bạn được làm gì? (403 Forbidden)

app.MapControllers();

app.Run();