using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialBook.Aplication.Command.Manager;
using SocialBook.Aplication.Command.Util;
using SocialBook.Aplication.Command;

namespace SocialBook.Aplication.Test
{
    [TestClass]
    public class CommandManagerTest
    {
        [TestMethod]
        public void GetInstance_OK()
        {
            Assert.IsInstanceOfType(CommandManager.GetInstance(), typeof(CommandManager));
        }

        [TestMethod]
        public void GetMessage_WhenIsEmpty()
        {
            string messageExpected = string.Empty;

            Assert.AreEqual(messageExpected, CommandManager.GetInstance().Message);
        }

        [TestMethod]
        public void GetMessage_WhenHaveValue()
        {
            string messageExpected = "Mensaje de prueba";
            CommandManager manager = CommandManager.GetInstance();
            CommandUtil.SetMessageResponse(messageExpected);

            Assert.AreEqual(messageExpected, manager.Message);
        }

        [TestMethod]
        public void GetMessage_CleanMessageAfterFirstGet()
        {
            string messageExpectedFirst = "Mensaje de prueba";
            string messageExpectedSecond = string.Empty;
            CommandManager manager = CommandManager.GetInstance();
            CommandUtil.SetMessageResponse(messageExpectedFirst);

            Assert.AreEqual(messageExpectedFirst, manager.Message);
            Assert.AreEqual(messageExpectedSecond, manager.Message);
        }

        [TestMethod]
        public void GetCommand_CommandNull()
        {
            CommandManager manager = CommandManager.GetInstance();

            var commandResult = manager.GetCommand(null);

            Assert.IsInstanceOfType(commandResult, typeof(ErrorCommand));
        }

        [TestMethod]
        public void GetCommand_CommandValid()
        {
            CommandManager manager = CommandManager.GetInstance();

            Assert.IsInstanceOfType(manager.GetCommand("follow"), typeof(FollowCommand));
            Assert.IsInstanceOfType(manager.GetCommand("post"), typeof(PostedCommand));
            Assert.IsInstanceOfType(manager.GetCommand("dashboard"), typeof(DashBoardCommand));
            Assert.IsInstanceOfType(manager.GetCommand("exit"), typeof(ExitCommand));
        }
    }
}
