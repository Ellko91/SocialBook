using SocialBook.Aplication.Command.Util;
using SocialBook.Infrastructure.Base;
using SocialBook.Domain.DataContext.Repository;
using SocialBook.Domain.Entity;
using System.Collections.Generic;
using System;

namespace SocialBook.Aplication.Command
{
    public class DashBoardCommand : CommandBase
    {
        private readonly string CommandName = CommandEnum.DASHBOARD.ToString();
        private readonly UserRepository _userRepository;
        private readonly PostedRepository _postedRepository;

        public DashBoardCommand()
        {
            _userRepository = new UserRepository();
            _postedRepository = new PostedRepository();
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
  
            GetDashboard(arguments[0]);
        }

        public override string getCommandName()
        {
            return CommandName;
        }

        private void GetDashboard(string nick)
        {
            List<Posted> posted = null;
            string message = string.Empty;
            var user = _userRepository.GetOne(x => x.Nick.ToUpper() == nick.ToUpper());

            if (user == null)
            {
                message += string.Format("No se encontró ningún usuario {0}", nick);
            }

            if (string.IsNullOrEmpty(message))
            {
                posted = _postedRepository.GetAll(user);

                foreach (var follow in user.FollowedUsers)
                {
                    posted.AddRange(_postedRepository.GetAll(follow.FollowedUsers));
                }

                message = posted.ConvertToDashBoardResponse();
            }

            CommandUtil.SetMessageResponse(message);
        }
    }
}
