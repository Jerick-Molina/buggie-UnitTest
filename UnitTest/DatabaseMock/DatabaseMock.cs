
using Buggie.DataProperties;
using System.Collections.Generic;

namespace BuggieUnit.UnitTest.DatabaseMock {

    public class DatabaseMockDb  {

        public List<User> Database = new List<User>()
        {
                    new User
                {
                    FirstName = "Squidward",
                    LastName = "Tentacles",
                    Email = "SquidTent@gmail.com",
                    Password = "Test3!!",
                    Role = "Admin"
                },
                new User
                {
                    FirstName = "Patrick",
                    LastName = "Star",
                    Email = "PatStar@gmail.com",
                    Password = "Test1!!",
                    Role = "Admin"
                },
                new User
                {
                    FirstName = "SpongeBob",
                    LastName = "SquarePants",
                    Email = "SpongeSquare@gmail.com",
                    Password = "Test2!!",
                    Role = "Admin"
                }
        };

        public User uniqeUser = new User() 
            {
                FirstName = "Eugene",
                LastName = "Krabs",
                Email = "EugeneKrabs@gmail.com",
                Password = "Test1-!!",
                Role = "Admin"
            };
        public User sameUser = new User() 
            {
                FirstName = "Squidward",
                LastName = "Tentacles",
                Email = "SquidTent@gmail.com",
                Password = "Test7!!",
                Role = "Admin"
            };
        
    }
}