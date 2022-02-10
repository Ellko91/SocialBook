using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialBook.Aplication.Command.Manager;
using SocialBook.Aplication.Command.Util;
using System;

namespace SocialBook.Aplication.Test
{
    [TestClass]
    public class CommandUtilTest
    {
        [TestMethod]
        public void ArgumentsTokenizer_AlfanumericSeparator()
        {
            string[] expected = new string[] { "This", "is", "a", "test", "tokenizer", "Alfanumeric" };
            string arguments = "This$is$a$test$tokenizer$Alfanumeric";
            char separator = '$';

            string[] result = CommandUtil.TokenizerArguments(arguments, separator);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ArgumentsTokenizer_WhitoutSeparator()
        {
            string[] expected = new string[] { "Hola", "Mundo" };
            string arguments = "Hola Mundo";

            string[] result = CommandUtil.TokenizerArguments(arguments);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SetMessageResponse_NullMessage()
        {
            CommandUtil.SetMessageResponse(null);
            string expected = string.Empty;

            var result = CommandManager.GetInstance().Message;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SetMessageResponse_EmptyMessage()
        {
            string expected = string.Empty;

            CommandUtil.SetMessageResponse(string.Empty);
            var result = CommandManager.GetInstance().Message;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SetMessageResponse_WithMessage()
        {
            string expected = "Esto es una prueba de mensaje";

            CommandUtil.SetMessageResponse("Esto es una prueba de mensaje");
            var result = CommandManager.GetInstance().Message;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CopyArrayFromIndex_IndexOutOfRange()
        {
            string[] arguments = new[] { "Prueba", "salir", "de", "array"};
            int index = arguments.Length + 1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 
                    CommandUtil.CopyArrayFromIndex(arguments, index));
        }

        [TestMethod]
        public void CopyArrayFromIndex_EmptyArguments()
        {
            string[] expected = new string[] { };
            string[] arguments = new string [] { };
            int index = arguments.Length;

            string[] result = CommandUtil.CopyArrayFromIndex(arguments, index);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CopyArrayFromIndex_WithRangeArgument()
        {
            string[] expected = new string[] { "esto", "se", "copia"};
            string[] arguments = new[] { "Prueba", "esto", "se", "copia", "esto", "no" };
            int index = 1;
            int range = 3;

            string[] result = CommandUtil.CopyArrayFromIndex(arguments, index, range);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CopyArrayFromIndex_IndexOutWithRangeArgument()
        {
            string[] expected = new string[] { "esto", "se", "copia" };
            string[] arguments = new[] { "Prueba", "esto", "se", "copia"};
            int index = 1;
            int range = 99;

            string[] result = CommandUtil.CopyArrayFromIndex(arguments, index, range);
            
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConcatArguments_IndexOutOfRange()
        {
            string[] arguments = new[] { "Prueba", "salir", "de", "array" };
            int index = arguments.Length + 1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                    CommandUtil.ConcatArguments(arguments, index));
        }

        [TestMethod]
        public void ConcatArguments_EmptyArguments()
        {
            string expected = string.Empty;
            string[] arguments = new string[] { };
            int index = arguments.Length;

            string result = CommandUtil.ConcatArguments(arguments, index);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConcatArguments_FromIndex()
        {
            string expected = "estoseconcatena";
            string[] arguments = new[] { "esto", "no", "se", "concatena", "esto", "se", "concatena" };
            int index = 4;

            string result = CommandUtil.ConcatArguments(arguments, index);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConcatArguments_WithRangeArgument()
        {
            string expected = "estosecopia";
            string[] arguments = new[] { "esto", "se", "copia", "esto", "no" };
            int index = 0;
            int range = 3;

            string result = CommandUtil.ConcatArguments(arguments, index, "", range);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ConcatArguments_WithRangeArgumentAndSeparator()
        {
            string expected = "esto se copia";
            string separator = " ";
            string[] arguments = new[] { "Prueba", "esto", "se", "copia", "esto", "no" };
            int index = 1;
            int range = 3;

            string result = CommandUtil.ConcatArguments(arguments, index, separator, range);

            Assert.AreEqual(expected, result);
        }
    }
}
