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