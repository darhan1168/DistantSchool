using System.Security.Claims;
using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User();
            model.MapTo(user);
            user.PasswordHash = model.Password;

            var addingResult = await _userService.Register(user);

            if (!addingResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = addingResult.Message;
                
                return View(model);
            }
            
            await Authenticate(model.Username);
            
            return RedirectToAction("Login", "Account");
        }
        
        TempData["ErrorMessage"] = "Model is not valid";
                
        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var loginResult = await _userService.Login(model.Username, model.Password);
            
            if (!loginResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = loginResult.Message;
                
                return View(model);
            }

            var user = await _userService.GetUserByUsername(model.Username);
            
            if (user == null)
            {
                TempData["ErrorMessage"] = $"{nameof(user)} not found";
                
                return View(model);
            }
            
            await Authenticate(model.Username);
            
            return RedirectToAction("Privacy", "Home");
        }
        
        TempData["ErrorMessage"] = "Model is not valid";
                
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        
        return RedirectToAction("Login", "Account");
    }
    
    private async Task Authenticate(string username)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, username)
        };

        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
}