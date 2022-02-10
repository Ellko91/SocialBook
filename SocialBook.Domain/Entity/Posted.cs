using SocialBook.Infrastructure.Base;
using System;

namespace SocialBook.Domain.Entity
{
    public class Posted : BaseEntity
    {
        public User OwnerUser;
        public string PostContent;
        public DateTime DateTimePost;
    }
}
