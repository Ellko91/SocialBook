using SocialBook.Infrastructure.Interface;

namespace SocialBook.Infrastructure.Base
{
    public abstract class CommandBase : ICommand
    {
        public abstract void execute(string[] arguments);

        public abstract string getCommandName();
    }
}
