using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public int SubjectId { get; set; }

    public DateTime Date { get; set; }

    public string? URL { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
