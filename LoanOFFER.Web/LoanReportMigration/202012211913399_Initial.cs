namespace LoanOFFER.Web.LoanReportMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        FetchedData = c.Boolean(nullable: false),
                        LoginUser = c.String(),
                        ErrorName = c.String(),
                        ErrorDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportName = c.String(),
                        ExportedDate = c.DateTime(nullable: false),
                        LoginUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Group = c.String(),
                        Date = c.DateTime(nullable: false),
                        IPAddress = c.String(),
                        HostName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RemittaLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Request = c.String(),
                        PhoneNo = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        SpooledData = c.Boolean(nullable: false),
                        LoginUser = c.String(),
                        Date = c.DateTime(nullable: false),
                        bvn = c.String(),
                        mobileNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        ResponseDate = c.String(),
                        Duration = c.String(),
                        ResponseCode = c.String(),
                        ResponseDescr = c.String(),
                        Remark = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        SpooledData = c.Boolean(nullable: false),
                        LoginUser = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecID);
            
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
            DropTable("dbo.SalaryTopUpLoans");
            DropTable("dbo.RemittaLoans");
            DropTable("dbo.Logins");
            DropTable("dbo.Exports");
            DropTable("dbo.ErrorLoans");
        }
    }
}
