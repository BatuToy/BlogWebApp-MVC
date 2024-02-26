using System.Net;
using System.Security.Claims;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using AppBlog.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBlog.Controllers;

public class PostsController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
  
    public PostsController(IPostRepository postRepository , ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }


    public async Task<IActionResult> Index(string tag){
        var claims = User.Claims;
        var posts =  _postRepository.Posts;
        
        if(!string.IsNullOrEmpty(tag))
        {
            posts = posts.Where(p => p.Tags.Any(t => t.Url == tag));
        }

        return View( new PostViewModel{
            Posts = await posts.ToListAsync()
    });
    }
    [HttpPost]    
    public JsonResult AddComment(int PostId  , string Text)
    {
        var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var UserName = User.FindFirstValue(ClaimTypes.Name);
        var Image = User.FindFirstValue(ClaimTypes.UserData);

        var comment = new Comment {
            PostId = PostId,
            Text = Text,
            PublishedOn = DateTime.Now,
            UserId = int.Parse(UserId ?? "")
        };

        _commentRepository.CreateComment(comment);
        
        //return Redirect("/posts/details/" + Url);
        //return RedirectToRoute("post_details" , new {url = Url});
        
        return Json ( new {
            UserName,
            Text ,
            comment.PublishedOn ,
            Image 
        });
    }






     public async Task<IActionResult> Details(string url)
        {
            return View(await _postRepository
                        .Posts
                        .Include(x => x.Tags)
                        .Include(x => x.Comments)
                        .ThenInclude(p => p.User)
                        .FirstOrDefaultAsync(p => p.Url == url));
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
