using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using travel.Models;
using System.Linq;
 

namespace travel.Factories
{
    public class UserFactory
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
 
        public UserFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }
        public void CreateUser(User newUser)
        {
        
            using(IDbConnection dbConnection = Connection)
            {
                string query = $"INSERT INTO users (FirstName, LastName, Password, UserName, CreatedAt, Updated) VALUES (@FirstName, @LastName, @Password, @UserName, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        
        }
        public List<User> TestUser(User testUser)
        {
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = $"SELECT * FROM users WHERE (UserName = UserName)";
                    dbConnection.Open();
                    return dbConnection.Query<User>(query).ToList();
                }
            }

        }

        public List<User> LoginUser(UserTest test)
        {
            using(IDbConnection dbConnection = Connection)
            {
                using(IDbCommand command = dbConnection.CreateCommand())
                {
                    string query = $"SELECT * FROM users WHERE (UserName = (@UserName && Password = @Password)";
                    dbConnection.Open();
                    return dbConnection.Query<User>(query).ToList();
                }
            }

        }



    }

}