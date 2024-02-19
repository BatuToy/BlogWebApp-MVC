using AppBlog.Data.Abstract;
using AppBlog.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppBlog.Controllers;

public class UsersController : Controller
{  
   private readonly IUserRepository _userRepository;     
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.Users.ToListAsync();
        return Ok(users);  
    }
}
