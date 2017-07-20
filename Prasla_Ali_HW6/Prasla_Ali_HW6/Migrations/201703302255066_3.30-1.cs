namespace Prasla_Ali_HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3301 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Committees", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Committees", "Name", c => c.String());
        }
    }
}
