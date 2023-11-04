using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class TeachersClassesSubjectsService : ITeachersClassesSubjectsService
{
    private readonly IBaseRepository<TeachersClassesSubject> _repository;

    public TeachersClassesSubjectsService(IBaseRepository<TeachersClassesSubject> repository)
    {
        _repository = repository;
    }

    public async Task<List<TeachersClassesSubject>> GetTeachersClassesSubjectsByTeacherId(int id)
    {
        var teachersClassesSubjects = await _repository.Get(tcs => tcs.TeacherId == id);

        return teachersClassesSubjects;
    }
}