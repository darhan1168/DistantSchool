using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IClassService
{
    Task<List<Class>> GetClasses();
    Task<Class> GetClassById(int classId);
}