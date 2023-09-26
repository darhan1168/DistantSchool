using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface ITeacherService
{
    Task<Teacher?> GetTeacherById(int id);
}