using SocialBook.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialBook.Aplication.Command
{
    public static class ResponseMapper
    {
        public static string ConvertToDashBoardResponse(this List<Posted> posted)
        {
            string message = string.Empty;

            if (posted != null && posted.Count > 0)
            {
                List<Posted> orderPosted = posted.AsQueryable().OrderBy(x => x.DateTimePost).ToList();
                StringBuilder buildMessage = new StringBuilder(message);

                foreach (var post in orderPosted)
                {
                    buildMessage.AppendLine(string.Format("\"{0}\" {1} {2}",
                                            post.PostContent,
                                            post.OwnerUser.Nick,
                                            post.DateTimePost.ToString("HH:mm")));
                }

                message = buildMessage.ToString();
            }

            return message;
        }
    }
}
