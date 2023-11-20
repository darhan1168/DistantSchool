using Core.Helpers;
using DistantSchool.Helpers;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class ReportService : IReportService
{
    private readonly IStudentService _studentService;
    private readonly ITeacherService _teacherService;

    public ReportService(IStudentService studentService, ITeacherService teacherService)
    {
        _studentService = studentService;
        _teacherService = teacherService;
    }
    
    public async Task<Result<byte[]>> GenerateGradesReportAsync(int studentId)
    {
        List<string> tableHeaders = new List<string> { "Date", "Grade", "Subject" };
        List<List<string>> tableData = new List<List<string>>();
        
        var student = await _studentService.GetStudentById(studentId);

        var grades = student.Grades;

        foreach (var grade in grades)
        {
            tableData.Add(new List<string>
            {
                grade.Date.ToString("MM/dd/yyyy hh:mm tt"),
                !string.IsNullOrWhiteSpace(grade.Value.ToString()) ? grade.Value.ToString() : "N/A",
                grade.Assignment.Lesson.TeacherClassSubject.Subject.Name
            });
        }

        var pdfBytes = PdfGeneratorService.CreatePdf(tableData,
                $"Report for {student.LastName} {student.LastName} {student.Patronymic}", tableHeaders);

        return new Result<byte[]>(true, pdfBytes);
    }

    public async Task<Result<byte[]>> GenerateLessonsReportAsync(int teacherId)
    {
        List<string> tableHeaders = new List<string> { "Date", "Class", "Subject" };
        List<List<string>> tableData = new List<List<string>>();
        
        var teacher = await _teacherService.GetTeacherById(teacherId);

        foreach (var teachersClassesSubject in teacher.TeachersClassesSubjects)
        {
            foreach (var lesson in teachersClassesSubject.Lessons)
            {
                tableData.Add(new List<string>
                {
                    lesson.Date.ToString("MM/dd/yyyy hh:mm tt"),
                    teachersClassesSubject.Class.Name,
                    teachersClassesSubject.Subject.Name
                });
            }
        }
        
        var pdfBytes = PdfGeneratorService.CreatePdf(tableData,
            $"Report for {teacher.LastName} {teacher.LastName} {teacher.Patronymic}", tableHeaders);

        return new Result<byte[]>(true, pdfBytes);
    }
}