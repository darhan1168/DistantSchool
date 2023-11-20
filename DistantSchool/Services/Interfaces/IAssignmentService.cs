using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IAssignmentService
{
    Task<Result<bool>> AddAssignment(Assignment assignment);
    Task<Result<bool>> UpdateAssignment(Assignment assignment);
    Task UpdateProgressForAssignments(List<Assignment> assignments);
    Task<Result<bool>> DeleteAssignment(int assignmentId);
    Task<Assignment> GetAssignmentById(int assignmentId);
    Task<List<Assignment>> GetAssignmentsByUser(User user, string className = null);
}