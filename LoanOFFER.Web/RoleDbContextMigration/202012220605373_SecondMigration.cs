namespace LoanOFFER.Web.RoleDbContextMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.ErrorLoans");
        }
        
        public override void Down()
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
            
            DropTable("dbo.Users");
        }
    }
}
