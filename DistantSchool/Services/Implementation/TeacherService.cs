using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class TeacherService : ITeacherService
{
    private readonly IBaseRepository<Teacher> _repository;

    public TeacherService(IBaseRepository<Teacher> repository)
    {
        _repository = repository;
    }
    
    public async Task<Teacher?> GetTeacherById(int id)
    {
        return await _repository.GetById(id);
    }
}