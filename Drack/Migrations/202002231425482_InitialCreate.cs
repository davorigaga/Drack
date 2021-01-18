namespace Drack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        OtherNames = c.String(),
                        DateOfBirth = c.DateTime(),
                        MaritalStatus = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        State = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        ImagePath = c.String(),
                        DriverStatus = c.Int(nullable: false),
                        Verified = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.DriverId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Headline = c.String(nullable: false),
                        NewsDescription = c.String(nullable: false),
                        NewsImagePath = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
            DropTable("dbo.Drivers");
        }
    }
}
