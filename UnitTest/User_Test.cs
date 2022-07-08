using System;
using Xunit;
using Autofac;
using Autofac.Extras.Moq;
using Buggie.Interface;
using Buggie.DataProperties;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Moq;
using Moq.Protected;
using Buggie.Logic;

namespace BuggieUnit
{
    public class User_Test
    {
        [Fact]
        public  async Task  Get_Users_Validation()
        {   
           
            using (var mock = AutoMock.GetLoose())
            {
              var sql = "select * from test";
              mock.Mock<IMySqlDataAccess>()
             .Setup(x => x.LoadData<User,dynamic>(sql, ""))
             .Returns(GetUserSamples());

             var cls =  mock.Create<UserAccess>();
             List<User> actual =  cls.GetUsers().Result;


             List<User> expected =  GetUserSamples().Result;

           
            for(var i = 0; i < expected.Count;i++){

               Assert.Equal(expected[i].FirstName,actual[i].FirstName);
            }
         
              
            }
               
        
            
        }

        

        private async Task<List<User>> GetUserSamples()
        {
             List<User> output = new List<User>()
            {
                new User
                {
                    FirstName = "Squidward",
                    LastName = "Tentacles"
                },
                new User
                {
                    FirstName = "Patrick",
                    LastName = "Star"
                },
                new User
                {
                    FirstName = "SpongeBob",
                    LastName = "SquarePants"
                },
             
            };

         
            return  output.ToList();
        }
    }
}
