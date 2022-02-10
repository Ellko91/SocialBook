using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialBook.Domain.DataContext.Repository;
using SocialBook.Domain.DataContext.Container;
using SocialBook.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SocialBook.Domain.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Repository_CreateWhenEntityIsNull()
        {
            User userImput = null;
            var userRepository = new UserRepository();
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.Create(userImput));

            Posted postImput = null;
            var postRepository = new PostedRepository();
            Assert.ThrowsException<ArgumentNullException>(() => postRepository.Create(postImput));

            FollowedUser followImput = null;
            var followRepository = new FollowedUserRepository();
            Assert.ThrowsException<ArgumentNullException>(() => followRepository.Create(followImput));
        }

        [TestMethod]
        public void Repository_CreateWhenEntityNotExist()
        {
            InicializerContainer();
            int createExpected = 1;

            User userImput = new User("Nick");
            var userRepository = new UserRepository();
            int createResult = userRepository.Create(userImput);
            Assert.AreEqual(createExpected, createResult);

            Posted postImput = new Posted() { OwnerUser = userImput, PostContent = "Post 1", DateTimePost = DateTime.Now };
            var postRepository = new PostedRepository();
            createResult = postRepository.Create(postImput);
            Assert.AreEqual(createExpected, createResult);

            User userFollowed = new User("Nick2");
            userRepository.Create(userFollowed);
            FollowedUser followImput = new FollowedUser() { User = userImput, FollowedUsers = userFollowed };
            var followRepository = new FollowedUserRepository();
            createResult = followRepository.Create(followImput);
            Assert.AreEqual(createExpected, createResult);
        }

        [TestMethod]
        public void Repository_CreateWhenEntityExist()
        {
            InicializerContainer();
            int createExpected = 0;

            User userImput = new User("Nick");
            var userRepository = new UserRepository();
            userRepository.Create(userImput);
            int createResult = userRepository.Create(userImput);
            Assert.AreEqual(createExpected, createResult);

            User userFollowed = new User("Nick2");
            userRepository.Create(userFollowed);
            FollowedUser followImput = new FollowedUser() { User = userImput, FollowedUsers = userFollowed };
            var followRepository = new FollowedUserRepository();
            followRepository.Create(followImput);
            createResult = followRepository.Create(followImput);
            Assert.AreEqual(createExpected, createResult);
        }

        [TestMethod]
        public void Repository_GetOneWhenUsersNull()
        {
            var userRepository = new UserRepository();
            Assert.ThrowsException<ArgumentNullException>(() => userRepository.GetOne(null));

            var followRepository = new FollowedUserRepository();
            Assert.ThrowsException<ArgumentNullException>(() => followRepository.GetOne(null, null));
        }

        [TestMethod]
        public void UserRepository_GetOneAccordExpressionNotFound()
        {
            InicializerContainer();
            string nickImput = "Nick";
            var userRepository = new UserRepository();

            var userResult = userRepository.GetOne(x => x.Nick == nickImput);
            Assert.IsNull(userResult);
        }

        [TestMethod]
        public void UserRepository_GetOneAccordExpressionFound()
        {
            InicializerContainer();
            User userExpexted = new User("Nick");
            string nickImput = "Nick";
            var userRepository = new UserRepository();

            userRepository.Create(userExpexted);
            var userResult = userRepository.GetOne(x => x.Nick == nickImput);

            Assert.AreEqual(userExpexted, userResult);
        }

        [TestMethod]
        public void UserRepository_GetOneMultipleFound()
        {
            InicializerContainer();
            User userExpexted = new User("Nick");
            User userSecond = new User("Nick2");
            string nickImput = "Nick";
            var userRepository = new UserRepository();

            userRepository.Create(userExpexted);
            userRepository.Create(userSecond);
            var userResult = userRepository.GetOne(x => x.Nick.Contains(nickImput));

            Assert.AreEqual(userExpexted, userResult);
            Assert.AreNotEqual(userSecond, userResult);
        }

        [TestMethod]
        public void PostedRepository_CreateMultiple()
        {
            InicializerContainer();
            int expectedResult = 1;
            User userImput = new User("Nick");
            new UserRepository().Create(userImput);

            Posted post1 = new Posted() { OwnerUser = userImput, PostContent = "Post 1", DateTimePost = DateTime.Now };
            Posted post2 = new Posted() { OwnerUser = userImput, PostContent = "Post 2", DateTimePost = DateTime.Now };
            var postRepository = new PostedRepository();

            var createResult = postRepository.Create(post1);
            Assert.AreEqual(expectedResult, createResult);

            createResult = postRepository.Create(post2);
            Assert.AreEqual(expectedResult, createResult);
        }

        [TestMethod]
        public void PostedRepository_GetAllWhenUserIsNull()
        {
            InicializerContainer();

            Assert.ThrowsException<ArgumentNullException>(() => new PostedRepository().GetAll(null));
        }

        [TestMethod]
        public void PostedRepository_GetAllOK()
        {
            InicializerContainer();
            User userImput = new User("Nick");
            User userExpected = userImput;
            new UserRepository().Create(userImput);

            Posted post1 = new Posted() { OwnerUser = userImput, PostContent = "Post 1", DateTimePost = DateTime.Now };
            Posted post2 = new Posted() { OwnerUser = userImput, PostContent = "Post 2", DateTimePost = DateTime.Now };
            userExpected.UserPostings.Add(post1);
            userExpected.UserPostings.Add(post2);

            var postRepository = new PostedRepository();
            postRepository.Create(post1);
            postRepository.Create(post2);

            var result = postRepository.GetAll(userImput);
            CollectionAssert.AreEquivalent(userExpected.UserPostings.ToList(), result);
        }

        [TestMethod]
        public void PostedRepository_GetAllUserNotExist()
        {
            InicializerContainer();
            User userAux = new User("Nick");
            User userImput = new User("Nick2");
            new UserRepository().Create(userAux);

            Posted post1 = new Posted() { OwnerUser = userAux, PostContent = "Post 1", DateTimePost = DateTime.Now };
            Posted post2 = new Posted() { OwnerUser = userAux, PostContent = "Post 2", DateTimePost = DateTime.Now };
            userAux.UserPostings.Add(post1);
            userAux.UserPostings.Add(post2);

            var postRepository = new PostedRepository();
            postRepository.Create(post1);
            postRepository.Create(post2);

            Assert.ThrowsException<ArgumentException>(() => postRepository.GetAll(userImput));
        }

        private void InicializerContainer()
        {
            SocialBookContainer.users = new List<User>();
        }
    }
}
