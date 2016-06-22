using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicon.Models
{
    public class GroupSong
    {
        public virtual int GroupSongId { get; set; }
        [Required]
        public virtual Group Group { get; set; }
        [Required]
        public virtual Song Song { get; set; }
    }
}