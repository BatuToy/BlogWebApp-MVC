using AppBlog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AppBlog;

public interface ITagRepository
{
    IQueryable<Tag> Tags{get;}

    Task<Tag> CreateTag([FromBody] Tag tag);
}
