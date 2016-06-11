namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSongMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Member_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Songs", "Member_Id");
            AddForeignKey("dbo.Songs", "Member_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "Member_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Songs", new[] { "Member_Id" });
            DropColumn("dbo.Songs", "Member_Id");
        }
    }
}
