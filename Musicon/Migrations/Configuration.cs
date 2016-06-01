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

            context.Songs.AddOrUpdate(i => i.Title,
            new Song
            {
                Title = "Wasting Time",               // Data Example
                Artist = "Fade2Blue",
                Composer = "Fade2Blue",
                Key = "A",
                Tempo = "Moderate",
                Length = "",
                Status = "Active",
                Vocal = "Brian",
                EntryDate = DateTime.Now

            },
            new Song
            {
                Title = "Bend In The River",
                Artist = "Fade2Blue",
                Composer = "Marty Robins",
                Key = "C",
                Tempo = "Moderate",
                Length = "",
                Status = "Active",
                Vocal = "Brian",
                EntryDate = DateTime.Now
            },
            new Song
            {
                Title = "The Eyes Have It",
                Artist = "Fade2Blue",
                Composer = "Joe Alles",
                Key = "A",
                Tempo = "Slow",
                Length = "",
                Status = "Active",
                Vocal = "Joe",
                EntryDate = DateTime.Now
            });
        }
    }
}