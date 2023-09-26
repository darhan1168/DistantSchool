using DistantSchool.Helpers;
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

    public async Task<Result<bool>> UpdateTeacher(Teacher teacher)
    {
        if (teacher == null)
        {
            return new Result<bool>(false, $"{nameof(teacher)} not found");
        }

        var updatingResult = await _repository.Update(teacher);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Teacher?> GetTeacherById(int id)
    {
        return await _repository.GetById(id);
    }
}