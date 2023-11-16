using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IStudentService
{
    Task<Result<bool>> UpdateStudent(Student student);
    Task<Result<bool>> UpdateClassByStudentId(int studentId, int classId);
    Task<Student?> GetStudentById(int id);
    Task<List<Student>> GetAllStudents(string searchQuery = null, string className = null);
    Task<Result<bool>> UpdateAverageRate(int studentId);
    Task<List<Student>> GetStudentsWithoutClass();
}