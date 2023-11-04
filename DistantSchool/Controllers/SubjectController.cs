using DistantSchool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class SubjectController : Controller
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var subjects = await _subjectService.GetSubjects();

        return View(subjects);
    }
}