using ConsoleAppEmployees.Enums;

namespace ConsoleAppEmployees.DataController
{
    internal interface IDataController
    {
        public List<Employee> GetListEmployee(int startOffset);
        public int CountEmployees();
        public Employee GetEmployeeById(Guid guid);
        public void Add(Employee emp);
        public void Remove(Guid guid);
        public void Edit(Guid guid, string newField, EmployeeFieldsEnum field);
    }
}
