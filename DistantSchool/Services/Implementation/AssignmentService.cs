using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class AssignmentService : IAssignmentService
{
    private readonly IBaseRepository<Assignment> _repository;
    private readonly ILessonService _lessonService;

    public AssignmentService(IBaseRepository<Assignment> repository, ILessonService lessonService)
    {
        _repository = repository;
        _lessonService = lessonService;
    }
    
    public async Task<Result<bool>> AddAssignment(Assignment assignment)
    {
        if (assignment == null)
        {
            return new Result<bool>(false, $"{nameof(assignment)} not found");
        }
        
        var addingResult = await _repository.Add(assignment);
        
        return !addingResult.IsSuccessful ? new Result<bool>(false, addingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> UpdateAssignment(Assignment assignment)
    {
        if (assignment == null)
        {
            return new Result<bool>(false, $"{nameof(assignment)} not found");
        }

        var updatingResult = await _repository.Update(assignment);
        
        return !updatingResult.IsSuccessful ? new Result<bool>(false, updatingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> DeleteAssignment(int assignmentId)
    {
        var assignment = await _repository.GetById(assignmentId);
        
        if (assignment == null)
        {
            return new Result<bool>(false, $"{nameof(assignment)} not found");
        }

        var deletingResult = await _repository.Delete(assignment);
        
        return !deletingResult.IsSuccessful ? new Result<bool>(false, deletingResult.Message) : new Result<bool>(true);
    }

    public async Task<Assignment> GetAssignmentById(int assignmentId)
    {
        var assignment = await _repository.GetById(assignmentId);

        return assignment;
    }
}