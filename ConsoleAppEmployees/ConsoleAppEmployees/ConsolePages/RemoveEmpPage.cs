using ConsoleAppEmployees.DataController;
using ConsoleAppEmployees.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppEmployees.ConsolePages
{
    internal class RemoveEmpPage : IConsolePage
    {

        private readonly IConsolePage _prevCommand;
        private IDataController _dataController;
        private IServiceProvider _services;

        public RemoveEmpPage(IConsolePage prevCommand, IServiceProvider services)
        {
            _services = services;
            _dataController = services.GetService<IDataController>()!;
            _prevCommand = prevCommand;
        }

        public void Functionality()
        {
            var _list = new GenerateList(_services);
            Print.PrintLogo(LogoEnum.DeleteEmployee);
            if (_list.IsListEmpty())
            {
                Console.WriteLine("\nСотрудников в базе нет!");
                return;
            }
            Console.WriteLine("\tВыберите сотрудника для удаления: \n");
            _list.PrintList();
        }
        public IConsolePage Execute(ConsoleKey key)
        {
            var _list = new GenerateList(_services);
            if (!_list.GetList().TryGetValue(key, out Guid guid))
            {
                if (key == ConsoleKey.DownArrow)
                    _list.PageDoun();
                if (key == ConsoleKey.UpArrow)
                    _list.PageUp();
                _list = new GenerateList(_services);
                return this;
            }
            _dataController.Remove(guid);
            return new MainMenuPage(_services);
        }
        public IConsolePage PrevCommand() => _prevCommand;
    }
}
