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
    
    public async Task<List<Subject>> GetSubjects()
    {
        var subjects = await _repository.Get();

        return subjects;
    }
}