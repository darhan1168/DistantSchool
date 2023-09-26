using DistantSchool.Helpers;
using DistantSchool.Models;

namespace DistantSchool.Services.Interfaces;

public interface IUserService
{
    Task<Result<bool>> Register(User user);
    Task<Result<bool>> Login(string username, string password);
    Task<User?> GetUserByUsername(string username);
    Task<bool> HasUsersInDataBase();
}