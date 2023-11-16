using DistantSchool.Models;

namespace DistantSchool.ViewModels;

public class JournalViewModel
{
    public List<Student> Students { get; set; }
    public List<Subject> Subjects { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Class> Classes { get; set; }
    public int SelectedClassId { get; set; }
}