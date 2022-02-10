using SocialBook.Infrastructure.Base;

namespace SocialBook.Domain.Entity
{
    public class FollowedUser : BaseEntity
    {
        public User User;
        public User FollowedUsers;
    }
}
