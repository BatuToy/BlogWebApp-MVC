using System.Net;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using AppBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBlog.Controllers;

public class PostsController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
  
    public PostsController(IPostRepository postRepository,ITagRepository tagRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
    }


    public IActionResult Index(){
        return View(
            new PostViewModel{
                Posts = _postRepository.Posts.ToList(),
                Tags = _tagRepository.Tags.ToList()
            }
        );
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postRepository.Posts.ToListAsync();
        return Ok(posts);
    }
    
    

}
