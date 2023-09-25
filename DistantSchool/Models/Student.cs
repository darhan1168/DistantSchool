using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class Student
{
    public int StudentID { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime? Birthdate { get; set; }

    public int GradeLevel { get; set; }

    public int UserID { get; set; }

    public virtual User User { get; set; } = null!;
}
