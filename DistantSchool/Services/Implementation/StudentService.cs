using DistantSchool.Helpers;
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

    public async Task<Result<bool>> UpdateStudent(Student student)
    {
        if (student == null)
        {
            return new Result<bool>(false, $"{nameof(student)} not found");
        }

        var updatingResult = await _repository.Update(student);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Student?> GetStudentById(int id)
    {
        return await _repository.GetById(id);
    }
}