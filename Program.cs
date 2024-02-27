using AppBlog;
using AppBlog.Controllers;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete;
using AppBlog.Data.Concrete.EfCore;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>( options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IPostRepository , EfPostRepository>();
builder.Services.AddScoped<IUserRepository , EfUserRepository>();
builder.Services.AddScoped<ITagRepository ,  EfTagRepository>();
builder.Services.AddScoped<ICommentRepository , EfCommentRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie( options =>
    options.LoginPath = "/users/login"
);

var app = builder.Build();

SeedData.GetTestData(app);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name : "post_by_tag",
    pattern : "posts/tag/{tag}",
    defaults : new {controller = "Posts" , action = "Index"}
);

app.MapControllerRoute(
    name : "post_details",
    pattern : "posts/details/{url}",
    defaults : new {controller = "Posts" , action = "Details"}
);

app.MapControllerRoute(
    name : "default",
    pattern : "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
    