namespace DistantSchool.ViewModels;

public class AssignmentViewModel
{
    public int LessonId { get; set; }

    public string? Description { get; set; }

    public DateTime Deadline { get; set; }
}