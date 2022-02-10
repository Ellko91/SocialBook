using SocialBook.Aplication.Command.Util;
using SocialBook.Infrastructure.Base;
using System;

namespace SocialBook.Aplication.Command
{
    public class ErrorCommand : CommandBase
    {
        private readonly string CommandName = CommandEnum.ERROR.ToString();

        public override void execute(string[] arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("Argumento nulo");
            }

            CommandUtil.SetMessageResponse(arguments.Length == 0 ? CommandName : CommandUtil.ConcatArguments(arguments, 0, " "));
        }

        public override string getCommandName()
        {
            return CommandName;
        }
    }
}
