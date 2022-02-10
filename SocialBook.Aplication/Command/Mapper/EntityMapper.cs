using SocialBook.Domain.Entity;
using System;

namespace SocialBook.Aplication.Command
{
    public static class EntityMapper
    {
        public static FollowedUser ConvertToFollowedUser(this User user, User followedUser)
        {
            FollowedUser follow = null;

            if (user != null && followedUser != null)
            {
                follow = new FollowedUser()
                {
                    User = user,
                    FollowedUsers = followedUser
                };
            }

            return follow;
        }

        public static Posted ConvertToUserPosted(this User user, string postContent)
        {
            Posted post = null;

            if (user != null && !string.IsNullOrWhiteSpace(postContent))
            {
                post = new Posted()
                {
                    OwnerUser = user,
                    PostContent = postContent,
                    DateTimePost = DateTime.Now
                };
            }
            
            return post;
        }
    }
}
