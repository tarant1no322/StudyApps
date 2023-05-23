using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiEmployees.Domain.DTOs.User;
using WebApiEmployees.Domain.Enums;
using WebApiEmployees.Domain.Models;
using WebApiEmployees.Service.Interfaces;

namespace WebApiEmployees.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        public AuthService(IConfiguration configuration, ApplicationDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }
        public async Task<Result<string>> Login(UserDto userDto)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(x => x.Username == userDto.Username);
            if (user?.Username == null)
            {
                return Result.Fail("Пользователь не найден!");
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            {
                return Result.Fail("Неверный пароль!");
            }

            string token = CreateToken(user);
            return Result.Ok(token);
        }

        public async Task<Result<string>> Delete(string username)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return Result.Fail("Пользователь не найден!");
            }
            try
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return Result.Ok("Пользователь удален!");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<string>> Register(UserDto userDto)
        {
            if (userDto.Username == userDto.Password)
                return Result.Fail("Логин не должен быть раен паролю!");

            try
            {
                User? user = await _db.Users.FirstOrDefaultAsync(x => x.Username == userDto.Username);
                if (user != null)
                {
                    return Result.Fail("Username уже занят!");
                }
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                user = new User()
                {
                    Username = userDto.Username,
                    PasswordHash = passwordHash,
                    Role = Role.Worker,
                };

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }


        public async Task<Result<string>> ChangeUserRole(ChangeRoleDto changeRoleDto)
        {
            if (!Enum.IsDefined(typeof(Role), changeRoleDto.Role))
            {
                return Result.Fail("Введена невалидная роль для пользователя!");
            }

            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Username == changeRoleDto.Username);
                if (user == null)
                {
                    return Result.Fail("Пользователь не найден!");
                }
                user.Role = (Role)changeRoleDto.Role;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return Result.Ok("Новая роль установлена!");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
