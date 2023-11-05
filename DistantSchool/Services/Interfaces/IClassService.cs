using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IClassService
{
    Task<Result<bool>> AddClass(Class schoolClass);
    Task<Result<bool>> UpdateClass(Class schoolClass);
    Task<Result<bool>> DeleteClass(int classId);
    Task<List<Class>> GetClasses(SortingParam sortOrder);
    Task<Class> GetClassById(int classId);
}