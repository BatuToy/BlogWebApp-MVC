using System.IO.Compression;
using System.Net;
using AppBlog.Data.Abstract;
using AppBlog.Entity;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.IdentityModel.Tokens;

namespace AppBlog.Data.Concrete.EfCore;

public class EfPostRepository : IPostRepository
{
    private readonly DataContext _context;

    public EfPostRepository(DataContext context)
    {
        _context = context;
    }

    public DbSet<Post> posts;
    public IQueryable<Post> Posts => _context.Posts;

    public async Task<Post> CreatePost([FromBody]Post post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return(post);
    }

    public async Task<Post> GetById(string url)
    {
        var post = await _context.Posts
        .Include(x => x.Tags)
        .FirstOrDefaultAsync(p => p.Url == url);
        return(post);
    }

    
    public async Task<Post> GetById(int id)
    {
        var post = await _context.Posts
        .Include(p => p.Tags)
        .FirstOrDefaultAsync(p => p.PostId == id);
        return(post);
    }

    public void UpdatePost([FromBody] Post post , int [] tagIds) 
    {
        var entity = _context.Posts.Include( x => x.Tags).FirstOrDefault( p => p.PostId == post.PostId);

        if(entity != null)
        {
            entity.Title = post.Title;
            entity.Content = post.Content;
            entity.Description = post.Description;
            entity.Url = post.Url;
            entity.IsActive = post.IsActive;

            entity.Tags = _context.Tags.Where( tag => tagIds.Contains(tag.TagId)).ToList();

          _context.SaveChangesAsync();
        } 
        
    }

    
}
    
