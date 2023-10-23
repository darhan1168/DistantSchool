using DistantSchool.Models;

namespace DistantSchool.ViewModels;

public class EditClassViewModel
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public int SelectedClassId { get; set; }
    public List<Class> Classes { get; set; }
}