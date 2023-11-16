using DistantSchool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class TeacherController : Controller
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTeachers()
    {
        var teachers = await _teacherService.GetAllTeachers();
        
        return View(teachers);
    }
}