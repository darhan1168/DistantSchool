using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class AssignmentController : Controller
{
    private readonly IAssignmentService _assignmentService;
    private readonly IUserService _userService;

    public AssignmentController(IAssignmentService assignmentService, IUserService userService)
    {
        _assignmentService = assignmentService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string subjectName = null)
    {
        var username = User.Identity.Name;

        var user = await _userService.GetUserByUsername(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var assignments = await _assignmentService.GetAssignmentsByUser(user, subjectName);

        return View(assignments);
    }

    [HttpGet]
    public IActionResult CreateAssignment(int lessonId)
    {
        var assignmentViewModel = new AssignmentViewModel()
        {
            LessonId = lessonId
        };
        
        return View(assignmentViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAssignment(AssignmentViewModel assignmentViewModel)
    {
        var assignment = new Assignment();
        assignmentViewModel.MapTo(assignment);
        
        var creatingResult = await _assignmentService.AddAssignment(assignment);
        
        if (!creatingResult.IsSuccessful)
        {
            TempData["ErrorMessage"] = creatingResult.Message;
                
            return View(assignmentViewModel);
        }
        
        return RedirectToAction("Details", "Lesson", new { id = assignmentViewModel.LessonId});
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var assignment = await _assignmentService.GetAssignmentById(id);

        if (assignment == null)
        {
            return NotFound();
        }

        return View(assignment); 
    }
}