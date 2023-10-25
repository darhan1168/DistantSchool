using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface ITeachersClassesSubjectsService
{
    Task<List<TeachersClassesSubject>> GetTeachersClassesSubjectsByTeacherId(int id);
}