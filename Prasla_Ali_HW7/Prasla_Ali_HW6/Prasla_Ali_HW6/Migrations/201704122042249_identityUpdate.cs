namespace Prasla_Ali_HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identityUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberCommittees", "Member_MemberID", "dbo.Members");
            DropForeignKey("dbo.MemberCommittees", "Committee_CommitteeID", "dbo.Committees");
            DropForeignKey("dbo.MemberEvents", "Member_MemberID", "dbo.Members");
            DropForeignKey("dbo.MemberEvents", "Event_EventID", "dbo.Events");
            DropIndex("dbo.MemberCommittees", new[] { "Member_MemberID" });
            DropIndex("dbo.MemberCommittees", new[] { "Committee_CommitteeID" });
            DropIndex("dbo.MemberEvents", new[] { "Member_MemberID" });
            DropIndex("dbo.MemberEvents", new[] { "Event_EventID" });
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        OkToText = c.Boolean(nullable: false),
                        Major = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AppUserCommittees",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Committee_CommitteeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Committee_CommitteeID })
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeID, cascadeDelete: true)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Committee_CommitteeID);
            
            CreateTable(
                "dbo.EventAppUsers",
                c => new
                    {
                        Event_EventID = c.Int(nullable: false),
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Event_EventID, t.AppUser_Id })
                .ForeignKey("dbo.Events", t => t.Event_EventID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .Index(t => t.Event_EventID)
                .Index(t => t.AppUser_Id);
            
            DropTable("dbo.Members");
            DropTable("dbo.MemberCommittees");
            DropTable("dbo.MemberEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MemberEvents",
                c => new
                    {
                        Member_MemberID = c.Int(nullable: false),
                        Event_EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_MemberID, t.Event_EventID });
            
            CreateTable(
                "dbo.MemberCommittees",
                c => new
                    {
                        Member_MemberID = c.Int(nullable: false),
                        Committee_CommitteeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_MemberID, t.Committee_CommitteeID });
            
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
            
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventAppUsers", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventAppUsers", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.AppUserCommittees", "Committee_CommitteeID", "dbo.Committees");
            DropForeignKey("dbo.AppUserCommittees", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.EventAppUsers", new[] { "AppUser_Id" });
            DropIndex("dbo.EventAppUsers", new[] { "Event_EventID" });
            DropIndex("dbo.AppUserCommittees", new[] { "Committee_CommitteeID" });
            DropIndex("dbo.AppUserCommittees", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.EventAppUsers");
            DropTable("dbo.AppUserCommittees");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            CreateIndex("dbo.MemberEvents", "Event_EventID");
            CreateIndex("dbo.MemberEvents", "Member_MemberID");
            CreateIndex("dbo.MemberCommittees", "Committee_CommitteeID");
            CreateIndex("dbo.MemberCommittees", "Member_MemberID");
            AddForeignKey("dbo.MemberEvents", "Event_EventID", "dbo.Events", "EventID", cascadeDelete: true);
            AddForeignKey("dbo.MemberEvents", "Member_MemberID", "dbo.Members", "MemberID", cascadeDelete: true);
            AddForeignKey("dbo.MemberCommittees", "Committee_CommitteeID", "dbo.Committees", "CommitteeID", cascadeDelete: true);
            AddForeignKey("dbo.MemberCommittees", "Member_MemberID", "dbo.Members", "MemberID", cascadeDelete: true);
        }
    }
}
