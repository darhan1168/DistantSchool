using DistantSchool.Helpers;

namespace DistantSchool.Services.Interfaces;

public interface ISeedUsersService
{
    Task<Result<bool>> AddSeedUsers();
}