using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.DataBase
{
    interface IRepository<T>
    {
        public T Select(object id);
        public void Insert(T instanceT);
        public void Update(T instanceT);
        public void Delete(int id);
    }
}
