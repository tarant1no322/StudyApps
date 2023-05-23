using ConsoleAppEmployees.Enums;

namespace ConsoleAppEmployees.ConsolePages
{
    class MainMenuPage : IConsolePage
    {
        private readonly Dictionary<ConsoleKey, IConsolePage> _supportNextCommand;
        public MainMenuPage(IServiceProvider services)
        {
            _supportNextCommand = new Dictionary<ConsoleKey, IConsolePage>()
            {
                { ConsoleKey.D1, new ViewAllEmpPage(this, services) },
                { ConsoleKey.D2, new AddEmpPage(this, services) },
                { ConsoleKey.D3, new RemoveEmpPage(this, services) }
            };
        }
        public void Functionality()
        {
            Print.PrintLogo(LogoEnum.Welcome);
            Print.PrintPointsMenu(new string[] { "Список сотрудников", "Добавить сотрудника", "Удалить сотрудника" });
        }
        public IConsolePage Execute(ConsoleKey key)
        {
            if (!_supportNextCommand.TryGetValue(key, out var command))
            {
                return this;
            }
            return command;
        }

        public IConsolePage PrevCommand() => this;
    }
}
