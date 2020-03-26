using Citatnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    interface ICitataRepository
    {
        public void AddCitata(Citata citata);
        public Citata GetCitata(int id);

    }
}
