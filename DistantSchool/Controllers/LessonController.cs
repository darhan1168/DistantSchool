using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class LessonController : Controller
{
    private readonly ILessonService _lessonService;
    private readonly IUserService _userService;
    private readonly IClassService _classService;
    private readonly ISubjectService _subjectService;
    private readonly ITeachersClassesSubjectsService _teachersClassesSubjectsService;

    public LessonController(ILessonService lessonService, IUserService userService, IClassService classService,
        ISubjectService subjectService, ITeachersClassesSubjectsService teachersClassesSubjectsService)
    {
        _lessonService = lessonService;
        _userService = userService;
        _classService = classService;
        _subjectService = subjectService;
        _teachersClassesSubjectsService = teachersClassesSubjectsService;
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

    [HttpGet]
    public async Task<IActionResult> CreateLesson()
    {        
        var username = User.Identity.Name;

        var user = await _userService.GetUserByUsername(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        if (user.UserType == UserType.Student)
        {
            return RedirectToAction("Index");
        }
        
        var classes = await _classService.GetClasses();
        var subjects = await _subjectService.GetSubjects();
        var teachersClassesSubjects = await _teachersClassesSubjectsService
            .GetTeachersClassesSubjectsByTeacherId(user.Teacher.TeacherID);

        var lessonViewModel = new LessonViewModel()
        {
            TeacherId = user.Teacher.TeacherID,
            Classes = classes,
            Subjects = subjects,
            TeachersClassesSubjects = teachersClassesSubjects
        };

        return View(lessonViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson(LessonViewModel lessonViewModel)
    {
        var lesson = new Lesson();
        lessonViewModel.MapTo(lesson);

        var creatingResult = await _lessonService.AddLesson(lesson);
        
        if (!creatingResult.IsSuccessful)
        {
            TempData["ErrorMessage"] = creatingResult.Message;
                
            return View(lessonViewModel);
        }
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var lesson = await _lessonService.GetLessonById(id);

        return View(lesson);
    }
}