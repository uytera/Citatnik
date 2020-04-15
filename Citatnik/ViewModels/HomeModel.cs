using Citatnik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.ViewModels
{
    public class HomeModel
    {
        public List<Citata> Citatas { get; set; } = new List<Citata>();
    }
}
