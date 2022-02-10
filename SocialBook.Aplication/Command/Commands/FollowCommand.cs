using SocialBook.Domain.DataContext.Repository;
using SocialBook.Domain.Entity;
using SocialBook.Aplication.Command.Util;
using SocialBook.Infrastructure.Base;
using System;

namespace SocialBook.Aplication.Command
{
    public class FollowCommand : CommandBase
    {
        private readonly string CommandName = CommandEnum.FOLLOW.ToString();
        private readonly UserRepository _userRepository;
        private readonly FollowedUserRepository _followedUserRepository;

        public FollowCommand()
        {
            _userRepository = new UserRepository();
            _followedUserRepository = new FollowedUserRepository();
        }

        public override void execute(string[] arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException("Argumento nulo");
            }

            if (arguments.Length != 2)
            {
                throw new ArgumentException("Cantidad de argumentos invalida");
            }

            FollowUser(arguments[0], arguments[1]);
        }

        public override string getCommandName()
        {
            return CommandName;
        }

        private void FollowUser(string nick, string followedNick)
        {
            string message = string.Empty;
            var user = _userRepository.GetOne(x => x.Nick.ToUpper() == nick.ToUpper());
            var followedUser = _userRepository.GetOne(x => x.Nick.ToUpper() == followedNick.ToUpper());

            message += CheckUserExist(user, nick);
            message += CheckUserExist(followedUser, followedNick);

            if (string.IsNullOrEmpty(message))
            {
                if (_followedUserRepository.Create(user.ConvertToFollowedUser(followedUser)) == 1)
                {
                    message = string.Format("{0} empezó a seguir a {1}", user.Nick, followedUser.Nick);
                }
                else
                {
                    message = string.Format("{0} ya está siguiendo a {1}", nick, followedNick);
                }
            }

            CommandUtil.SetMessageResponse(message);
        }

        private string CheckUserExist(User user, string nick)
        {
            string message = string.Empty;

            if (user == null)
            {
                message += string.Format("No se encontró ningún usuario {0}\n", nick);
            }

            return message;
        }
    }
}
