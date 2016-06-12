namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateFormed = c.DateTime(nullable: false),
                        Style = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Groups");
        }
    }
}
