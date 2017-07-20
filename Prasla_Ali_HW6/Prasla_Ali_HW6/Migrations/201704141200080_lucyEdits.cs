namespace Prasla_Ali_HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lucyEdits : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EventAppUsers", newName: "AppUserEvents");
            DropForeignKey("dbo.AppUserCommittees", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserCommittees", "Committee_CommitteeID", "dbo.Committees");
            DropIndex("dbo.AppUserCommittees", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserCommittees", new[] { "Committee_CommitteeID" });
            DropPrimaryKey("dbo.AppUserEvents");
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddPrimaryKey("dbo.AppUserEvents", new[] { "AppUser_Id", "Event_EventID" });
            DropTable("dbo.AppUserCommittees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppUserCommittees",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Committee_CommitteeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Committee_CommitteeID });
            
            DropPrimaryKey("dbo.AppUserEvents");
            DropColumn("dbo.AspNetUsers", "Password");
            AddPrimaryKey("dbo.AppUserEvents", new[] { "Event_EventID", "AppUser_Id" });
            CreateIndex("dbo.AppUserCommittees", "Committee_CommitteeID");
            CreateIndex("dbo.AppUserCommittees", "AppUser_Id");
            AddForeignKey("dbo.AppUserCommittees", "Committee_CommitteeID", "dbo.Committees", "CommitteeID", cascadeDelete: true);
            AddForeignKey("dbo.AppUserCommittees", "AppUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.AppUserEvents", newName: "EventAppUsers");
        }
    }
}
