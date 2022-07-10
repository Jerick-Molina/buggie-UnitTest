using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Buggie.DataProperties;
using Buggie.Interface;
using Buggie.Logic;
using BuggieUnit.UnitTest.Logic;
using Xunit;
using BuggieUnit.UnitTest.DatabaseMock;
namespace BuggieUnit
{
    public class Account_Test
    {   
        DatabaseMockDb mockDb = new DatabaseMockDb();
        DatabaseMockDb mockDb2 = new DatabaseMockDb();
            Logic logic = new Logic();
            AccountAuthentication acc = new AccountAuthentication();
        //TEST: 
        //1. Make sure sql is the same
        //2. If user exist account shouldnt be inserted into "db"
        //3. User account inside the "db"
        [Fact]
        public  async Task  Account_Creation_Validation_Valid()
        {   
            
            //Using the unique 
            using (var mock = AutoMock.GetLoose())
            {
              var sql = "insert into Users(FirstName,LastName,Email,Password,Role) values (@FirstName,@LastName,@Email,@Password,@Role)";
              mock.Mock<IMySqlDataAccess>()
                .Setup(x => x.SaveData<User>(sql,mockDb.uniqeUser));

              mock.Mock<IUserAccess>()
                .Setup(x => x.FindUser(mockDb.uniqeUser))
                .Returns(logic.DoesUserExist(mockDb.uniqeUser));

              mock.Mock<IAccountAuthentication>()
                .Setup(x => x.GenerateJwtAccessToken(mockDb.uniqeUser))
                .Returns(acc.GenerateJwtAccessToken(mockDb.uniqeUser));
        
             var cls =  mock.Create<AccountAccess>();
             string  actual =  cls.AccountCreate(mockDb.uniqeUser).Result;
             string expected =  logic.Expected_AccountCreation(mockDb2.uniqeUser).Result;


              // since its encoded the last value will always be different, therefore we only get the two values
             actual = logic.GetJwtSplit(actual);
             expected = logic.GetJwtSplit(expected);

            //Both jwt should not be empty
                Assert.Equal(actual,expected);
            }

            //Using the sameUser
            using (var mock = AutoMock.GetLoose())
            {
              var sql = "insert into Users(FirstName,LastName,Email,Password,Role) values (@FirstName,@LastName,@Email,@Password,@Role)";
              mock.Mock<IMySqlDataAccess>()
                 .Setup(x => x.SaveData<User>(sql, mockDb.sameUser));

              mock.Mock<IUserAccess>()
                 .Setup(x => x.FindUser(mockDb.sameUser))
                 .Returns(logic.DoesUserExist(mockDb.sameUser));

              mock.Mock<IAccountAuthentication>()
                 .Setup(x => x.GenerateJwtAccessToken(mockDb.sameUser))
                 .Returns(logic.CreateToken(mockDb.sameUser));
             
             var cls =  mock.Create<AccountAccess>();
             string  actual =  cls.AccountCreate(mockDb.sameUser).Result;
             string expected =  logic.Expected_AccountCreation(mockDb.sameUser).Result;

            //Both jwt should be empty
                Assert.Equal(actual,expected);
            }


        }

         [Fact]
        public  async Task  Account_Creation_Validation_Empty()
        {   
            //Using the sameUser
            using (var mock = AutoMock.GetLoose())
            {
              var sql = "insert into Users(FirstName,LastName,Email,Password,Role) values (@FirstName,@LastName,@Email,@Password,@Role)";
              mock.Mock<IMySqlDataAccess>()
                 .Setup(x => x.SaveData<User>(sql, mockDb.sameUser));

              mock.Mock<IUserAccess>()
                 .Setup(x => x.FindUser(mockDb.sameUser))
                 .Returns(logic.DoesUserExist(mockDb.sameUser));

              mock.Mock<IAccountAuthentication>()
                 .Setup(x => x.GenerateJwtAccessToken(mockDb.sameUser))
                 .Returns(logic.CreateToken(mockDb.sameUser));
             
             var cls =  mock.Create<AccountAccess>();
             string  actual =  cls.AccountCreate(mockDb.sameUser).Result;
             string expected =  logic.Expected_AccountCreation(mockDb.sameUser).Result;

            //Both jwt should be empty
                Assert.Equal(actual,expected);
            }


        }

        [Fact]
        public async Task  Account_SignIn_Validation()
        {
          
            //Using the unique 
            using (var mock = AutoMock.GetLoose())
            {
            
              mock.Mock<IUserAccess>()
                .Setup(x => x.FindUser(mockDb.sameUser))
                .Returns(logic.DoesUserExist(mockDb.sameUser));
               mock.Mock<IAccountAuthentication>()
                .Setup(x => x.HashPassword(mockDb.sameUser.Password))
                .Returns(acc.HashPassword(mockDb.sameUser.Password));
              mock.Mock<IAccountAuthentication>()
                .Setup(x => x.GenerateJwtAccessToken(mockDb.sameUser))
                .Returns(logic.CreateToken(mockDb.sameUser));
        
             var cls =  mock.Create<AccountAccess>();
             string  actual =  cls.AccountCreate(mockDb.sameUser).Result;
             string expected =  logic.Expected_SignIn(mockDb.sameUser).Result;
            //Both jwt should not be empty
                Assert.Equal(actual,expected);
            }
        }

    }
}