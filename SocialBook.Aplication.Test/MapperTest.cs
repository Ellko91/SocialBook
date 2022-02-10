using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialBook.Aplication.Command;
using SocialBook.Domain.Entity;
using System.Collections.Generic;
using System.Text;
using System;

namespace SocialBook.Aplication.Test
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void ConvertToUserPosted_UserNull()
        {
            User userImput = null;
            string posted = "";

            Posted postResult = userImput.ConvertToUserPosted(posted);

            Assert.IsNull(postResult);
        }

        [TestMethod]
        public void ConvertToUserPosted_PostNull()
        {
            User userImput = new User("Nick");
            string posted = null;

            Posted postResult = userImput.ConvertToUserPosted(posted);

            Assert.IsNull(postResult);
        }

        [TestMethod]
        public void ConvertToUserPosted_PostedOK()
        {
            User userImput = new User("Nick");
            string posted = "Posteado";
            Posted postExpectex = new Posted()
            {
                OwnerUser = userImput,
                PostContent = posted,
                DateTimePost = DateTime.Now
            };

            Posted postResult = userImput.ConvertToUserPosted(posted);

            Assert.AreEqual(postExpectex.OwnerUser, postResult.OwnerUser);
            Assert.AreEqual(postExpectex.PostContent, postResult.PostContent);
            Assert.IsNotNull(postResult.DateTimePost);
        }

        [TestMethod]
        public void ConvertToFollowedUser_UserNull()
        {
            User userImput = null;
            User followeUserInput = new User("NickFollow");

            FollowedUser followResult = userImput.ConvertToFollowedUser(followeUserInput);

            Assert.IsNull(followResult);
        }

        [TestMethod]
        public void ConvertToFollowedUser_FollowedUserNull()
        {
            User userImput = new User("Nick");
            User followeUserInput = null;

            FollowedUser followResult = userImput.ConvertToFollowedUser(followeUserInput);

            Assert.IsNull(followResult);
        }

        [TestMethod]
        public void ConvertToFollowedUser_FollowedUserOK()
        {
            User userImput = new User("Nick");
            User followeUserInput = new User("NickFollow");
            FollowedUser followedExpected = new FollowedUser()
            {
                User = userImput,
                FollowedUsers = followeUserInput
            };

            FollowedUser followResult = userImput.ConvertToFollowedUser(followeUserInput);

            Assert.AreEqual(followedExpected.User, followResult.User);
            Assert.AreEqual(followedExpected.FollowedUsers, followResult.FollowedUsers);
        }

        [TestMethod]
        public void ConvertToDashBoardResponse_PostedNull()
        {
            List<Posted> listPostExpected = null;

            string dashboardResult = listPostExpected.ConvertToDashBoardResponse();

            Assert.IsTrue(string.IsNullOrEmpty(dashboardResult));
        }

        [TestMethod]
        public void ConvertToDashBoardResponse_PostedEmpty()
        {
            List<Posted> listPostExpected = new List<Posted>();

            string dashboardResult = listPostExpected.ConvertToDashBoardResponse();

            Assert.IsTrue(string.IsNullOrEmpty(dashboardResult));
        }

        [TestMethod]
        public void ConvertToDashBoardResponse_PostedOK()
        {
            List<Posted> listPostExpected = MockPostedList();
            string dashboardExpected = MockDashboardExpected();

            string dashboardResult = listPostExpected.ConvertToDashBoardResponse();

            Assert.AreEqual(dashboardExpected, dashboardResult);
        }

        private List<Posted> MockPostedList()
        {
            List<Posted> listPost = new List<Posted>();
            User user = new User("Nick");

            listPost.Add(new Posted() 
            { 
                OwnerUser = user, 
                PostContent = "Post 1", 
                DateTimePost = new DateTime(2022, 02, 09, 18, 59, 00) }
            );
            listPost.Add(new Posted() 
            { 
                OwnerUser = user, 
                PostContent = "Post 2", 
                DateTimePost = new DateTime(2022, 02, 09, 19, 59, 00)
            });
            listPost.Add(new Posted() 
            { 
                OwnerUser = user, 
                PostContent = "Post 3", 
                DateTimePost = new DateTime(2022, 02, 09, 20, 59, 00)
            });

            return listPost;
        }

        private string MockDashboardExpected()
        {
            String expected = string.Empty;
            StringBuilder builder = new StringBuilder(expected);

            builder.AppendLine("\"Post 1\" Nick 18:59");
            builder.AppendLine("\"Post 2\" Nick 19:59");
            builder.AppendLine("\"Post 3\" Nick 20:59");
            expected = builder.ToString();

            return expected;
        }
    }
}
