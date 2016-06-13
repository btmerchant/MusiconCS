using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Musicon.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateFormed { get; set; }

        public string Style { get; set; }
    }
}