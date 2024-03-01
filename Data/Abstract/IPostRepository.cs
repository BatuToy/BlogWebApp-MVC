using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog.Data.Abstract;

public interface IPostRepository 
{
    IQueryable<Post> Posts { get; }
    Task<Post> CreatePost(Post post);
    void UpdatePost(Post post , int [] tagIds);
    void GetById(string url);
    void GetById(int id);

    
}
