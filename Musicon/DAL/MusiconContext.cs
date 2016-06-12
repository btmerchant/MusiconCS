using System;
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
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Tempo> Tempos { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
    }
}