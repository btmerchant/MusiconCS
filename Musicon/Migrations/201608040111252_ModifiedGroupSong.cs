namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedGroupSong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupSongs", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupSongs", "Song_SongId", "dbo.Songs");
            DropIndex("dbo.GroupSongs", new[] { "Group_GroupId" });
            DropIndex("dbo.GroupSongs", new[] { "Song_SongId" });
            AddColumn("dbo.GroupSongs", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.GroupSongs", "Title", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.GroupSongs", "Artist", c => c.String());
            AddColumn("dbo.GroupSongs", "Composer", c => c.String());
            AddColumn("dbo.GroupSongs", "Key", c => c.String());
            AddColumn("dbo.GroupSongs", "Tempo", c => c.String());
            AddColumn("dbo.GroupSongs", "Length", c => c.Double(nullable: false));
            AddColumn("dbo.GroupSongs", "Status", c => c.String());
            AddColumn("dbo.GroupSongs", "Vocal", c => c.String());
            AddColumn("dbo.GroupSongs", "EntryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GroupSongs", "Genre", c => c.String());
            AddColumn("dbo.GroupSongs", "Arrangement", c => c.String());
            AddColumn("dbo.GroupSongs", "Lyric", c => c.String());
            DropColumn("dbo.GroupSongs", "Group_GroupId");
            DropColumn("dbo.GroupSongs", "Song_SongId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupSongs", "Song_SongId", c => c.Int(nullable: false));
            AddColumn("dbo.GroupSongs", "Group_GroupId", c => c.Int(nullable: false));
            DropColumn("dbo.GroupSongs", "Lyric");
            DropColumn("dbo.GroupSongs", "Arrangement");
            DropColumn("dbo.GroupSongs", "Genre");
            DropColumn("dbo.GroupSongs", "EntryDate");
            DropColumn("dbo.GroupSongs", "Vocal");
            DropColumn("dbo.GroupSongs", "Status");
            DropColumn("dbo.GroupSongs", "Length");
            DropColumn("dbo.GroupSongs", "Tempo");
            DropColumn("dbo.GroupSongs", "Key");
            DropColumn("dbo.GroupSongs", "Composer");
            DropColumn("dbo.GroupSongs", "Artist");
            DropColumn("dbo.GroupSongs", "Title");
            DropColumn("dbo.GroupSongs", "GroupId");
            CreateIndex("dbo.GroupSongs", "Song_SongId");
            CreateIndex("dbo.GroupSongs", "Group_GroupId");
            AddForeignKey("dbo.GroupSongs", "Song_SongId", "dbo.Songs", "SongId", cascadeDelete: true);
            AddForeignKey("dbo.GroupSongs", "Group_GroupId", "dbo.Groups", "GroupId", cascadeDelete: true);
        }
    }
}
