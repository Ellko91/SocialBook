using SocialBook.Infrastructure.Base;

namespace SocialBook.Aplication.Command
{
    public class ExitCommand : CommandBase
    {
        private readonly string CommandName = CommandEnum.EXIT.ToString();

        public override void execute(string[] arguments)
        {
            System.Environment.Exit(0);
        }

        public override string getCommandName()
        {
            return CommandName;
        }
    }
}
