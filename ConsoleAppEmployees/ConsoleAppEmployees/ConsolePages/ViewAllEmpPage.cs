using ConsoleAppEmployees.Enums;

namespace ConsoleAppEmployees.ConsolePages
{
    /// <summary>
    /// класс должен вызывать GetList после чего вызывать класс просмотра пользователя,
    ///     передавая в качестве параметра Guid Employee.
    ///     
    /// </summary>
    internal class ViewAllEmpPage : IConsolePage
    {

        private readonly IConsolePage _prevCommand;
        private IServiceProvider _services;
        private GenerateList _list;

        public ViewAllEmpPage(IConsolePage prevCommand, IServiceProvider services)
        {
            _services = services;
            _prevCommand = prevCommand;
            _list = new GenerateList(_services);
        }

        public void Functionality()
        {
            _list = new GenerateList(_services);
            Print.PrintLogo(LogoEnum.ViewEmployees);
            if (_list.IsListEmpty())
            {
                Console.WriteLine("\nСотрудников в базе нет!");
                return;
            }
            Console.WriteLine("\tВыберите сотрудника для просмотра детальной информации: \n");
            _list.PrintList();

        }
        public IConsolePage Execute(ConsoleKey key)
        {
            if (!_list.GetList().TryGetValue(key, out Guid guid))
            {
                if (key == ConsoleKey.DownArrow)
                    _list.PageDoun();
                else if (key == ConsoleKey.UpArrow)
                    _list.PageUp();
                return this;
            }
            return new ProfileEmpPage(this, _services, guid);
        }
        public IConsolePage PrevCommand() => _prevCommand;
    }
}
