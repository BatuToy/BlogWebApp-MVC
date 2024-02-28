using System.Security.Claims;
using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using AppBlog.Entity;
using AppBlog.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppBlog.Controllers;

    public class UsersController : Controller
    {  
    private readonly IUserRepository _userRepository;     
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    

    
        public IActionResult Login() 
        {
            if(User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index","Posts");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        

        public IActionResult Profile(string username)
        {
            if(username.IsNullOrEmpty())
            {
                return NotFound();
            }
            var user = _userRepository
                                      .Users
                                      .Include( x => x.Posts)
                                      .Include( x => x.Comments)
                                      .ThenInclude( x => x.Post)
                                      .FirstOrDefault( x => x.UserName == username);  
            return View(user);
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);
                if(user == null)
                {
                    await _userRepository.CreateUser(new User {
                        UserName = model.UserName ,
                        Name = model.Name ,
                        Email = model.Email ,
                        Password = model.Password ,
                        Image = "avatar.jpg",
                    });   

                    return RedirectToAction("login" ,"users");
                }
                else
                {
                    ModelState.AddModelError("","UserName ya da Email kullanımda .");
                }                
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login" , "Users");

        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if(ModelState.IsValid)  
            {
                var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if(isUser != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData , isUser.Image ?? ""));

                    if(isUser.Email == "info@sadikturan.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    } 

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties 
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), 
                        authProperties);

                    return RedirectToAction("Index","Posts");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                }
            } 
            
            return View(model);
        }
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.Users.ToListAsync();
            return Ok(users);  
        }
    }
