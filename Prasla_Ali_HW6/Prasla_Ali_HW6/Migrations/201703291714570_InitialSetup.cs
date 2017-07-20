namespace Prasla_Ali_HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Committees",
                c => new
                    {
                        CommitteeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CommitteeID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Location = c.String(nullable: false),
                        MembersOnly = c.Boolean(nullable: false),
                        Committee_CommitteeID = c.Int(),
                        Member_MemberID = c.Int(),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeID)
                .ForeignKey("dbo.Members", t => t.Member_MemberID)
                .Index(t => t.Committee_CommitteeID)
                .Index(t => t.Member_MemberID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        OkToText = c.Boolean(nullable: false),
                        Major = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberID);
            
            CreateTable(
                "dbo.MemberCommittees",
                c => new
                    {
                        Member_MemberID = c.Int(nullable: false),
                        Committee_CommitteeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_MemberID, t.Committee_CommitteeID })
                .ForeignKey("dbo.Members", t => t.Member_MemberID, cascadeDelete: true)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeID, cascadeDelete: true)
                .Index(t => t.Member_MemberID)
                .Index(t => t.Committee_CommitteeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Member_MemberID", "dbo.Members");
            DropForeignKey("dbo.MemberCommittees", "Committee_CommitteeID", "dbo.Committees");
            DropForeignKey("dbo.MemberCommittees", "Member_MemberID", "dbo.Members");
            DropForeignKey("dbo.Events", "Committee_CommitteeID", "dbo.Committees");
            DropIndex("dbo.MemberCommittees", new[] { "Committee_CommitteeID" });
            DropIndex("dbo.MemberCommittees", new[] { "Member_MemberID" });
            DropIndex("dbo.Events", new[] { "Member_MemberID" });
            DropIndex("dbo.Events", new[] { "Committee_CommitteeID" });
            DropTable("dbo.MemberCommittees");
            DropTable("dbo.Members");
            DropTable("dbo.Events");
            DropTable("dbo.Committees");
        }
    }
}
