using Citatnik.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    public class UserRepository : DataBaseRepository, IRepository<User>
    {
        private static UserRepository instance;

        public static UserRepository getInstance()
        {
            if (instance == null)
            {
                CreateDataBase();
                instance = new UserRepository();
            }

            return instance;
        }


        public void Insert(User instanceT)
        {
            ExecuteSqlCommand(@"INSERT INTO Users (Login, Password, CitataIds)
                                    VALUES ('" 
                                            + instanceT.Login + "','"
                                            + instanceT.Password + "','"
                                            + String.Join(" ", instanceT.CitataIds) + "')");
        }


        public void Update(User instanceT)
        {
            ExecuteSqlCommand(@"UPDATE Users SET 
                                    Password = '" + instanceT.Password + "', " +
                                    "CitataIds = '" + String.Join(" ", instanceT.CitataIds) + "' " +
                               "WHERE Login = " + instanceT.Login + ";");
        }


        public void Delete(int id)
        {
            ExecuteSqlCommand("DELETE FROM Users WHERE Login = " + id + ";");
        }


        public User Select(object id)
        {
            string tempId = (string)id;

            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();

                SqliteCommand command = new SqliteCommand
                {
                    Connection = dbConnection,
                    CommandText = "SELECT * FROM Users WHERE Login = '" + tempId + "'"
                };

                try
                {
                    SqliteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        User user;

                        if (reader.GetString(2).Equals(""))
                        {
                            user = new User(reader.GetString(0),
                                            reader.GetString(1),
                                            new List<int>());
                        }
                        else
                        {
                            user = new User(reader.GetString(0),
                                            reader.GetString(1),
                                            reader.GetString(2).Split(" ").Select(n => Convert.ToInt32(n)).ToList());
                        }

                        return user;
                    }
                }
                catch (SqliteException exeption)
                {
                    Console.WriteLine("Error when getting the user from the database:" + exeption.Message);
                }

                return null;
            }
        }


        private UserRepository() { }
    }
}
