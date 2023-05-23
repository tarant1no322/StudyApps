using FluentResults;
using WebApiEmployees.Domain.DTOs.User;

namespace WebApiEmployees.Service.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> Register(UserDto userDto);
        Task<Result<string>> Login(UserDto userDto);
        Task<Result<string>> ChangeUserRole(ChangeRoleDto changeRoleDto);
        Task<Result<string>> Delete(string username);
    }
}
