using System.Net;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBlog.Controllers;

public class PostsController : Controller
{
    private readonly IPostRepository _postRepository;
  
    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }


    public async Task<IActionResult> Index(string tag){
        
        var posts =  _postRepository.Posts;
        
        if(!string.IsNullOrEmpty(tag))
        {
            posts = posts.Where(p => p.Tags.Any(t => t.Url == tag));
        }

        return View(await posts.ToListAsync());
    }
    

    public async Task<IActionResult> Details(string url)
    {
        return View(await _postRepository.GetById(url));
    }

    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _postRepository.GetById(id);
        return Ok(post);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postRepository.Posts.ToListAsync();
        return Ok(posts);
    }

   
    
    

}
