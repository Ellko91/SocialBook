using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialBook.Aplication.Command.Manager;
using SocialBook.Domain.DataContext.Container;
using SocialBook.Domain.Entity;
using System.Collections.Generic;
using System.Text;
using System;

namespace SocialBook.Aplication.Test
{
    [TestClass]
    public class CommandTest
    {
        
        [TestMethod]
        public void AllGetCommandName_GetCommandNameBySearch()
        {
            CommandManager manager = CommandManager.GetInstance();
            string expected = "DASHBOARD";
            string commandSearch = "dashboard";
            Assert.AreEqual(expected, manager.GetCommand(commandSearch).getCommandName());

            expected = "FOLLOW";
            commandSearch = "follow";
            Assert.AreEqual(expected, manager.GetCommand(commandSearch).getCommandName());

            expected = "POST";
            commandSearch = "post";
            Assert.AreEqual(expected, manager.GetCommand(commandSearch).getCommandName());

            expected = "EXIT";
            commandSearch = "exit";
            Assert.AreEqual(expected, manager.GetCommand(commandSearch).getCommandName());

            expected = "ADDUSER";
            commandSearch = "adduser";
            Assert.AreEqual(expected, manager.GetCommand(commandSearch).getCommandName());
        }

        [TestMethod]
        public void GetCommandName_InvalidCommandBySearch()
        {
            CommandManager manager = CommandManager.GetInstance();
            string expected = "ERROR";
            string commandSearch = "ANYTHING";
            Assert.AreEqual(expected, manager.GetCommand(commandSearch).getCommandName());

            expected = "ERROR";
            commandSearch = null;
            Assert.AreEqual(expected, manager.GetCommand(commandSearch).getCommandName());
        }

        [TestMethod]
        public void CommandExecute_WhenNullArguments()
        {
            CommandManager manager = CommandManager.GetInstance();
            string[] argumentsImput = null;

            string commandSearch = "dashboard";
            var commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentNullException>(() => commandExecute.execute(argumentsImput));

            commandSearch = "follow";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentNullException>(() => commandExecute.execute(argumentsImput));

            commandSearch = "post";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentNullException>(() => commandExecute.execute(argumentsImput));

            commandSearch = "adduser";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentNullException>(() => commandExecute.execute(argumentsImput));
        }

        [TestMethod]
        public void CommandExecute_WhenEmptyArguments()
        {
            CommandManager manager = CommandManager.GetInstance();
            string[] argumentsImput = new string[] { };

            string commandSearch = "dashboard";
            var commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));

            commandSearch = "follow";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));

            commandSearch = "post";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));

            commandSearch = "adduser";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));
        }

        [TestMethod]
        public void CommandExecute_WhenExceedArguments()
        {
            CommandManager manager = CommandManager.GetInstance();

            string[] argumentsImput = new string[] { "Nick", "Nick2" };
            string commandSearch = "dashboard";
            var commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));

            argumentsImput = new string[] { "Nick", "Nick2", "Nick3" };
            commandSearch = "follow";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));

            argumentsImput = new string[] { "Nick" };
            commandSearch = "post";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));

            argumentsImput = new string[] { "Nick", "Nick2" };
            commandSearch = "adduser";
            commandExecute = manager.GetCommand(commandSearch);
            Assert.ThrowsException<ArgumentException>(() => commandExecute.execute(argumentsImput));
        }

        [TestMethod]
        public void DashboardExecute_WhenUserNotExist()
        {
            InicializerContainer();
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "No se encontró ningún usuario Nick";
            string commandSearch = "dashboard";
            string[] argumentsImput = new string[] { "Nick" };

            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void DashboardExecute_WhenUserExist()
        {
            CommandManager manager = CommandManager.GetInstance();
            manager.GetCommand("adduser").execute(new string[] { "Juan" });
            manager.GetCommand("post").execute(new string[] { "Juan", "Post", "1" });
            manager.GetCommand("post").execute(new string[] { "Juan", "Post", "2" });

            string expectedMessage = string.Empty;
            StringBuilder buildMessage = new StringBuilder(expectedMessage);
            buildMessage.AppendLine(string.Format("\"Post 1\" Juan {0}", DateTime.Now.ToString("HH:mm")));
            buildMessage.AppendLine(string.Format("\"Post 2\" Juan {0}", DateTime.Now.ToString("HH:mm")));
            expectedMessage = buildMessage.ToString();

            string commandSearch = "dashboard";
            string[] argumentsImput = new string[] { "Juan" };

            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void PostExecute_WhenUserNotExist()
        {
            InicializerContainer();
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "No se encontró ningún usuario Nick";
            string commandSearch = "post";
            string[] argumentsImput = new string[] { "Nick", "Post", "1" };

            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void PostExecute_WhenUserExist()
        {
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = string.Format("Nick posted -> \"Post 1\" {0}", DateTime.Now.ToString("HH:mm"));
            manager.GetCommand("adduser").execute(new string[] { "Nick" });

            string commandSearch = "post";
            string[] argumentsImput = new string[] { "Nick", "Post", "1" };
            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void FollowExecute_WhenUserNotExist()
        {
            InicializerContainer();
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "No se encontró ningún usuario Nick\nNo se encontró ningún usuario Nick2\n";
            string commandSearch = "follow";
            string[] argumentsImput = new string[] { "Nick", "Nick2" };

            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void FollowExecute_WhenUserExistButNotFollowUser()
        {
            InicializerContainer();
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "No se encontró ningún usuario Nick2\n";
            manager.GetCommand("adduser").execute(new string[] { "Nick" });

            string commandSearch = "follow";
            string[] argumentsImput = new string[] { "Nick", "Nick2" };
            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void FollowExecute_WhenUserAndFollowUserExist()
        {
            InicializerContainer();
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "Nick empezó a seguir a Nick2";
            manager.GetCommand("adduser").execute(new string[] { "Nick" });
            manager.GetCommand("adduser").execute(new string[] { "Nick2" });

            string commandSearch = "follow";
            string[] argumentsImput = new string[] { "Nick", "Nick2" };
            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void FollowExecute_WhenFollowedUserExist()
        {
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "Nick ya está siguiendo a Nick2";
            manager.GetCommand("adduser").execute(new string[] { "Nick" });
            manager.GetCommand("adduser").execute(new string[] { "Nick2" });

            string commandSearch = "follow";
            string[] argumentsImput = new string[] { "Nick", "Nick2" };
            manager.GetCommand(commandSearch).execute(argumentsImput);
            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void AddUserExecute_WhenUserNotExist()
        {
            InicializerContainer();
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "Usuario Nick creado";
            string commandSearch = "adduser";
            string[] argumentsImput = new string[] { "Nick" };

            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        [TestMethod]
        public void AddUserExecute_WhenUserExist()
        {
            CommandManager manager = CommandManager.GetInstance();
            string expectedMessage = "Usuario Nick ya existe";
            string commandSearch = "adduser";
            string[] argumentsImput = new string[] { "Nick" };

            manager.GetCommand(commandSearch).execute(argumentsImput);
            manager.GetCommand(commandSearch).execute(argumentsImput);
            string resultMessage = manager.Message;

            Assert.AreEqual(expectedMessage, resultMessage);
        }

        private void InicializerContainer()
        {
            SocialBookContainer.users = new List<User>();
        }
    }
}
