using DistantSchool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class ClassController : Controller
{
    private readonly IUserService _userService;
    private readonly IClassService _classService;

    public ClassController(IUserService userService, IClassService classService)
    {
        _userService = userService;
        _classService = classService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var username = User.Identity.Name;

        var user = await _userService.GetUserByUsername(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        if (user.Student != null)
        {
            return RedirectToAction("Details", new { id = user.Student.ClassID });
        }
        else
        {
            var classes = await _classService.GetClasses();

            if (!classes.Any())
            {
                TempData["ErrorMessage"] = "Classes not added yet";
                
                return View();
            }

            return View(classes);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var schoolClass = await _classService.GetClassById(id);

        if (schoolClass == null)
        {
            TempData["ErrorMessage"] = $"Class with id : {id} not found";
                
            return RedirectToAction("Index", "Profile");
        }

        return View(schoolClass);
    }
}