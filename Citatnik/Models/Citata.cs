﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citatnik.Models
{
    public class Citata
    {
        public int CitataId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreationDate { get; set; }


        public Citata(int citataId, string title, string content, string creationDate)
        {
            CitataId = citataId;
            Title = title;
            Content = content;
            CreationDate = creationDate;
        }
    }
}
