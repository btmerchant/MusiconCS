﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicon.Models
{
    public class Song
    {
        public int SongId { get; set; }

        [MaxLength(500)]
        [Required]
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Composer { get; set; }

        public string Key { get; set; }

        public string Tempo { get; set; }

        public double Length { get; set; }

        public string Status { get; set; }

        public string Vocal { get; set; }

        public DateTime EntryDate { get; set; }
    }
}