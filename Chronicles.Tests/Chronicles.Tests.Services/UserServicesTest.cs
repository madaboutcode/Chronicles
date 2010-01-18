using System;
using Chronicles.DataAccess.Facade;
using Chronicles.Framework;
using Chronicles.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronicles.Entities;
using Moq;
using StructureMap.AutoMocking;

namespace Chronicles.Tests.Services
{


    /// <summary>
    ///This is a test class for UserServicesTest and is intended
    ///to contain all UserServicesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserServicesTest
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

        #region Test helpers , mocks

        private UserServices service = null;
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<IAppConfigProvider> appConfigProviderMock;

        private User existingUser;

        [TestInitialize]
        public void SetupTest()
        {
            var serviceMock = new MoqAutoMocker<UserServices>();

            userRepositoryMock = Mock.Get(serviceMock.Get<IUserRepository>());
            appConfigProviderMock = Mock.Get(serviceMock.Get<IAppConfigProvider>());

            existingUser = CreateNewUser();

            SetupUserRepository(userRepositoryMock);
            
            service = serviceMock.ClassUnderTest;

        }

        private User CreateNewUser()
        {
            return new User
            {
                Id = 10,
                Name = "testuser",
                Email = "existinguser@chronicles.com",
            };
        }

        private User CreateNewSystemUser()
        {
            User user = CreateNewUser();
            user.Salt = "zlstFnfBp0qGQ/L5xwtjuMfykoWGxJ+pYYu/Lqz9liE41SUNUzZE+g8GQkJSx7sUw2Zw";
            user.Hash = "Nyzp/MFIKxkBxCHiZGvwftV6e9Vqx+LlHZ9JVzbWbG4=";
            return user;
        }

        private void SetupUserRepository(Mock<IUserRepository> repositoryMock)
        {
            repositoryMock
                .Setup(x => x.GetUserByEmail(It.IsAny<string>()))
                .Returns((string x)=> x == "existinguser@chronicles.com" ? existingUser : null);
        }

        #endregion Test helpers , mocks


        #region GetNewOrExistingVisitor Tests

        [TestMethod]
        public void GetNewOrExistingUser_when_user_exists_it_should_return_the_existing_user()
        {
            //Arrange
            User user = new User { Email = "existinguser@chronicles.com" };

            //Act

            User ret = service.GetNewOrExistingVisitor(user);

            //Assert
            Assert.AreEqual(existingUser.Id, ret.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNewOrExistingUser_when_user_doesnot_have_an_email_it_should_throw_ArgumentException()
        {
            //Arrange
            User user = new User();

            //Act
            service.GetNewOrExistingVisitor(user);

            //Assert
        }

        [TestMethod]
        public void GetNewOrExistingUser_when_user_doesnot_exists_new_user_should_be_created_with_proper_fields()
        {
            //Arrange
            User user = new User
            {
                Email = "newuser@new.com",
                Name = "newuser",
                Id = 5,
                DateCreated = DateTime.Now.AddDays(-1),
                Role = UserRole.Reviewer
            };

            //Act
            User ret = service.GetNewOrExistingVisitor(user);

            //Assert
            Assert.AreEqual("newuser", ret.Name);
            Assert.AreEqual(0, ret.Id);
            Assert.AreEqual("newuser@new.com", ret.Email);
            Assert.AreEqual(UserRole.Visitor, ret.Role);
            Assert.IsTrue((ret.DateCreated - DateTime.Now).Seconds < 5);
        }

        [TestMethod]
        public void GetNewOrExistingUser_when_user_exists_and_name_is_different_then_it_should_return_existing_user_with_new_name()
        {
            //Arrange
            User user = CreateNewUser();
            user.Name = "Name changed";

            //Act
            User ret = service.GetNewOrExistingVisitor(user);

            //Assert
            Assert.AreEqual(existingUser.Id,ret.Id);
            Assert.AreEqual(user.Name, ret.Name);
        }

        #endregion GetNewOrExistingVisitor Tests


        #region AuthenticateUser Test

        [TestMethod]
        public void AuthenticateUser_Test()
        {
            //Arrange
            User user = CreateNewSystemUser();
            user.Role = UserRole.Admin;
            userRepositoryMock.Setup(x => x.GetUserByName("ajeesh"))
                .Returns(user);

            //Act
            bool ret = service.AuthenticateUser("ajeesh", "password");

            //Assert
            userRepositoryMock.VerifyAll();
            Assert.IsTrue(ret);
        }

        #endregion AuthenticateUser Test
    }
}
