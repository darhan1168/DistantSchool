using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int TeacherClassSubjectId { get; set; }

    public DateTime Date { get; set; }

    public string? URL { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual TeachersClassesSubject TeacherClassSubject { get; set; } = null!;
}
