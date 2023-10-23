using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface ISubjectService
{
    Task<List<Subject>> GetSubjects();
}