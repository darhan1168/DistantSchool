using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IStudentService
{
    Task<Student?> GetStudentById(int id);
}