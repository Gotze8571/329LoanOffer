namespace LoanOFFER.Web.LoanReportMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FashcashLoans",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FashcashLoans");
        }
    }
}
