using Microsoft.EntityFrameworkCore;
using Todo.Application.Modules;
using Todo.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký DbContext (Chỉ được xuất hiện 1 lần)
builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cnnStr"));
});

builder.Services.AddControllers();
builder.Services.AllApplicationMudules();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// 3. Khởi tạo ứng dụng (DÒNG NÀY CHỈ ĐƯỢC CÓ 1 TRONG CẢ FILE)
var app = builder.Build();

// 4. Cấu hình Pipeline
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();