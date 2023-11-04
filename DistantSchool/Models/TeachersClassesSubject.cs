﻿using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class TeachersClassesSubject
{
    public int TeacherClassSubjectId { get; set; }

    public int TeacherId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Subject Subject { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
