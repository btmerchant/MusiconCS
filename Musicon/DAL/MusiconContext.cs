﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Musicon.Models;

namespace Musicon.DAL
{

    public class MusiconContext : ApplicationDbContext
    {
        public virtual DbSet<Song> Songs { get; set; }
    }
}