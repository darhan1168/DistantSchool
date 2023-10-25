using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IAssignmentService
{
    Task<Result<bool>> AddAssignment(Assignment assignment);
    Task<Result<bool>> UpdateAssignment(Assignment assignment);
    Task<Result<bool>> DeleteAssignment(int assignmentId);
}