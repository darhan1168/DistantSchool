using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public int LessonId { get; set; }

    public string? Description { get; set; }

    public AssignmentStatus Status { get; set; }

    public DateTime Deadline { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Lessons Lesson { get; set; } = null!;
}
