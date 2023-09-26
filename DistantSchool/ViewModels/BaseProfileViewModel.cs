using DistantSchool.Models;

namespace DistantSchool.ViewModels;

public class BaseProfileViewModel
{
    public int UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public UserType UserType { get; set; }
    public DateTime? Birthdate { get; set; }
}