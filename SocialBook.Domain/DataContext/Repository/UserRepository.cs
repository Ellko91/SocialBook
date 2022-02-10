using SocialBook.Domain.DataContext.Container;
using SocialBook.Domain.Entity;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace SocialBook.Domain.DataContext.Repository
{
    public class UserRepository
    {
        public int Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Argumento no puede ser nulo");
            }
            
            if (GetOne(e => e.Nick.ToUpper() == user.Nick.ToUpper()) == null)
            {
                SocialBookContainer.users.Add(user);
                return 1;
            }
            return 0;
        }

        public User GetOne(Expression<Func<User, bool>> predicate)
        {
            return SocialBookContainer.users.AsQueryable().Where(predicate).FirstOrDefault();
        }
    }
}
