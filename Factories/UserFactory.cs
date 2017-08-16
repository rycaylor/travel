using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using travel.Models;
using System.Linq;
 

namespace travel.Factories
{
    public class UserFactory
    {
        static string server = "localhost";
        static string db = "travel"; //Change to your schema name
        static string port = "8889"; //Potentially 8889
        static string user = "root";
        static string pass = "root";
        internal static IDbConnection Connection {
            get {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }
    
        public static void CreateUser(User newUser)
        {
        
            using(IDbConnection dbConnection = Connection)
            {
                string query = $"INSERT INTO users (FirstName, LastName, Password, UserName, CreatedAt, Updated) VALUES (@FirstName, @LastName, @Password, @UserName, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        
        }
        public static List<User> TestUser(User testUser)
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

        public static List<User> LoginUser(UserTest test)
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