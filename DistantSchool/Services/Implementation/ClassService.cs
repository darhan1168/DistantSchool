using System.Linq.Expressions;
using System.Reflection;
using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class ClassService : IClassService
{
    private readonly IBaseRepository<Class> _repository;

    public ClassService(IBaseRepository<Class> repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> AddClass(Class schoolClass)
    {
        if (schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} not found");
        }

        if (!await IsValidClass(schoolClass))
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} is not valid");
        }
        
        var addingResult = await _repository.Add(schoolClass);
        
        return !addingResult.IsSuccessful ? new Result<bool>(false, addingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> UpdateClass(Class schoolClass)
    {
        if (schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} not found");
        }
        
        if (await IsValidClass(schoolClass))
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} is not valid");
        }
        
        var updatingResult = await _repository.Update(schoolClass);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> DeleteClass(int classId)
    {
        var schoolClass = await _repository.GetById(classId);
        
        if (schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(schoolClass)} not found");
        }
        
        if (schoolClass.Students.Any())
        {
            return new Result<bool>(false, $"You can't delete class with students");
        }
        
        var deletingResult = await _repository.Delete(schoolClass);
        
        return !deletingResult.IsSuccessful ? new Result<bool>(false, deletingResult.Message) : new Result<bool>(true);
    }

    public async Task<List<Class>> GetClasses(SortingParam sortOrder)
    {
        var query = GetOrderByExpression(sortOrder);
        
        var classes = await _repository.Get(orderBy: query);

        return classes;
    }

    public async Task<Class> GetClassById(int classId)
    {
        var classById = await _repository.GetById(classId);

        return classById;
    }

    public async Task<Result<List<Review>>> GetGradesForClass(int classId)
    {
        var schoolClass = await _repository.GetById(classId);
        
        if (schoolClass == null)
        {
            return new Result<List<Review>>(false, $"{nameof(schoolClass)} not found");
        }

        var reviews = new List<Review>();
        
        var students = schoolClass.Students;
        
        foreach (var student in students)
        {
            var grades = student.Grades.GroupBy(grade => grade.Assignment.Lesson.TeacherClassSubject.Subject);

            foreach (var gradeGroup in grades)
            {
                var averageGrade = gradeGroup.Average(grade => grade.Value);

                var review = new Review
                {
                    Student = student,
                    AverageGrade = averageGrade,
                    Subject = gradeGroup.Key
                };

                reviews.Add(review);
            }
        }
        
        return new Result<List<Review>>(true, reviews);
    }

    private async Task<bool> IsValidClass(Class schoolClass)
    {
        return (await _repository.Get(c => c.Name == schoolClass.Name)).Any();
    }

    private Expression<Func<IQueryable<Class>, IOrderedQueryable<Class>>> GetOrderByExpression(SortingParam sortBy)
    {
        Expression<Func<IQueryable<Class>, IOrderedQueryable<Class>>> query;

        switch (sortBy)
        {
            case SortingParam.NameDesc:
                query = q => q.OrderByDescending(q => q.Name);
                break;
            case SortingParam.AcademicYear:
                query = q => q.OrderBy(q => q.AcademicYear);
                break;
            case SortingParam.AcademicYearDesc:
                query = q => q.OrderByDescending(q => q.AcademicYear);
                break;
            default:
                query = q => q.OrderBy(q => q.Name);
                break;
        }
        
        return query;
    }
}