using ConsoleAppEmployees.DataController;
using ConsoleAppEmployees.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppEmployees.ConsolePages
{
    internal class ProfileEmpPage : IConsolePage
    {
        private Dictionary<ConsoleKey, IConsolePage> _supportNextCommand;
        private readonly IConsolePage _prevCommand;
        private readonly IDataController _dataController;
        private readonly Employee _currentEmployee;
        public ProfileEmpPage(IConsolePage prevCommand, IServiceProvider services, Guid guid)
        {
            _dataController = services.GetService<IDataController>()!;
            _prevCommand = prevCommand;
            _currentEmployee = _dataController.GetEmployeeById(guid);

            _supportNextCommand = new Dictionary<ConsoleKey, IConsolePage>()
            {
                { ConsoleKey.D1, new EditEmpPage(prevCommand, guid, EmployeeFieldsEnum.FirstName, services) },
                { ConsoleKey.D2, new EditEmpPage(prevCommand, guid, EmployeeFieldsEnum.LastName, services) },
                { ConsoleKey.D3, new EditEmpPage(prevCommand, guid, EmployeeFieldsEnum.PhoneNumber, services) },
                { ConsoleKey.D4, new EditEmpPage(prevCommand, guid, EmployeeFieldsEnum.Description, services) }
            };
        }
        public void Functionality()
        {
            Print.PrintLogo(LogoEnum.EmployeeProfile);
            Console.WriteLine(
            @$"
[1] Имя: {_currentEmployee.FirstName}
[2] Фамилия: {_currentEmployee.LastName}
[3] Телефон: {_currentEmployee.PhoneNumber}
[4] Описание: {_currentEmployee.Description}

Для редактирования данных нажмите соответствующую клавишу...
");
        }
        public IConsolePage Execute(ConsoleKey key)
        {
            if (!_supportNextCommand.TryGetValue(key, out var command))
            {
                return this;
            }
            return command;
        }

        public IConsolePage PrevCommand() => _prevCommand;

    }
}
