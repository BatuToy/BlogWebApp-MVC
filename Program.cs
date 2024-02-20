using AppBlog;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>( options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IPostRepository , EfPostRepository>();
builder.Services.AddScoped<IUserRepository , EfUserRepository>();
builder.Services.AddScoped<ITagRepository ,  EfTagRepository>();

var app = builder.Build();

SeedData.GetTestData(app);

app.UseStaticFiles();

app.MapDefaultControllerRoute(); 

app.Run();
    