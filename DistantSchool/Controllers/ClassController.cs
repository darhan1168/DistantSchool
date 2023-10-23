using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class ClassController : Controller
{
    private readonly IUserService _userService;
    private readonly IClassService _classService;
    private readonly IStudentService _studentService;

    public ClassController(
        IUserService userService, 
        IClassService classService,
        IStudentService studentService)
    {
        _userService = userService;
        _classService = classService;
        _studentService = studentService;
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

    [HttpGet]
    public async Task<IActionResult> EditClass(int studentId)
    {
        var student = await _studentService.GetStudentById(studentId);
        
        if (student == null)
        {
            TempData["ErrorMessage"] = $"{nameof(student)} not found";
                
            return RedirectToAction("Index", "Profile");
        }

        var classes = await _classService.GetClasses();

        var viewModel = new EditClassViewModel
        {
            StudentId = student.StudentID,
            StudentName = student.FirstName,
            Classes = classes
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditClass(EditClassViewModel model)
    {
        var editClassResult =
            await _studentService.UpdateClassByStudentId(model.StudentId, model.SelectedClassId);
        
        if (!editClassResult.IsSuccessful)
        {
            TempData["ErrorMessage"] = editClassResult.Message;
                
            return View(model);
        }
        
        return RedirectToAction("Details", "Class", new {id = model.SelectedClassId}); 
    }
}