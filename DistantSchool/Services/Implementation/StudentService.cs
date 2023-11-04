using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DistantSchool.Services.Implementation;

public class StudentService : IStudentService
{
    private readonly IBaseRepository<Student> _repository;
    private readonly IClassService _classService;

    public StudentService(IBaseRepository<Student> repository, IClassService classService)
    {
        _repository = repository;
        _classService = classService;
    }

    public async Task<Result<bool>> UpdateStudent(Student student)
    {
        if (student == null)
        {
            return new Result<bool>(false, $"{nameof(student)} not found");
        }

        var updatingResult = await _repository.Update(student);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> UpdateClassByStudentId(int studentId, int classId)
    {
        var student = await _repository.GetById(studentId);
        var schoolClass = await _classService.GetClassById(classId);

        if (student == null || schoolClass == null)
        {
            return new Result<bool>(false, $"{nameof(student)} or {nameof(schoolClass)} not found");
        }

        //student.Class = schoolClass;
        student.ClassID = schoolClass.Id;
        
        var updatingResult = await _repository.Update(student);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Student?> GetStudentById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<List<Student>> GetAllStudents(string searchQuery = null)
    {
        List<Student> students;
        
        students = await _repository.Get();
        
        if (!String.IsNullOrEmpty(searchQuery))
        {
            // Придумати як дістати саме за ПІБ
            
            //students = await _repository.Get(s => s.);
        }

        return students;

    public async Task<List<Student>> GetStudentsWithoutClass()
    {
        var studentsWithoutClass = await _repository.Get(s => s.Class == null);

        return studentsWithoutClass;
    }
}