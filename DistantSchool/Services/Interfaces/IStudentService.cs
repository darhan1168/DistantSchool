using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IStudentService
{
    Task<Result<bool>> UpdateStudent(Student student);
    Task<Student?> GetStudentById(int id);
}