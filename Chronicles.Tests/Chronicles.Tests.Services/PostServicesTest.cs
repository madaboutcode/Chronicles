using Chronicles.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronicles.DataAccess.Facade;
using Chronicles.Entities;
using System.Collections.Generic;
using Moq;
using System;
using System.Linq;
using Chronicles.Framework;
using FizzWare.NBuilder;
using System.Collections.Specialized;
using StructureMap.Configuration.DSL;
using StructureMap.AutoMocking;

namespace Chronicles.Tests.Services
{

    /// <summary>
    ///This is a test class for PostServicesTest and is intended
    ///to contain all PostServicesTest Unit Tests
    ///</summary>
    [TestClass]
    public class PostServicesTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        private PostServices service = null; 
        private Mock<IPostRepository> postRepositoryMock;
        private Mock<IAppConfigProvider> appConfigProviderMock;
        private Mock<ITagRepository> tagRepositoryMock;

        private Post _AddedPost;

        #region Test Setup, Mocks

        public void SetupMocks()
        {
            var serviceMock = new MoqAutoMocker<PostServices>();
            postRepositoryMock = Mock.Get(serviceMock.Get<IPostRepository>());
            appConfigProviderMock = Mock.Get(serviceMock.Get<IAppConfigProvider>());
            tagRepositoryMock = Mock.Get(serviceMock.Get<ITagRepository>());

            SetupPostRepositoryMock(postRepositoryMock);
            SetupTagRepositoryMock(tagRepositoryMock);
            SetupAppConfigProviderMock(appConfigProviderMock);

            service = serviceMock.ClassUnderTest;

            _AddedPost = null;
        }

        private void SetupTagRepositoryMock(Mock<ITagRepository> tagRepositoryMock)
        {
            tagRepositoryMock.Setup(x => x.GetTagByName(It.IsAny<string>())).Returns((string tagname) => GetTagByName(tagname));
        }

        private Tag GetTagByName(string tagname)
        {
            if (tagname == ".net")
                return new Tag { Id = 1, TagName = ".net", DateCreated = DateTime.Parse("2009/10/10") };

            if (tagname == "misc")
                return new Tag { Id = 2, TagName = "misc", DateCreated = DateTime.Parse("2009/12/10") };

            return null;
        }

        private void SetupPostRepositoryMock(Mock<IPostRepository> postRepositoryMock)
        {
            postRepositoryMock.Setup(x => x.GetLatestPosts(It.IsAny<int>())).Returns((int count) => GeneratePosts(count));
            postRepositoryMock.Setup(x => x.AddPost(It.IsAny<Post>())).Returns((Post value) => { _AddedPost = value; return value; });
            postRepositoryMock.Setup(x => x.GetPostById(It.IsAny<int>())).Returns((int id) => { Post p = CreateNewPost(); p.Id = id; return p; });
        }

        private IList<Post> GeneratePosts(int count)
        {
            IList<Tag> tags = Builder<Tag>.CreateListOfSize(count).Build();

            var result = Builder<Post>.CreateListOfSize(count)
                         .WhereAll()
                         .HaveDoneToThem(x => x.AddTag(Pick<Tag>.RandomItemFrom(tags)))
                         .Build();

            return result;
        }


        private void SetupAppConfigProviderMock(Mock<IAppConfigProvider> appConfigProviderMock)
        {
            appConfigProviderMock.Setup(x => x.GetConfig(It.IsAny<string>())).Returns((string key) => GetAppConfig(key));
        }

        private string GetAppConfig(string key)
        {
            NameValueCollection config = new NameValueCollection();
            config.Add("NoOfHomePagePosts", "10");
            config.Add("ConnectionString", "<BLANK>");

            return config.Get(key);
        }

        private Post CreateNewPost()
        {
            return Builder<Post>.CreateNew().With(x => x.Id = 0).Build();
        }

        #endregion

        #region GetLatestPosts
        /// <summary>
        ///A test for GetLatestPosts
        ///</summary>
        [TestMethod()]
        public void GetLatestPosts_should_return_ten_posts()
        {
            //Arrange
            SetupMocks();

            //Act
            var result = service.GetLatestPosts();

            //Assert
            Assert.AreEqual(10, result.Count);
            postRepositoryMock.Verify(postrepo => postrepo.GetLatestPosts(10), Times.Exactly(1));
        }

        [TestMethod()]
        public void GetLatestPosts_with_args_should_return_correct_number_of_posts()
        {
            //Arrange
            SetupMocks();
            int postcount = 20;

            //Act
            var result = service.GetLatestPosts(postcount);

            //Assert
            Assert.AreEqual(postcount, result.Count);
            postRepositoryMock.Verify(postrepo => postrepo.GetLatestPosts(postcount), Times.Exactly(1));
        }
        #endregion

        #region AddPost

        #region AddPost_with_null_post_should_throw_exception
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddPost_with_null_post_should_throw_exception()
        {
            //Arrange
            SetupMocks();

            //Act
            service.AddPost(null);

            //Assert
            postRepositoryMock.Verify(x => x.AddPost(It.IsAny<Post>()), Times.Never());
        }
        #endregion

        #region AddPost_with_no_tags_should_throw_exception
        [TestMethod]
        [ExpectedException(typeof(ConstraintViolationException))]
        public void AddPost_with_no_tags_should_throw_exception()
        {
            //Arrange
            SetupMocks();
            Post post = new Post();

            //Act
            service.AddPost(post);

            //Assert
            postRepositoryMock.Verify(x => x.AddPost(It.IsAny<Post>()), Times.Never());
        }
        #endregion

        #region AddPost_when_adding_an_tag_with_a_blank_tagname_should_throw_exception
        [TestMethod]
        [ExpectedException(typeof(ConstraintViolationException))]
        public void AddPost_when_adding_an_tag_with_a_blank_tagname_should_throw_exception()
        {
            //Arrange
            SetupMocks();
            Post post = new Post
            {
                Id = 0
            };

            post.AddTag(new Tag());

            //Act
            service.AddPost(post);

            //Assert
            tagRepositoryMock.Verify(x => x.GetTagByName(It.IsAny<string>()), Times.Never());
            postRepositoryMock.Verify(x => x.AddPost(It.IsAny<Post>()), Times.Never());
        }
        #endregion

        #region AddPost_when_adding_a_tag_that_already_exists_should_use_the_same_instance_of_the_tag

        [TestMethod]
        public void AddPost_when_adding_a_tag_that_already_exists_should_use_the_same_instance_of_the_tag() 
        {
            //Arrange
            SetupMocks();

            Tag newTag1 = new Tag { Id = 0, TagName = ".net" };
            Tag newTag2 = new Tag { Id = 0, TagName = "misc" };

            Post post = new Post();
            post.AddTag(newTag1);
            post.AddTag(newTag2);

            //Act
            service.AddPost(post);
            
            //Assert 
            Assert.IsNotNull(_AddedPost,"The post was not saved");
            Assert.AreEqual(2, _AddedPost.Tags.Count);

            Tag tag1 = _AddedPost.Tags.Where(t => t.TagName == ".net").FirstOrDefault();
            Tag tag2 = _AddedPost.Tags.Where(t => t.TagName == "misc").FirstOrDefault();

            Assert.IsNotNull(tag1);
            Assert.IsNotNull(tag2);

            Assert.AreEqual(1, tag1.Id);
            Assert.AreEqual(2, tag2.Id);
        }

        #endregion

        #region AddPost_when_adding_a_tag_that_doesnot_exists_should_add_a_new_tag_with_id_equal_to_zero
        [TestMethod]
        public void AddPost_when_adding_a_tag_that_doesnot_exists_should_add_a_new_tag_with_id_equal_to_zero()
        {
            //Arrange 
            SetupMocks();

            Tag newtag = new Tag { Id = 0, TagName = "Newtag" };
            Post post = CreateNewPost();

            post.AddTag(newtag);

            //Act
            service.AddPost(post);

            //Assert
            Assert.IsNotNull(_AddedPost);
            Assert.AreEqual(1, _AddedPost.Tags.Count);
            Assert.AreSame(newtag, _AddedPost.Tags[0]);
        }

        #endregion

        #region AddPost_when_adding_a_tag_should_get_rid_of_duplicate_tags
        [TestMethod]
        public void AddPost_when_adding_a_tag_should_get_rid_of_duplicate_tags()
        {
            //Arrange
            SetupMocks();

            Post post = CreateNewPost();
            post.AddTag(new Tag { Id = 0, TagName = "NewTag" });
            post.AddTag(new Tag { Id = 0, TagName = "NewTag" });
            post.AddTag(new Tag { Id = 0, TagName = "NewTag" });
            post.AddTag(new Tag { Id = 0, TagName = "NewTag" });
            post.AddTag(new Tag { Id = 0, TagName = "NewTag" });
            post.AddTag(new Tag { Id = 0, TagName = "NewTag" });

            //Act
            service.AddPost(post);

            //Assert
            Assert.IsNotNull(_AddedPost);
            Assert.AreEqual(1, _AddedPost.Tags.Count);
            Assert.AreEqual("NewTag", _AddedPost.Tags[0].TagName);
        } 
        #endregion

        #region AddPost_when_adding_a_post_the_id_should_be_zero
        [TestMethod]
        public void AddPost_when_adding_a_post_the_id_should_be_zero()
        {
            //Arrange
            SetupMocks();

            Post post = CreateNewPost();
            post.Id = 100;
            post.AddTag(new Tag { Id = 0, TagName = "misc" });

            //Act
            service.AddPost(post);

            //Assert
            Assert.IsNotNull(_AddedPost);
            Assert.AreEqual(0, _AddedPost.Id);
        } 
        #endregion

        #region AddPost_when_adding_a_post_the_dates_should_be_proper
        [TestMethod]
        public void AddPost_when_adding_a_post_the_dates_should_be_proper()
        {
            //Arrange 
            SetupMocks();

            Post post = CreateNewPost();
            post.Id = 100;
            post.AddTag(new Tag { Id = 0, TagName = "misc" });

            //Act
            service.AddPost(post);

            //Assert
            Assert.IsNotNull(_AddedPost);
            Assert.IsTrue((_AddedPost.ModifiedDate - DateTime.Now).Seconds < 5, "ModifiedDate is not proper");
            Assert.IsTrue((_AddedPost.CreateDate - DateTime.Now).Seconds < 5, "CreateDate is not proper");
        }

        #endregion

        #region AddPost_when_adding_a_post_the_userid_should_be_set_to_one
        [TestMethod]
        public void AddPost_when_adding_a_post_the_userid_should_be_set_to_one()
        {
            //Arrange 
            SetupMocks();

            Post post = CreateNewPost();
            post.Id = 100;
            post.AddTag(new Tag { Id = 0, TagName = "misc" });

            //Act
            service.AddPost(post);

            //Assert
            Assert.IsNotNull(_AddedPost);
            Assert.AreEqual(1, _AddedPost.UserId);
        }

        #endregion

        #region AddPost_when_adding_a_post_with_a_future_scheduled_date_should_keep_the_date
        [TestMethod]
        public void AddPost_when_adding_a_post_with_a_future_scheduled_date_should_keep_the_date()
        {
            //Arrange 
            SetupMocks();

            DateTime futureDate = DateTime.Now.AddDays(1);

            Post post = CreateNewPost();
            post.Id = 100;
            post.ScheduledDate = futureDate;
            post.AddTag(new Tag { Id = 0, TagName = "misc" });

            //Act
            service.AddPost(post);

            //Assert
            Assert.IsNotNull(_AddedPost);
            Assert.AreEqual(futureDate, _AddedPost.ScheduledDate);
        }

        #endregion

        #region AddPost_when_adding_a_post_with_a_past_scheduled_date_should_change_it_to_current_date
        [TestMethod]
        public void AddPost_when_adding_a_post_with_a_past_scheduled_date_should_change_it_to_current_date()
        {
            //Arrange 
            SetupMocks();

            DateTime pastDate = DateTime.Now.AddDays(-1);
            Post post = CreateNewPost();
            post.Id = 100;
            post.AddTag(new Tag { Id = 0, TagName = "misc" });

            //Act
            service.AddPost(post);

            //Assert
            Assert.IsNotNull(_AddedPost);
            Assert.IsTrue((_AddedPost.ScheduledDate - DateTime.Now).Seconds < 5);
        }

        #endregion

        #endregion

        #region GetPostById

        [TestMethod()]
        public void GetPostById_with_id_should_call_repository_with_proper_id()
        {
            //Arrange
            SetupMocks();

            //Act
            var result = service.GetPostById(1001);

            //Assert
            postRepositoryMock.Verify(postrepo => postrepo.GetPostById(1001), Times.Exactly(1));
        }

        #endregion GetPostById

    }
}
