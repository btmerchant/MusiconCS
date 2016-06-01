namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempoModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tempi",
                c => new
                    {
                        TempoId = c.Int(nullable: false, identity: true),
                        TempoType = c.String(),
                    })
                .PrimaryKey(t => t.TempoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tempi");
        }
    }
}
