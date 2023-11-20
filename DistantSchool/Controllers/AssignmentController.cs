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

        await _assignmentService.UpdateProgressForAssignments(assignments);

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
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    { 
        var assignment = await _assignmentService.GetAssignmentById(id);
        
        return View(assignment);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Assignment updatedAssignment)
    {
        if (ModelState.IsValid)
        {
            var updatingResult = await _assignmentService.UpdateAssignment(updatedAssignment);
        
            if (!updatingResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = updatingResult.Message;
            }
            
            return RedirectToAction("Details", new { id = updatedAssignment.AssignmentId });
        }

        return View(updatedAssignment);
    }
}