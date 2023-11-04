using DistantSchool.Models;

namespace DistantSchool.ViewModels;

public class CreateGradeViewModel
{
    public int AssignmentId { get; set; }

    public int StudentId { get; set; }

    public int Value { get; set; }

    public string? Comment { get; set; }

    public Lesson Lesson { get; set; }
}