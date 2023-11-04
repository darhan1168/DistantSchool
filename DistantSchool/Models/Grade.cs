using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int AssignmentId { get; set; }

    public int StudentId { get; set; }

    public int Value { get; set; }

    public string? Comment { get; set; }

    public DateTime Date { get; set; }

    public virtual Assignment Assignment { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
