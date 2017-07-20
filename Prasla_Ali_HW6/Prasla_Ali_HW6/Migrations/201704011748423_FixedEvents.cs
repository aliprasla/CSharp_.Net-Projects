namespace Prasla_Ali_HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedEvents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Member_MemberID", "dbo.Members");
            DropIndex("dbo.Events", new[] { "Member_MemberID" });
            CreateTable(
                "dbo.MemberEvents",
                c => new
                    {
                        Member_MemberID = c.Int(nullable: false),
                        Event_EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_MemberID, t.Event_EventID })
                .ForeignKey("dbo.Members", t => t.Member_MemberID, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EventID, cascadeDelete: true)
                .Index(t => t.Member_MemberID)
                .Index(t => t.Event_EventID);
            
            DropColumn("dbo.Events", "Member_MemberID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Member_MemberID", c => c.Int());
            DropForeignKey("dbo.MemberEvents", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.MemberEvents", "Member_MemberID", "dbo.Members");
            DropIndex("dbo.MemberEvents", new[] { "Event_EventID" });
            DropIndex("dbo.MemberEvents", new[] { "Member_MemberID" });
            DropTable("dbo.MemberEvents");
            CreateIndex("dbo.Events", "Member_MemberID");
            AddForeignKey("dbo.Events", "Member_MemberID", "dbo.Members", "MemberID");
        }
    }
}
