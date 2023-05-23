using System.Web.Http.Description;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEmployees.Domain.DTOs.Employee;
using WebApiEmployees.Domain.Models;
using WebApiEmployees.Service.Interfaces;

namespace WebApiEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ResponseType(typeof(List<Employee>))]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> List()
        {
            return await _employeeService.GetListEmployees().ToActionResult();
            //if(result.IsFailed)
            //{
            //    return result.ToActionResult();
            //}
            //return result.ToActionResult();
        }

        [HttpGet("{guid}")]
        [ResponseType(typeof(Employee))]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> GetEmployee(Guid guid)
        {
            return await _employeeService.GetEmployee(guid).ToActionResult();
            //if(responce.StatusCode == Domain.Enums.StatusCode.OK)
            //{ 
            //    return View(responce.Data); 
            //}
            //return RedirectToAction("Error");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] Guid guid)
        {
            return await _employeeService.DeleteEmployee(guid).ToActionResult();
            //if (responce.StatusCode == WebApiEmployees.Domain.Enums.StatusCode.OK)
            //{
            //    return RedirectToAction("List");
            //}
            //return RedirectToAction("Error");
        }


        [HttpPut]
        [ResponseType(typeof(EmployeeDto))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([FromBody] EmployeeDto employeeDto)
        {
            return await _employeeService.EditEmployee(employeeDto.Id, employeeDto).ToActionResult();
        }

        [HttpPost]
        [ResponseType(typeof(EmployeeDto))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
        {
            return await _employeeService.CreateEmployee(employeeDto).ToActionResult();
        }
    }
}