using DistantSchool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents(string searchQuery = null, string className = null)
    {
        var students = await _studentService.GetAllStudents(searchQuery, className);
        
        return View(students);
    }
}