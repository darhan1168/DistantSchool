using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<TeachersClassesSubject> TeachersClassesSubjects { get; set; } = new List<TeachersClassesSubject>();
}
