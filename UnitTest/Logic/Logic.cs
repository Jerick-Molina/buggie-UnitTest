
using Buggie.DataProperties;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuggieUnit.UnitTest.DatabaseMock;

namespace BuggieUnit.UnitTest.Logic {

    public class Logic  {

        DatabaseMockDb mockDb = new DatabaseMockDb();
        AccountAuth acc = new AccountAuth();
        public async Task<List<User>> GetAllUsers(){
            return mockDb.Database;
        }
        public  async Task<User> DoesUserExist(User user)
            {
                bool doesUserExist = false;
                foreach(var u in mockDb.Database)
                {
                    if(u.Email == user.Email )
                    {
                        doesUserExist = true;      
                    }
                }
                
                if(doesUserExist == false)
                {       
                
                    return new User();
                }

                    return user;
            }

        public async Task<string> CreateToken(User user)
            {
                var token = acc.GenerateJwtAccessToken(user);
                return token;
            }
        public async Task<string> Expected_AccountCreation(User user)
            {
                bool doesUserExist = false;
                foreach(var u in mockDb.Database)
                {
                    if(u.Email == user.Email)
                    {
                        doesUserExist = true;
                        return "Error";
                    }
                }

                if(doesUserExist == false)
                {      
                    user.Password = acc.HashPassword(user.Password);
                    var token = acc.GenerateJwtAccessToken(user);
                    return token;
                }

                return "Error" ;
            }


        public async Task<string> Expected_SignIn(User user)
        {
            bool isUserValid = false;
                foreach(var u in mockDb.Database)
                {
                    if(u.Email == user.Email)
                    {
                        if(u.Password == user.Password)
                        {
                             isUserValid = true;
                        }
                        return "";
                    }
                }
                if(isUserValid == true)
                {       
                    user.Password = acc.HashPassword(user.Password);
                    var token = acc.GenerateJwtAccessToken(user);
                    return token;
                }

                return "" ; 
        }
    



        public string GetJwtSplit(string jwt)
        {
             string[] jwtSplit = jwt.Split('.');
          

             return  jwtSplit[0] + "." + jwtSplit[1];

            
        }
    }
}