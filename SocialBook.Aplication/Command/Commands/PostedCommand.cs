using SocialBook.Domain.DataContext.Repository;
using SocialBook.Aplication.Command.Util;
using SocialBook.Infrastructure.Base;
using System;

namespace SocialBook.Aplication.Command
{
    public class PostedCommand : CommandBase
    {
        private readonly string CommandName = CommandEnum.POST.ToString();
        private readonly UserRepository _userRepository;
        private readonly PostedRepository _postedRepository;

        public PostedCommand()
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

            if (arguments.Length < 2)
            {
                throw new ArgumentException("Cantidad de argumentos invalida");
            }

            var argumentPost = CommandUtil.CopyArrayFromIndex(arguments, 1);
            UserPosting(arguments[0], CommandUtil.ConcatArguments(argumentPost, 0, " "));
        }

        public override string getCommandName()
        {
            return CommandName;
        }

        private void UserPosting(string nick, string postContent)
        {
            string message = string.Empty;
            var user = _userRepository.GetOne(x => x.Nick.ToUpper() == nick.ToUpper());

            if (user == null)
            {
                message += string.Format("No se encontró ningún usuario {0}", nick);
            }

            if (string.IsNullOrEmpty(message))
            {
                var post = user.ConvertToUserPosted(postContent);
                _postedRepository.Create(post);
                message = string.Format("{0} posted -> \"{1}\" {2}", user.Nick, post.PostContent, post.DateTimePost.ToString("HH:mm"));
            }

            CommandUtil.SetMessageResponse(message);
        }
    }
}
