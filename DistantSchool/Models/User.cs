using System;
using System.Collections.Generic;

namespace DistantSchool.Models;

public partial class User
{
    public int UserID { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public UserType UserType { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
