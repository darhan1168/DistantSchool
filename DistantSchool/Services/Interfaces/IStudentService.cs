using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IStudentService
{
    Task<Result<bool>> UpdateStudent(Student student);
    Task<Result<bool>> UpdateClassByStudentId(int studentId, int classId);
    Task<Student?> GetStudentById(int id);
}