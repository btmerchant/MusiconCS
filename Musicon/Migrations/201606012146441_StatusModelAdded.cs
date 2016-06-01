namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusType = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Status");
        }
    }
}
