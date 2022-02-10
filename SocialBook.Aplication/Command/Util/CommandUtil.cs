using SocialBook.Aplication.Command.Manager;
using System;

namespace SocialBook.Aplication.Command.Util
{
    public static class CommandUtil
    {
        public static string[] TokenizerArguments(string arguments, char separator = ' ')
        {
            return arguments.Split(separator);
        }

        public static void SetMessageResponse(string message)
        {
            CommandManager.GetInstance().Message = message;
        }

        public static string[] CopyArrayFromIndex(string[] argumentsIn, int indexArgumentInit, int argumentRange = 0)
        {
            int size = argumentsIn.Length - indexArgumentInit;

            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("El indice supera el tamaño del array");
            }

            if (argumentsIn.Length - (indexArgumentInit + argumentRange) < 0 || argumentRange == 0)
            {
                argumentRange = size;
            }

            string[] argumentsOut = new string[argumentRange];
            Array.Copy(argumentsIn, indexArgumentInit, argumentsOut, 0, argumentRange);

            return argumentsOut;
        }

        public static string ConcatArguments(string[] arguments, int indexArgumentsInit, string separator = "", int argumentRange = 0)
        {
            try
            {
                string[] argumentsRequired = CopyArrayFromIndex(arguments, indexArgumentsInit, argumentRange);
                return string.Join(separator, argumentsRequired, 0, argumentsRequired.Length);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                throw exception;
            }
        }
    }
}
