using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Services.Interfaces;
using DistantSchool.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserService _userService;
    private readonly IStudentService _studentService;
    private readonly ITeacherService _teacherService;

    public ProfileController(IUserService userService, IStudentService studentService, ITeacherService teacherService)
    {
        _userService = userService;
        _studentService = studentService;
        _teacherService = teacherService;
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

        var profileViewModel = GetBaseProfileViewModel(user);

        return View(profileViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> EditStudent(int id)
    {
        var student = await _studentService.GetStudentById(id);
        
        if (student == null)
        {
            TempData["ErrorMessage"] = $"{nameof(student)} not found";
                
            return RedirectToAction("Index", "Profile");
        }

        var studentProfileViewModel = new StudentProfileViewModel();
        student.MapTo(studentProfileViewModel);
        
        return View(studentProfileViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditStudent(StudentProfileViewModel model)
    {
        var student = new Student();
        model.MapTo(student);

        var result = await _studentService.UpdateStudent(student);

        if (!result.IsSuccessful)
        {
            TempData["ErrorMessage"] = result.Message;
                
            return View(model);
        }
        
        return RedirectToAction("Index", "Profile");
    }
    
    [HttpGet]
    public async Task<IActionResult> EditTeacher(int id)
    {
        var teacher = await _teacherService.GetTeacherById(id);
        
        if (teacher == null)
        {
            TempData["ErrorMessage"] = $"{nameof(teacher)} not found";
                
            return RedirectToAction("Index", "Profile");
        }

        var teacherProfileViewModel = new TeacherProfileViewModel();
        teacher.MapTo(teacherProfileViewModel);
        
        return View(teacherProfileViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditTeacher(TeacherProfileViewModel model)
    {
        var teacher = new Teacher();
        model.MapTo(teacher);
            
        var result = await _teacherService.UpdateTeacher(teacher);
        
        if (!result.IsSuccessful)
        {
            TempData["ErrorMessage"] = result.Message;
                
            return View(model);
        }
        
        return RedirectToAction("Index", "Profile");
    }

    private BaseProfileViewModel GetBaseProfileViewModel(User user)
    {
        BaseProfileViewModel profileViewModel;

        if (user.UserType == UserType.Student)
        {
            var student = user.Student;

            profileViewModel = new StudentProfileViewModel
            {
                StudentID = student.StudentID,
                Address = student.Address,
                GradeLevel = student.GradeLevel
            };
            student.MapTo(profileViewModel);
        }
        else
        {
            var teacher = user.Teacher;
            
            profileViewModel = new TeacherProfileViewModel
            {
                TeacherID = teacher.TeacherID,
                TeachingYears = teacher.TeachingYears
            };
            teacher.MapTo(profileViewModel);
        }
        
        user.MapTo(profileViewModel);

        return profileViewModel;
    }
}