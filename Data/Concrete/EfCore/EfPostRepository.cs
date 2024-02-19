using System.Net;
using AppBlog.Data.Abstract;
using AppBlog.Entity;
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

    public void CreatePost(Post post)
    {
        throw new NotImplementedException();
    }
}
