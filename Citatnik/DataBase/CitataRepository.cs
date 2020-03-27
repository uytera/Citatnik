using Citatnik.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    public class CitataRepository : DataBaseRepository, IRepository<Citata>
    {
        private List<Citata> list = new List<Citata>();
        public int lastId;

        public CitataRepository()
        {
            CreateDataBase();
            UploadCitatsFromDataBase(list);
            lastId = list.Count != 0 ? list.Last().CitataId + 1 : 1;
        }

        public void Insert(Citata instanceT)
        {
            ExecuteSqlCommand(@"INSERT INTO Citats (CitataId, Title, Content, CreationDate)
                                    VALUES ('" + instanceT.CitataId + "','"
                                               + instanceT.Title + "','"
                                               + instanceT.Content + "','"
                                               + instanceT.CreationDate + "')");
            list.Add(instanceT);
            lastId += 1;
        }

        public void Update(Citata instanceT)
        {
            ExecuteSqlCommand(@"UPDATE Citats SET 
                                    Title = '" + instanceT.Title + "', " +
                                    "Content = '" + instanceT.Content + "' " +
                               "WHERE CitataId = " + instanceT.CitataId + ";");
            Citata oldCitata = list.Find(citata => citata.CitataId == instanceT.CitataId);
            oldCitata = instanceT;
        }


        public void Delete(int id)
        {
            ExecuteSqlCommand("DELETE FROM Citats WHERE CitataId = " + id + ";");
            list.RemoveAll(citata => citata.CitataId == id);
        }


        public Citata Select(object id)
        {
            int tempId = (int)id;
            return list.Find(citata => citata.CitataId == tempId);
        }

        private static void UploadCitatsFromDataBase(List<Citata> list)
        {
            using (SqliteConnection dbConnection = DbConnection())
            {
                dbConnection.Open();
                SqliteCommand command = new SqliteCommand
                {
                    Connection = dbConnection,
                    CommandText = "SELECT * FROM Citats"
                };

                try
                {
                    SqliteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Citata citata = new Citata(reader.GetInt32(0),
                                                   reader.GetString(1),
                                                   reader.GetString(2),
                                                   reader.GetString(3));
                        list.Add(citata);
                    }

                    list.Sort((citata1, citata2) => citata1.CitataId.CompareTo(citata2.CitataId));
                }
                catch (SqliteException exeption)
                {
                    Console.WriteLine("Error when executing custom command:" + exeption.Message);
                }
            }
        }
    }
}
