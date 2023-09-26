using DistantSchool.Helpers;
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

        return View(profileViewModel);
    }
}