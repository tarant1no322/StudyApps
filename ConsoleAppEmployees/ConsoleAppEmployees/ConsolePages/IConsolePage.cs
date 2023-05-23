namespace ConsoleAppEmployees.ConsolePages
{
    internal interface IConsolePage
    {
        void Functionality();
        IConsolePage Execute(ConsoleKey key);
        IConsolePage PrevCommand();
    }
}
