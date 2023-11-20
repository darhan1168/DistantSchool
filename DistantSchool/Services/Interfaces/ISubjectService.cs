using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface ISubjectService
{
    Task<Result<bool>> AddSubject(Subject subject);
    Task<Result<bool>> UpdateSubject(Subject subject);
    Task<Result<bool>> DeleteSubject(int subjectId);
    Task<Subject> GetClassById(int subjectId);
    Task<List<Subject>> GetSubjects();
}