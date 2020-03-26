﻿using Citatnik.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    public class UserRepository : DataBaseRepository, IUserRepository
    {
        public void AddUser(User user)
        {
            CreateDataBase();

            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();

                SqliteCommand command = new SqliteCommand
                {
                    Connection = dbConnection,
                    CommandText = @"INSERT INTO Users (Login, Password, CitataIds)
                                    VALUES ('" + user.Login + "','" + user.Password + "','" + String.Join(" ", user.CitataIds) + "')"
                };

                try
                {
                    command.ExecuteNonQuery();
                }
                catch(SqliteException exeption)
                {
                    Console.WriteLine("Error when adding a user to the database:" + exeption.Message);
                }
            }
        }


        public User GetUser(string login)
        {
            CreateDataBase();

            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();

                SqliteCommand command = new SqliteCommand
                {
                    Connection = dbConnection,
                    CommandText = "SELECT * FROM Users WHERE Login = '" + login + "'"
                };

                try
                {
                    SqliteDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        User user = new User(reader.GetString(0),
                                             reader.GetString(1),
                                             reader.GetString(2).Split(" ").Select(n => Convert.ToInt32(n)).ToArray());

                        return user;
                    }
                }
                catch(SqliteException exeption)
                {
                    Console.WriteLine("Error when getting the user from the database:" + exeption.Message);
                }

                return null;
            }
        }
    }
}
