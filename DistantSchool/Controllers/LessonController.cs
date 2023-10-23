using DistantSchool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class LessonController : Controller
{
    private readonly ILessonService _lessonService;
    private readonly IUserService _userService;

    public LessonController(ILessonService lessonService, IUserService userService)
    {
        _lessonService = lessonService;
        _userService = userService;
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

        var lessons = await _lessonService.GetLessonsByUser(user);

        return View(lessons);
    }
}