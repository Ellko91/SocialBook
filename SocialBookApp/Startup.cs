using SocialBook.Aplication.Command.Manager;
using SocialBook.Aplication.Command.Util;
using SocialBook.Domain.DataContext;
using System;

namespace SocialBookApp.Presentation
{
    class Startup
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SocialBook");
            CommandManager manager = CommandManager.GetInstance();
            var commandInitializer = manager.GetCommand("ADDUSER");
            commandInitializer.execute(new string[] { "@Alfonso" });
            Console.WriteLine(manager.Message);
            commandInitializer.execute(new string[] { "@Ivan" });
            Console.WriteLine(manager.Message);
            commandInitializer.execute(new string[] { "@Alicia" });
            Console.WriteLine(manager.Message);

            while (true)
            {
                try
                {
                    string commandLine = Console.ReadLine();
                    var commandTokens = CommandUtil.TokenizerArguments(commandLine);
                    var commandArguments = CommandUtil.CopyArrayFromIndex(commandTokens, 1);
                    string commandName = commandTokens[0];
                    var command = manager.GetCommand(commandName);
                    command.execute(commandArguments);
                    Console.WriteLine(manager.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
