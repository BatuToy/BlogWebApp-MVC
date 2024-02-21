using System.Net;
using AppBlog.Data.Abstract;
using AppBlog.Entity;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;

namespace AppBlog.Data.Concrete.EfCore;

public class EfPostRepository : IPostRepository
{
    private readonly DataContext _context;

    public EfPostRepository(DataContext context)
    {
        _context = context;
    }

    public IQueryable<Post> Posts => _context.Posts;

    public async Task<Post> CreatePost([FromBody]Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return(post);
    }

    public async Task<Post> GetById(string url)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Url == url);
        return(post);
    }

    public async Task<Post> GetById(int id)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
        return(post);
    }
}
    
