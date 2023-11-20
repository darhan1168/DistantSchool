using DistantSchool.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DistantSchool.Controllers;

public class ReportController : Controller
{
    private readonly IReportService _reportService;
    private readonly IUserService _userService;

    public ReportController(IReportService reportService, IUserService userService)
    {
        _reportService = reportService;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GenerateGradesReport()
    {
        var username = User.Identity.Name;

        var user = await _userService.GetUserByUsername(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await _reportService.GenerateGradesReportAsync(user.Student.StudentID);

        var pdfContent = result.Data;
        var contentType = "application/pdf";
        var studentName = "Student_" + username;
        var fileName = $"{studentName}.pdf";

        Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");

        return File(pdfContent, contentType, fileName);
    }
    
    [HttpGet]
    public async Task<IActionResult> GenerateLessonsReport()
    {
        var username = User.Identity.Name;

        var user = await _userService.GetUserByUsername(username);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var result = await _reportService.GenerateLessonsReportAsync(user.Teacher.TeacherID);

        var pdfContent = result.Data;
        var contentType = "application/pdf";
        var teacherName = "Teacher_" + username;
        var fileName = $"{teacherName}.pdf";

        Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");

        return File(pdfContent, contentType, fileName);
    }
}