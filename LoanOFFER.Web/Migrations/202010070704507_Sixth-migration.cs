namespace LoanOFFER.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sixthmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalaryTopUpLoans", "ResponseDate", c => c.String());
            AddColumn("dbo.SalaryTopUpLoans", "ResponseDescr", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalaryTopUpLoans", "ResponseDescr");
            DropColumn("dbo.SalaryTopUpLoans", "ResponseDate");
        }
    }
}
