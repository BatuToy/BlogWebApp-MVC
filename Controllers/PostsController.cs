using System.Net;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
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


    public IActionResult Index(){
        return View(_postRepository.Posts.ToList());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postRepository.Posts.ToListAsync();
        return Ok(posts);
    }
    
    

}
