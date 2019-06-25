using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace login.Controllers
{
  public class HomeController : Controller
  {
    private loginContext dbContext;
    public HomeController(loginContext context)
    {
      dbContext = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("success")]
    public IActionResult Success()
    {
      if(HttpContext.Session.GetInt32("UserId") != null)
      {
        return View("success");
      }
      else
      {
      return RedirectToAction("Index");
      }
    }

    [HttpPost("register")]
    public IActionResult Register(Both newUser)
    {
      User user = newUser.user;
      if(ModelState.IsValid)
      {
        if(dbContext.Users.Any(u => u.Email == user.Email))
        {
          ModelState.AddModelError("Email", "Email is alerady in use!");
          return View("Index");
        }
        else
        {
          PasswordHasher<User> Hasher = new PasswordHasher<User>();
          user.Password = Hasher.HashPassword(newUser.user, user.Password);
          dbContext.Add(newUser.user);
          dbContext.SaveChanges();
          HttpContext.Session.SetInt32("UserId", user.UserId);
          return RedirectToAction("success");
        }
      }
      else
      {
        return View("Index");
      }
    }

    [HttpPost("login")]
    public IActionResult Login(Both userSubmission)
    {
      LoginUser login = userSubmission.login;
      if(ModelState.IsValid)
      {
        var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == login.Email);

        if(userInDb == null)
        {
          ModelState.AddModelError("Email", "Invalid Email/Password");
          return View("Index");
        }

        var hasher = new PasswordHasher<LoginUser>();
        var result = hasher.VerifyHashedPassword(userSubmission.login, userInDb.Password, login.Password);

        if(result == 0)
        {
          ModelState.AddModelError("Email", "Invalid Email/Password");
          return View("Index");
        }
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        return RedirectToAction("success");
      }
      else
      {
        return View("Index");
      }
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index");
    }


  }
}
