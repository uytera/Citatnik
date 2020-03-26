using Citatnik.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    public class CitataRepository : DataBaseRepository, ICitataRepository
    {
        public int LastId;

        public void AddCitata(Citata citata)
        {
            CreateDataBase();

            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();

                SqliteCommand command = new SqliteCommand
                {
                    Connection = dbConnection,
                    CommandText = @"INSERT INTO Citats (Title, Content, CreationDate)
                                    VALUES ('" + citata.Title + "','" + citata.Content + "','" + citata.CreationDate + "')"
                };

                try
                {
                    command.ExecuteNonQuery();

                    LastId += 1;
                }
                catch (SqliteException exeption)
                {
                    Console.WriteLine("Error when adding a citata to the database:" + exeption.Message);
                }
            }
        }

        public Citata GetCitata(int id)
        {
            CreateDataBase();

            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();

                SqliteCommand command = new SqliteCommand
                {
                    Connection = dbConnection,
                    CommandText = "SELECT * FROM Citats WHERE CitataId = '" + id + "'"
                };

                try
                {
                    SqliteDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        Citata citata = new Citata(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

                        return citata;
                    }
                }
                catch (SqliteException exeption)
                {
                    Console.WriteLine("Error when getting the citata from the database:" + exeption.Message);
                }

                return null;
            }
        }
    }
}
