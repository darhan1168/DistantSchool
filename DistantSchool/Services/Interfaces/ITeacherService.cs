using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface ITeacherService
{
    Task<Result<bool>> UpdateTeacher(Teacher teacher);
    Task<Teacher?> GetTeacherById(int id);
}