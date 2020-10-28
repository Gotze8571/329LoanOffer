namespace LoanOFFER.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SalaryTopUpLoans",
                c => new
                    {
                        RecID = c.Int(nullable: false, identity: true),
                        RequestID = c.String(),
                        SourcePhone = c.String(),
                        Channel = c.String(),
                        RequestType = c.String(),
                        RequestDate = c.String(),
                        Duration = c.String(),
                        ResponseCode = c.String(),
                        Remark = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        SpooledData = c.Boolean(nullable: false),
                        LoginUser = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecID);
            
            DropTable("dbo.SimbrellaLoans");
        }
        
        public override void Down()
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
            
            DropTable("dbo.SalaryTopUpLoans");
        }
    }
}
