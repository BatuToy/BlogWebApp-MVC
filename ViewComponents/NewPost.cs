using AppBlog.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AppBlog.ViewComponents;

public class NewPost : ViewComponent
{
    private IPostRepository _postRepository;

    public NewPost(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _postRepository
                                .Posts
                                .OrderByDescending(p => p.PublishedOn)
                                .Take(5)
                                .ToListAsync()
            );
    }
}