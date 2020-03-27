using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    public abstract class DataBaseRepository
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
            ExecuteSqlCommand(@"CREATE TABLE IF NOT EXISTS Users (Login TEXT PRIMARY KEY, Password TEXT, CitataIds TEXT);
                                CREATE TABLE IF NOT EXISTS Citats (CitataId INTEGER PRIMARY KEY, Title TEXT, Content TEXT, CreationDate TEXT);");
        }


        protected static void ExecuteSqlCommand(string commandString)
        {
            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();
                SqliteCommand command = new SqliteCommand
                {
                    Connection = dbConnection,
                    CommandText = commandString
                };

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqliteException exeption)
                {
                    Console.WriteLine("Error when executing custom command:" + commandString + exeption.Message);
                }
            }
        }
    }
}
