using SocialBook.Domain.DataContext.Container;
using SocialBook.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SocialBook.Domain.DataContext.Repository
{
    public class PostedRepository 
    {
        public int Create(Posted post)
        {
            if (post == null)
            {
                throw new ArgumentNullException("Argumento no puede ser nulo");
            }

            if (ExistUser(post.OwnerUser))
            {
                GetUser(post.OwnerUser).UserPostings.Add(post);
                return 1;
            }
            return 0;
        }

        public List<Posted> GetAll(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Argumento no puede ser nulo");
            }

            if (!ExistUser(user))
            {
                throw new ArgumentException("El usuario no existe");
            }

            return GetUser(user).UserPostings.ToList();
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
