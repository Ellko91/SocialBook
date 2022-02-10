using SocialBook.Infrastructure.Base;
using System.Collections.Generic;

namespace SocialBook.Domain.Entity
{
    public class User : BaseEntity
    {
        public string Nick;
        public ICollection<Posted> UserPostings;
        public ICollection<FollowedUser> FollowedUsers;

        public User(string nick)
        {
            Nick = nick;
            UserPostings = new List<Posted>();
            FollowedUsers = new List<FollowedUser>();
        }
    }
}
