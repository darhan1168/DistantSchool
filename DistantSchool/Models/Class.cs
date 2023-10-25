using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Class
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AcademicYear { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<TeachersClassesSubject> TeachersClassesSubjects { get; set; } = new List<TeachersClassesSubject>();
}
