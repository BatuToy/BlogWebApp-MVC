using System.Net;
using System.Security.Claims;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using AppBlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

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
        var posts =  _postRepository.Posts.Where( i => i.IsActive == true);
        
        if(!string.IsNullOrEmpty(tag))
        {
            posts = posts.Where(p => p.Tags.Any(t => t.Url == tag));
        }

        return View( new PostViewModel{
            Posts = await posts.ToListAsync()
    });
    }

    [Authorize]
    public IActionResult AddPost()
    {
        // if(!User.Identity!.IsAuthenticated)
        // {
        //     return RedirectToAction("login","users");
        // }
        return View();
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
    [Authorize]
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddPost(AddPostViewModel model)
    {
        if(ModelState.IsValid)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _postRepository.CreatePost(
                new Post {
                    Title = model.Title ,
                    Content = model.Content ,
                    Description = model.Description ,
                    UserId = int.Parse(UserId ?? "") ,
                    Image = "1.jpg" ,
                    Url = model.Url ,
                    PublishedOn = DateTime.UtcNow ,
                    IsActive = false
                }   
            );

            return RedirectToAction("Index");
        }
        return View(model);
    }

    [Authorize]
    public ActionResult EditPost(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var post = _postRepository.Posts.FirstOrDefault( p => p.PostId == id);
        if(post is null)
        {
            return NotFound();
        }
        return View( new AddPostViewModel {
            PostId = post.PostId ,
            Title = post.Title ,
            Content = post.Content ,
            Description = post.Description ,
            Url = post.Url ,
            isActive = post.IsActive
        });
    }

    [Authorize]
    [HttpPost]
    public IActionResult EditPost(AddPostViewModel model)
    {
        if (ModelState.IsValid)
        {
            var UdpateEntity = new Post
            {
                PostId = model.PostId,
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                Url = model.Url,
                IsActive = model.isActive
            };

            _postRepository.UpdatePost(UdpateEntity);
            return RedirectToAction("List");
        }

        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> List()
    {
        var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
        var Role = User.FindFirstValue(ClaimTypes.Role);

        var post =  _postRepository.Posts;

        if(string.IsNullOrEmpty(Role))
        {
            post = post.Where(p => p.UserId == UserId);
        }

        return View(await post.ToListAsync());
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
