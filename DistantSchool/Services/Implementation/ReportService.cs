using Core.Helpers;
using DistantSchool.Helpers;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class ReportService : IReportService
{
    private readonly IStudentService _studentService;

    public ReportService(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    public async Task<Result<byte[]>> GenerateGradesReportAsync(int studentId,
        CancellationToken cancellationToken = default)
    {
        List<string> tableHeaders = new List<string> { "Date", "Grade" };
        List<List<string>> tableData = new List<List<string>>();
        
        var student = await _studentService.GetStudentById(studentId);

        var grades = student.Grades;

        foreach (var grade in grades)
        {
            tableData.Add(new List<string>
            {
                grade.Date.ToString("MM/dd/yyyy hh:mm tt"),
                !string.IsNullOrWhiteSpace(grade.Value.ToString()) ? grade.Value.ToString() : "N/A"
            });
        }

        var pdfBytes = PdfGeneratorService.CreatePdf(tableData,
                $"Report for { student.LastName} {student.Patronymic}", tableHeaders);

        return new Result<byte[]>(true, pdfBytes);
    }

    public Task<Result<byte[]>> GenerateGradesReportAsync(string studentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}