using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog;

public class EfTagRepository : ITagRepository
{
    private readonly DataContext _context;
    public EfTagRepository(DataContext context)
    {
        _context = context;
    }

    public IQueryable<Tag> Tags => _context.Tags;

    public async Task<Tag> CreateTag([FromBody] Tag tag)
    {
        await _context.AddAsync(tag);
        await _context.SaveChangesAsync();
        return(tag);
    }
}
