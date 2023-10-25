using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class LessonService : ILessonService
{
    private readonly IBaseRepository<Lesson> _repository;

    public LessonService(IBaseRepository<Lesson> repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<bool>> AddLesson(Lesson lesson)
    {
        if (lesson == null)
        {
            return new Result<bool>(false, $"{nameof(lesson)} not found");
        }

        var addingResult = await _repository.Add(lesson);
        
        return !addingResult.IsSuccessful ? new Result<bool>(false, addingResult.Message) : new Result<bool>(true);
    }

    public async Task<List<Lesson>> GetLessonsByUser(User user)
    {
        List<Lesson> lessons;
        
        if (user.UserType == UserType.Teacher)
        {
            lessons = await _repository.Get(l => l.TeacherClassSubject.TeacherId == user.Teacher.TeacherID);
        }
        else
        {
            lessons = await _repository.Get(l => l.TeacherClassSubject.ClassId == user.Student.ClassID);
        }
        
        return lessons;
    }
}