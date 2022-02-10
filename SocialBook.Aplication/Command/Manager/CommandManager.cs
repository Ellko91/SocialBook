using SocialBook.Infrastructure.Interface;
using SocialBook.Infrastructure.Base;
using System.Collections.Generic;

namespace SocialBook.Aplication.Command.Manager
{
    public class CommandManager
    {
        private static CommandManager commandManager;
        private readonly Dictionary<string, CommandBase> Commands = new Dictionary<string, CommandBase>();
        private string message = string.Empty;

        public string Message
        {
            get 
            {
                var auxMessage = message;
                message = string.Empty;
                return auxMessage;
            }
            internal set
            {
                if (value != null)
                {
                    message = value;
                }
            }
        }

        private CommandManager()
        {
            RegisterCommand(CommandEnum.FOLLOW.ToString(), new FollowCommand());
            RegisterCommand(CommandEnum.POST.ToString(), new PostedCommand());
            RegisterCommand(CommandEnum.DASHBOARD.ToString(), new DashBoardCommand());
            RegisterCommand(CommandEnum.EXIT.ToString(), new ExitCommand());
            RegisterCommand(CommandEnum.ADDUSER.ToString(), new AddUserCommand());
            //siguientes comandos
        }

        public static CommandManager GetInstance()
        {
            if (commandManager is null)
            {
                commandManager = new CommandManager();
            }
            return commandManager;
        }

        public ICommand GetCommand(string commandName)
        {
            CommandBase command;
            if (!string.IsNullOrEmpty(commandName) && Commands.TryGetValue(commandName.ToUpper(), out command))
            {
                return command;
            }
            else
            {
                return new ErrorCommand();
            }
        }

        private void RegisterCommand(string commandName, CommandBase command)
        {
            commandName = commandName.ToUpper();
            if (!Commands.ContainsKey(commandName))
            {
                Commands.Add(commandName, command);
            }
        }
    }
}
