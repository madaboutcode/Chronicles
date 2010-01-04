using System;
using Chronicles.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronicles.DataAccess.Facade;
using Chronicles.Framework;
using Chronicles.Entities;
using Moq;
using StructureMap.AutoMocking;

namespace Chronicles.Tests.Services
{
    /// <summary>
    ///This is a test class for CommentServicesTest and is intended
    ///to contain all CommentServicesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CommentServicesTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        #region Test Helpers

        private Mock<ICommentRepository> commentRepositoryMock;
        private Mock<IUserRepository> userRepository;
        private CommentServices service;
        private Mock<IAppConfigProvider> appConfigProviderMock;
        private Mock<UserServices> userServicesMock;
        private User user;

        private Comment _AddedComment = null;

        [TestInitialize]
        public void TestInitialize()
        {
            SetupMocks();
        }

        private void SetupMocks()
        {
            var serviceMock = new MoqAutoMocker<CommentServices>();

            commentRepositoryMock = Mock.Get(serviceMock.Get<ICommentRepository>());
            appConfigProviderMock = Mock.Get(serviceMock.Get<IAppConfigProvider>());
            userRepository = Mock.Get(serviceMock.Get<IUserRepository>());
            userServicesMock = Mock.Get(serviceMock.Get<UserServices>());

            SetupUserRepositoryMocks();
            SetupCommentRepositoryMocks();
            SetupUserServiceMocks();
            SetupAppConfigMocks();

            service = serviceMock.ClassUnderTest;

            _AddedComment = null;
            user = new User{DateCreated = DateTime.Now, Email = "a@b.com", Name = "testuser"};
        }

        private void SetupUserRepositoryMocks()
        {
            userRepository
                .Setup(x => x.GetUserByEmail(It.IsAny<string>()))
                .Returns((string x) => new User { Id = 5, Email = x });
        }

        private void SetupUserServiceMocks()
        {
            userServicesMock.Setup(x => x.GetNewOrExistingUser(user)).Returns(user);
        }

        private void SetupAppConfigMocks()
        {
            //throw new NotImplementedException();
        }

        private void SetupCommentRepositoryMocks()
        {
            commentRepositoryMock
                .Setup(x => x.AddComment(It.IsAny<Comment>()))
                .Returns<Comment>(y =>
                                      {
                                          _AddedComment = y;
                                          return y;
                                      });
        }

        private Comment GetNewComment()
        {
            Comment comment = new Comment();
            comment.Approved = false;
            comment.Date = DateTime.Now;
            comment.Id = 0;
            comment.Post = new Post{Id=6};
            comment.Text = "New test comment";
            comment.AddUser(user);
            return comment;
        }

        #endregion Test Helpers

        #region AddComment Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddComment_when_comment_is_null_it_should_throw_ArgumentNullException()
        {
            //Arrange
            Comment comment = null;

            //Act
            service.AddComment(comment);

            //Assert
            commentRepositoryMock.Verify(x=>x.AddComment(It.IsAny<Comment>()), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddComment_when_comment_has_post_as_null_it_should_throw_ArgumentException()
        {
            //Arrange
            Comment comment = new Comment();
            comment.Post = null;
            
            //Act
            service.AddComment(comment);

            //Assert
            commentRepositoryMock.Verify(x=>x.AddComment(comment), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddComment_when_comment_has_user_as_null_it_should_throw_ArgumentException_()
        {
            //Arrange
            Comment comment = new Comment();
            comment.User = null;

            //Act
            service.AddComment(comment);

            //Assert
            commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Never());
        }

        [TestMethod]
        public void AddComment_when_comment_has_user_it_should_fetch_proper_user_from_the_user_service()
        {
            //Arrange
            Comment comment = GetNewComment();

            //Act
            service.AddComment(comment);

            //Assert
            commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Once());
            userServicesMock.Verify(x=>x.GetNewOrExistingUser(user), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddComment_when_comment_has_post_with_id_0_it_should_throw_ArgumentException()
        {
            //Arrange
            Comment comment = GetNewComment();
            comment.Post.Id = 0;

            //Act
            service.AddComment(comment);

            //Assert
            commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Never());
        }

        [TestMethod]
        public void AddComment_when_comment_is_added_the_date_field_should_be_the_current_date_and_time()
        {
            //Arrange
            Comment comment = GetNewComment();
            comment.Date = DateTime.Now.AddDays(3);

            //Act
            service.AddComment(comment);

            //Assert
            commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Once());
            Assert.IsTrue((_AddedComment.Date - DateTime.Now).Seconds < 5);
        }

        [TestMethod]
        public void AddComment_when_comment_is_added_the_approved_field_should_be_false()
        {
            //Arrange
            Comment comment = GetNewComment();
            comment.Approved = false;

            //Act
            service.AddComment(comment);

            //Assert
            commentRepositoryMock.Verify(x => x.AddComment(comment), Times.Once());
            Assert.AreEqual(false, _AddedComment.Approved);
        }

        #endregion 
    }
}