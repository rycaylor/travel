using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using travel.Models;
using System.Linq; 

namespace travel.Factories
{
    public class TripFactory
    {
        private readonly IOptions<MySqlOptions> MySqlConfig;
 
        public TripFactory(IOptions<MySqlOptions> config)
        {
            MySqlConfig = config;
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }
        public void CreateTrip(Trip newTrip)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trips (Title, Description, LeaveDate, ReturnDate, UserId, CreatedAt, UpdatedAt) " +
                                @"VALUES (@Title, @Description, @LeaveDate, @ReturnDate, @UserId, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, newTrip);
            }
        }
        public Trip GetTripById(int id){
            using (IDbConnection dbConnection = Connection){
                string query = $"SELECT * FROM trips WHERE (id={id})";
                dbConnection.Open();
                return dbConnection.Query<Trip>(query).SingleOrDefault();
            }
        }
        public List<Trip> GetAllTrips(){
            using (IDbConnection dbConnection = Connection){
                string query = "SELECT * FROM trips JOIN users ON trips.UserId = users.id";
                dbConnection.Open();
                var trips = dbConnection.Query<Trip>(query).ToList();

                foreach (var trip in trips){
                    query = $"SELECT FirstName, LastName FROM users WHERE (id = {trip.UserId})";
                    User user = dbConnection.Query<User>(query).SingleOrDefault();
                    trip.User = user;
                }
                return trips;
            }
        }
    }

}