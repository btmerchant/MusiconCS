namespace Musicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupMemberModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupMemberId = c.Int(nullable: false, identity: true),
                        Group_GroupId = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupMemberId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Group_GroupId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupMembers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembers", "Group_GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "User_Id" });
            DropIndex("dbo.GroupMembers", new[] { "Group_GroupId" });
            DropTable("dbo.GroupMembers");
        }
    }
}
