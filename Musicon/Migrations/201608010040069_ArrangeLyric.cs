namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArrangeLyric : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Arrangement", c => c.String());
            AddColumn("dbo.Songs", "Lyric", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "Lyric");
            DropColumn("dbo.Songs", "Arrangement");
        }
    }
}
