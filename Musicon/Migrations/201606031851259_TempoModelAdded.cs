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
            
            AlterColumn("dbo.Songs", "Length", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Songs", "Length", c => c.String());
            DropTable("dbo.Tempi");
        }
    }
}
