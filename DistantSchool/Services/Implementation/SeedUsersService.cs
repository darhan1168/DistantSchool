using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class SeedUsersService : ISeedUsersService
{
    private readonly IUserService _userService;
    private readonly string? _userPassword;
    private readonly string? _userUsername;
    private const int UsersInDistantSchool = 10;
    private const int TeacherInDistantSchool = 5;

    public SeedUsersService(IConfiguration configuration, IUserService userService)
    {
        _userService = userService;
        _userPassword = configuration["UserInfo:Password"];
        _userUsername = configuration["UserInfo:Username"];
    }
    
    public async Task<Result< bool>> AddSeedUsers()
    {
        if (!(await _userService.HasUsersInDataBase()))
        {
            if (_userUsername == null || _userPassword == null)
            {
                return new Result<bool>(false, "Username or password not found in a configuration");
            }

            for (var i = 0; i < UsersInDistantSchool; i++)
            {
                var user = new User
                {
                    Username = _userUsername,
                    PasswordHash = _userPassword,
                    UserType = UserType.Teacher
                };

                user.Username += i;

                if (i > TeacherInDistantSchool - 1)
                {
                    user.UserType = UserType.Student;
                }

                var addingAdminResult = await _userService.Register(user);
        
                if (!addingAdminResult.IsSuccessful)
                {
                    return new Result<bool>(false, addingAdminResult.Message);
                }
            }
        }
        
        return new Result<bool>(true);
    }
}