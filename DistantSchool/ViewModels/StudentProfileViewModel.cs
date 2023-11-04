using DistantSchool.Models;

namespace DistantSchool.ViewModels;

public class StudentProfileViewModel : BaseProfileViewModel
{
    public int StudentID { get; set; }
    public string Address { get; set; }
    public int GradeLevel { get; set; }
    public Class Class { get; set; }
    public int ClassID { get; set; }
}