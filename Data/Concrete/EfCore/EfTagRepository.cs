using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;

namespace AppBlog;

public class EfTagRepository : ITagRepository
{
    private readonly DataContext _context;
    public EfTagRepository(DataContext context)
    {
        _context = context;
    }


    public IQueryable<Tag> Tags => _context.Tags;

    public void CreateTag(Tag tag)
    {
        _context.Add(tag);
        _context.SaveChanges();
    }

    
}
