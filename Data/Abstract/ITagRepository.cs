using AppBlog.Entity;

namespace AppBlog;

public interface ITagRepository
{
    IQueryable<Tag> Tags{get;}

    void CreateTag(Tag tag);
}
