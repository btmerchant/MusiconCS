namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SongGenreAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Genre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Genre");
        }
    }
}
