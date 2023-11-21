using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class SubjectService : ISubjectService
{
    private readonly IBaseRepository<Subject> _repository;

    public SubjectService(IBaseRepository<Subject> repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> AddSubject(Subject subject)
    {
        if (subject == null)
        {
            return new Result<bool>(false, $"{nameof(subject)} not found");
        }
        
        if (await IsValidSubject(subject))
        {
            return new Result<bool>(false, $"{nameof(subject)} is not valid");
        }
        
        var addingResult = await _repository.Add(subject);
        
        return !addingResult.IsSuccessful ? new Result<bool>(false, addingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> UpdateSubject(Subject subject)
    {
        if (subject == null)
        {
            return new Result<bool>(false, $"{nameof(subject)} not found");
        }
        
        if (await IsValidSubject(subject))
        {
            return new Result<bool>(false, $"{nameof(subject)} is not valid");
        }
        
        var updatingResult = await _repository.Update(subject);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> DeleteSubject(int subjectId)
    {
        var schoolClass = await _repository.GetById(subjectId);
        
        if (schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} not found");
        }
        
        if (schoolClass.TeachersClassesSubjects.Any())
        {
            return new Result<bool>(false, $"You can't delete class, which uses for lesson");
        }
        
        var deletingResult = await _repository.Delete(schoolClass);
        
        return !deletingResult.IsSuccessful ? new Result<bool>(false, deletingResult.Message) : new Result<bool>(true);
    }
    
    public async Task<Subject> GetClassById(int subjectId)
    {
        var subject = await _repository.GetById(subjectId);

        return subject;
    }

    public async Task<List<Subject>> GetSubjects()
    {
        var subjects = await _repository.Get();

        return subjects;
    }
    
    private async Task<bool> IsValidSubject(Subject subject)
    {
        return (await _repository.Get(s => s.Name == subject.Name)).Any();
    }
}