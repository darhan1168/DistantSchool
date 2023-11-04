using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class GradeService : IGradeService
{
    private readonly IBaseRepository<Grade> _repository;

    public GradeService(IBaseRepository<Grade> repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<bool>> AddGrade(Grade grade)
    {
        if (grade == null)
        {
            return new Result<bool>(false, $"{nameof(grade)} not found");
        }
        
        grade.Date = DateTime.Now;

        var addingResult = await _repository.Add(grade);
        
        return !addingResult.IsSuccessful ? new Result<bool>(false, addingResult.Message) : new Result<bool>(true);
    }
}