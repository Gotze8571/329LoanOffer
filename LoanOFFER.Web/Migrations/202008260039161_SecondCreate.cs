namespace LoanOFFER.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SimbrellaLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Request = c.String(),
                        customerId = c.String(),
                        RequestTime = c.DateTime(nullable: false),
                        LogDate = c.DateTime(nullable: false),
                        AccessString = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SimbrellaLoanTrackers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        FetchedData = c.Boolean(nullable: false),
                        LoginUser = c.String(),
                        CustomerId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SimbrellaLoanTrackers");
            DropTable("dbo.SimbrellaLoans");
        }
    }
}
