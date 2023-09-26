using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class StudentService : IStudentService
{
    private readonly IBaseRepository<Student> _repository;

    public StudentService(IBaseRepository<Student> repository)
    {
        _repository = repository;
    }
    
    public async Task<Student?> GetStudentById(int id)
    {
        return await _repository.GetById(id);
    }
}