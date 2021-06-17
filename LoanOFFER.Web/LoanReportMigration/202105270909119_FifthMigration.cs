namespace LoanOFFER.Web.LoanReportMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logins", "Name", c => c.String());
            AlterColumn("dbo.Logins", "Group", c => c.String());
            AlterColumn("dbo.Logins", "IPAddress", c => c.String());
            AlterColumn("dbo.Logins", "HostName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logins", "HostName", c => c.String(nullable: false));
            AlterColumn("dbo.Logins", "IPAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Logins", "Group", c => c.String(nullable: false));
            AlterColumn("dbo.Logins", "Name", c => c.String(nullable: false));
        }
    }
}
