using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    public class DataBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + "\\DataBase.sqlite"; }
        }

        public static SqliteConnection DbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }

        protected static void CreateDataBase()
        {
            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = dbConnection;

                command.CommandText = "CREATE TABLE IF NOT EXISTS Users (Login TEXT PRIMARY KEY, Password TEXT);";

                try
                {
                    command.ExecuteNonQuery();
                }
                catch(SqliteException exeption)
                {
                    Console.WriteLine("Error when creating database:" + exeption.Message);
                }
            }
        }
    }
}
