using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class ClassController : Controller
{
    private readonly IUserService _userService;
    private readonly IClassService _classService;
    private readonly IStudentService _studentService;
    private readonly ISubjectService _subjectService;

    public ClassController(
        IUserService userService, 
        IClassService classService,
        IStudentService studentService,
        ISubjectService subjectService)
    {
        _userService = userService;
        _classService = classService;
        _studentService = studentService;
        _subjectService = subjectService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(SortingParam sortOrder)
    {
        var username = User.Identity.Name;

        var user = await _userService.GetUserByUsername(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var person = user.Student;

        if (person != null)
        {
            return RedirectToAction("Details", new { id = person.ClassID });
        }
        else
        {
            var classes = await _classService.GetClasses(sortOrder);

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

        var classes = await _classService.GetClasses(SortingParam.Name);

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

    [HttpGet]
    public async Task<IActionResult> GetStudentsWithoutClass()
    {
        var studentsWithoutClass = await _studentService.GetStudentsWithoutClass();
        
        return View(studentsWithoutClass);
    }
    
    [HttpGet]
    public IActionResult AddClass()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddClass(Class newClass)
    {
        if (ModelState.IsValid)
        {
            var addingResult = await _classService.AddClass(newClass);
        
            if (!addingResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = addingResult.Message;
                
                return View(newClass);
            }
            
            return RedirectToAction("Index"); 
        }
        
        return View(newClass);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteClass(int id)
    {
        var deletingResult = await _classService.DeleteClass(id);
        
        if (!deletingResult.IsSuccessful)
        {
            TempData["ErrorMessage"] = deletingResult.Message;
        }
        
        return RedirectToAction("Index"); 
    }
    
    [HttpGet]
    public async Task<IActionResult> EditValuesOfClass(int id)
    { 
        var classToEdit = await _classService.GetClassById(id);
        
        return View(classToEdit);
    }

    [HttpPost]
    public async Task<IActionResult> EditValuesOfClass(Class updatedClass)
    {
        if (ModelState.IsValid)
        {
            var updatingResult = await _classService.UpdateClass(updatedClass);
        
            if (!updatingResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = updatingResult.Message;
            }
            
            return RedirectToAction("Details", new { id = updatedClass.Id });
        }

        return View(updatedClass);
    }
    
    [HttpGet]
    public async Task<IActionResult> Journal()
    {
        var allClasses = await _classService.GetClasses(SortingParam.Name);

        var journalViewModel = new JournalViewModel
        {
            Classes = allClasses
        };

        return View(journalViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Journal(JournalViewModel model)
    {
        var selectedClass = await _classService.GetClassById(model.SelectedClassId);
        var allClasses = await _classService.GetClasses(SortingParam.Name);
        var students = selectedClass.Students.ToList();
        var subjects = await _subjectService.GetSubjects();

        var reviewsResult = await _classService.GetGradesForClass(model.SelectedClassId);

        if (reviewsResult.IsSuccessful)
        {
            var journalViewModel = new JournalViewModel
            {
                Students = students,
                Subjects = subjects,
                Reviews = reviewsResult.Data,
                Classes = allClasses
            };

            return View(journalViewModel);
        }
        
        return View(model);
    }
}