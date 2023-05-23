using FluentResults;
using WebApiEmployees.Domain.DTOs.Employee;
using WebApiEmployees.Domain.Models;

namespace WebApiEmployees.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<Result<IEnumerable<EmployeesShortDto>>> GetListEmployees();
        Task<Result<Employee>> GetEmployee(Guid guid);
        Task<Result<string>> DeleteEmployee(Guid guid);
        Task<Result<string>> CreateEmployee(EmployeeDto employeeViewModel);
        Task<Result<string>> EditEmployee(Guid guid, EmployeeDto employeeViewModel);
    }
}
