using SocialBook.Domain.DataContext.Container;
using SocialBook.Domain.Entity;
using System.Linq;
using System;

namespace SocialBook.Domain.DataContext.Repository
{
    public class FollowedUserRepository
    {
        public int Create(FollowedUser follow)
        {
            if (follow == null)
            {
                throw new ArgumentNullException("Argumento no puede ser nulo");
            }

            if (ExistUser(follow.User) && ExistUser(follow.FollowedUsers))
            {
                if (GetOne(follow.User, follow.FollowedUsers) != null)
                {
                    return 0;
                } 

                GetUser(follow.User).FollowedUsers.Add(follow);
                return 1;
            }
            return 0;
        }

        public FollowedUser GetOne(User user, User followedUser)
        {
            if (user == null || followedUser == null)
            {
                throw new ArgumentNullException("El argumento no puede ser nulo");
            }

            if (!ExistUser(user) || !ExistUser(followedUser))
            {
                return null;
            }

            return SocialBookContainer.users.First(x => x.Nick == user.Nick)
                                      .FollowedUsers.FirstOrDefault(x => x.FollowedUsers.Nick == followedUser.Nick);
        }

        private bool ExistUser(User user)
        {
            return SocialBookContainer.users.Exists(e => e.Nick.ToUpper() == user.Nick.ToUpper());
        }

        private User GetUser(User user)
        {
            return SocialBookContainer.users.First(e => e.Nick.ToUpper() == user.Nick.ToUpper());
        }
    }
}
