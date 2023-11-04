using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IGradeService
{
    Task<Result<bool>> AddGrade(Grade grade);
}