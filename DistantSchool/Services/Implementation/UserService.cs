using DistantSchool.Helpers;
using DistantSchool.Models;
using DistantSchool.Repositories.Interfaces;
using DistantSchool.Services.Interfaces;

namespace DistantSchool.Services.Implementation;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _repository;
    private readonly IPasswordService _passwordService;

    public UserService(IBaseRepository<User> repository, IPasswordService passwordService, IConfiguration configuration)
    {
        _repository = repository;
        _passwordService = passwordService;
    }

    public async Task<Result<bool>> Register(User user)
    {
        if (user == null)
        {
            return new Result<bool>(false, $"{nameof(user)} not found");
        }

        if (!await IsFreeUserName(user.Username))
        {
            return new Result<bool>(false, "This username is not available");
        }

        var hashPassword = _passwordService.HashPassword(user.PasswordHash);
        user.PasswordHash = hashPassword;

        var addingResult = await _repository.Add(user);
        
        return !addingResult.IsSuccessful ? new Result<bool>(false, addingResult.Message) : new Result<bool>(true);
    }

    public async Task<Result<bool>> Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return new Result<bool>(false, $"Username or password is empty or null");
        }

        var user = await GetUserByUsername(username);
        
        if (user == null)
        {
            return new Result<bool>(false, $"User with username {username} not found");
        }
        
        if (!_passwordService.VerifyPassword(password, user.PasswordHash))
        {
            return new Result<bool>(false, "Incorrect password");
        }
        
        return new Result<bool>(true);
    }

    public async Task<bool> HasUsersInDataBase()
    {
        var usersByEmail = await _repository.Get();

        return usersByEmail.Any();
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var usersByEmail = await _repository.Get(u => u.Username == username);

        return usersByEmail.FirstOrDefault();
    }
    
    private async Task<bool> IsFreeUserName(string username)
    {
        var usersByEmail = await _repository.Get(u => u.Username == username);

        return !usersByEmail.Any();
    }
}