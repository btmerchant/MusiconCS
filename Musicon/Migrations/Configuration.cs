namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Musicon.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Musicon.DAL.MusiconContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Musicon.DAL.MusiconContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Songs.AddOrUpdate(i => i.Title,
            //    new Song
            //    {
            //        Title = "Wasting Time",               // Data Example
            //        Artist = "Fade2Blue",
            //        Composer = "Fade2Blue",
            //        Key = "A",
            //        Tempo = "Moderate",
            //        Length = 3.0,
            //        Status = "Active",
            //        Vocal = "Brian",
            //        EntryDate = DateTime.Now,
            //        Genre = "Bluebrass"

            //    },
            //    new Song
            //    {
            //        Title = "Bend In The River",
            //        Artist = "Fade2Blue",
            //        Composer = "Marty Robins",
            //        Key = "C",
            //        Tempo = "Moderate",
            //        Length = 3.0,
            //        Status = "Active",
            //        Vocal = "Brian",
            //        EntryDate = DateTime.Now,
            //        Genre = "Bluebrass"
            //    },
            //    new Song
            //    {
            //        Title = "The Eyes Have It",
            //        Artist = "Fade2Blue",
            //        Composer = "Joe Alles",
            //        Key = "A",
            //        Tempo = "Slow",
            //        Length = 3.0,
            //        Status = "Active",
            //        Vocal = "Joe",
            //        EntryDate = DateTime.Now,
            //        Genre = "Bluebrass"
            //    });

            //context.Statuses.AddOrUpdate(i => i.StatusId,
            //    new Status
            //    {
            //        StatusType = "Preliminary"               // Data Example
            //    },
            //    new Status
            //    {
            //        StatusType = "Active"
            //    },
            //    new Status
            //    {
            //        StatusType = "Closed"
            //    });

            //context.Tempos.AddOrUpdate(i => i.TempoId,
            //    new Tempo
            //    {
            //        TempoType = "Slow"               // Data Example
            //    },
            //    new Tempo
            //    {
            //        TempoType = "Moderate"
            //    },
            //    new Tempo
            //    {
            //        TempoType = "Fast"
            //    });

            //context.Groups.AddOrUpdate(i => i.GroupId,
            //    new Group
            //    {
            //        Name = "Fade2Blue",
            //        DateFormed = Convert.ToDateTime("03/01/2008"),
            //        Style = "Bluegrass"
            //    },
            //    new Group
            //    {
            //        Name = "The Miltons",
            //        DateFormed = Convert.ToDateTime("03/01/2000"),
            //        Style = "Western Swing"
            //    },
            //     new Group
            //     {
            //         Name = "Shelby Bottoms String Band",
            //         DateFormed = Convert.ToDateTime("03/01/2001"),
            //         Style = "Folk"
            //     },
            //    new Group
            //    {
            //        Name = "The Lonsome Pine Ramblers",
            //        DateFormed = Convert.ToDateTime("03/01/2002"),
            //        Style = "Mixed"
            //    });
        }
    }
}
