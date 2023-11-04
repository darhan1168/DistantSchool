using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class GradeController : Controller
{
    private readonly IGradeService _gradeService;
    private readonly ILessonService _lessonService;

    public GradeController(IGradeService gradeService, ILessonService lessonService)
    {
        _gradeService = gradeService;
        _lessonService = lessonService;
    }

    [HttpGet]
    public async Task<IActionResult> RateGrade(int studentId, int lessonId, int assignmentId)
    {
        var lesson = await _lessonService.GetLessonById(lessonId);
        
        var createGradeViewModel = new CreateGradeViewModel()
        {
            StudentId = studentId,
            Lesson = lesson,
            AssignmentId = assignmentId
        };

        return View(createGradeViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> RateGrade(Grade grade)
    {
        var ratingResult = await _gradeService.AddGrade(grade);
        
        if (!ratingResult.IsSuccessful)
        {
            TempData["ErrorMessage"] = ratingResult.Message;
        }
        
        return RedirectToAction("Details", "Assignment", new { id = grade.AssignmentId }); 
    }
}