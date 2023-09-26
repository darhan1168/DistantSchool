using System.ComponentModel.DataAnnotations;

namespace DistantSchool.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public UserType UserType { get; set; }
}