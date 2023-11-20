using DistantSchool.Helpers;

namespace DistantSchool.Services.Interfaces;

public interface IReportService
{
    Task<Result<byte[]>> GenerateGradesReportAsync(int studentId);
    
    Task<Result<byte[]>> GenerateLessonsReportAsync(int teacherId);
}