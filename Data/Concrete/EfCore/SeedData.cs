using AppBlog.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppBlog.Data.Concrete.EfCore;

public static class SeedData
{
    public static void GetTestData(IApplicationBuilder app) 
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DataContext>();

        if(context != null)
        {
            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if(!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Text = "Web Programlama"},
                    new Tag { Text = "BackEnd"},
                    new Tag { Text = "FrontEnd"},
                    new Tag { Text = "Php"},
                    new Tag { Text = "FullStack"}
                );
                context.SaveChanges();
            }
            if(!context.Users.Any())
            {
                context.Users.AddRange(
                    new User {UserName = "Akin Özcift"},
                    new User {UserName = "Yusuf Ozcevik"},
                    new User {UserName = "Muge Ozcevik"}
                );
                context.SaveChanges();
            }
            if(!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post {
                        Title = ".NET 8 Onion Architecture",
                        Content= "lorem ipsum",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId = 2,
                        Image = "1.jpg"
                    },
                    new Post {
                        Title = ".NET 9 Onion Architecture",
                        Content= "lorem ipsum",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(3).ToList(),
                        UserId = 1,
                        Image = "2.jpg"
                    },
                    new Post {
                        Title = ".NET 10 Onion Architecture",
                        Content= "lorem ipsum",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-15),
                        Tags = context.Tags.Take(2).ToList(),
                        UserId = 3,
                        Image = "3.jpg"
                    },
                    new Post {
                        Title = ".NET 11 Onion Architecture",
                        Content= "lorem ipsum",
                        IsActive = false,
                        PublishedOn = DateTime.Now.AddDays(-17),
                        Tags = context.Tags.Take(5).ToList(),
                        UserId = 1,
                        Image = ".jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
