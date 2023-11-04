using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class AssignmentController : Controller
{
    private readonly IAssignmentService _assignmentService;

    public AssignmentController(IAssignmentService assignmentService)
    {
        _assignmentService = assignmentService;
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
}