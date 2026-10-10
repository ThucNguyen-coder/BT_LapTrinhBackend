using LHU_124000698_NguyenHuynhDuyThuc.Api;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
using LHU_124000698_NguyenHuynhDuyThuc.Api.IServices;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseSqlServer(connectionString);
    }
);

builder.Services.AddScoped<IMonHocService, MonHocService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
