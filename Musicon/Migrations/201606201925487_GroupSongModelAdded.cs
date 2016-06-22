namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupSongModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupSongs",
                c => new
                    {
                        GroupSongId = c.Int(nullable: false, identity: true),
                        Group_GroupId = c.Int(nullable: false),
                        Song_SongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupSongId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.Song_SongId, cascadeDelete: true)
                .Index(t => t.Group_GroupId)
                .Index(t => t.Song_SongId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupSongs", "Song_SongId", "dbo.Songs");
            DropForeignKey("dbo.GroupSongs", "Group_GroupId", "dbo.Groups");
            DropIndex("dbo.GroupSongs", new[] { "Song_SongId" });
            DropIndex("dbo.GroupSongs", new[] { "Group_GroupId" });
            DropTable("dbo.GroupSongs");
        }
    }
}
