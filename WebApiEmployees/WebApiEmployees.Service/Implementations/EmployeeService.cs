using FluentResults;
using Microsoft.EntityFrameworkCore;
using WebApiEmployees.Domain.DTOs.Employee;
using WebApiEmployees.Domain.Models;
using WebApiEmployees.Service.Interfaces;

namespace WebApiEmployees.Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        public EmployeeService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<Result<string>> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                DateCreate = DateTime.UtcNow,
                DateUpdate = DateTime.UtcNow,
                Description = employeeDto.Description,
                PhoneNumber = employeeDto.PhoneNumber
            };

            try
            {
                await _db.Employees.AddAsync(employee);
                await _db.SaveChangesAsync();
                return Result.Ok("Сотрудник успешно создан!");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<string>> DeleteEmployee(Guid guid)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == guid);
                if (employee == null)
                {
                    return Result.Fail("Сотрудник не найден!");
                }
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();

                return Result.Ok("Сотрудник удален!");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<string>> EditEmployee(Guid guid, EmployeeDto employeeDto)
        {


            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == guid);
                if (employee == null)
                {
                    return Result.Fail("Сотрудник не найден!");
                }
                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.DateUpdate = DateTime.UtcNow;
                employee.PhoneNumber = employeeDto.PhoneNumber;
                employee.Description = employeeDto.Description;

                _db.Employees.Update(employee);
                await _db.SaveChangesAsync();
                return Result.Ok("Сотрудник отредактирован!");

            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Employee>> GetEmployee(Guid guid)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == guid);
                if (employee == null)
                {
                    return Result.Fail<Employee>("Сотрудник не найден!");
                }
                return Result.Ok(employee);
            }
            catch (Exception ex)
            {
                return Result.Fail<Employee>(ex.Message);
            }
        }
        public async Task<Result<IEnumerable<EmployeesShortDto>>> GetListEmployees()
        {
            try
            {
                List<EmployeesShortDto> employees = await _db.Employees
                    .Select(x => new EmployeesShortDto()
                    {
                        Id = x.Id,
                        FullName = $"{x.LastName} {x.FirstName}"
                    })
                    .ToListAsync();

                if (employees.Count == 0)
                {
                    return Result.Fail("Найдено 0 элементов");
                }

                return Result.Ok<IEnumerable<EmployeesShortDto>>(employees);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
