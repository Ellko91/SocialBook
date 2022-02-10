using SocialBook.Aplication.Command.Util;
using SocialBook.Infrastructure.Base;
using SocialBook.Domain.DataContext.Repository;
using SocialBook.Domain.Entity;
using System;

namespace SocialBook.Aplication.Command
{
    public class AddUserCommand : CommandBase
    {
        private readonly string CommandName = CommandEnum.ADDUSER.ToString();
        private readonly UserRepository _userRepository;

        public AddUserCommand()
        {
            _userRepository = new UserRepository();
        }

        public override void execute(string[] arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("Argumento nulo");
            }

            if (arguments.Length != 1)
            {
                throw new ArgumentException("Cantidad de argumentos invalida");
            }

            CreateUser(arguments[0]);
        }

        public override string getCommandName()
        {
            return CommandName;
        }

        private void CreateUser(string nick)
        {
            string message = string.Empty;

            var result = _userRepository.Create(new User(nick));

            if (result == 1)
            {
                message = string.Format("Usuario {0} creado", nick);
            }
            else 
            {
                message = string.Format("Usuario {0} ya existe", nick);
            }

            CommandUtil.SetMessageResponse(message);
        }
    }
}
