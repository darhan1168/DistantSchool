﻿using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Teacher
{
    public int TeacherID { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public DateTime? Birthdate { get; set; }

    public int TeachingYears { get; set; }

    public int UserID { get; set; }

    public virtual ICollection<TeachersClassesSubject> TeachersClassesSubjects { get; set; } = new List<TeachersClassesSubject>();

    public virtual User User { get; set; } = null!;
}
