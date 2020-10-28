namespace LoanOFFER.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondinitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RemittaLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNo = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        SpooledData = c.Boolean(nullable: false),
                        LoginUser = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RemittaLoans");
        }
    }
}
