using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface ILessonService
{
    Task<Result<bool>> AddLesson(Lesson lesson);
    Task<List<Lesson>> GetLessonsByUser(User user);
}