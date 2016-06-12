using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musicon.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public DateTime DateFormed { get; set; }
        public string Style { get; set; }
    }
}