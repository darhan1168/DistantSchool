using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class ClassService : IClassService
{
    private readonly IBaseRepository<Class> _repository;

    public ClassService(IBaseRepository<Class> repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> AddClass(Class schoolClass)
    {
        if (schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} not found");
        }
        
        var addingResult = await _repository.Add(schoolClass);
        
        return !addingResult.IsSuccessful ? new Result<bool>(false, addingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> UpdateClass(Class schoolClass)
    {
        if (schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} not found");
        }
        
        var updatingResult = await _repository.Update(schoolClass);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> DeleteClass(int classId)
    {
        var schoolClass = await _repository.GetById(classId);
        
        if (schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} not found");
        }
        
        if (schoolClass.Students.Any())
        {
            return new Result<bool>(false, $"You can't delete class with students");
        }
        
        var deletingResult = await _repository.Delete(schoolClass);
        
        return !deletingResult.IsSuccessful ? new Result<bool>(false, deletingResult.Message) : new Result<bool>(true);
    }

    public async Task<List<Class>> GetClasses()
    {
        var classes = await _repository.Get();

        return classes;
    }

    public async Task<Class> GetClassById(int classId)
    {
        var classById = await _repository.GetById(classId);

        return classById;
    }
}