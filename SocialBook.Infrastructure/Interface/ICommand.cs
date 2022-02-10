namespace SocialBook.Infrastructure.Interface
{
    public interface ICommand
    {
        string getCommandName();
        void execute(string[] arguments);
    }
}
