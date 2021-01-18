namespace Drack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DriverLicenses",
                c => new
                    {
                        DriverLicenseId = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        DriverLicenseImagePath = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.DriverLicenseId)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.DriverNextOfKins",
                c => new
                    {
                        DriverNextOfKinId = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
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
                        Relationship = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.DriverNextOfKinId)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Garages",
                c => new
                    {
                        GarageId = c.Int(nullable: false, identity: true),
                        GarageName = c.String(nullable: false),
                        GarageNumber = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        DriverDue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.GarageId);
            
            CreateTable(
                "dbo.GarageChairmen",
                c => new
                    {
                        GarageChairmanId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        GarageImagePath = c.String(),
                        GarageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GarageChairmanId)
                .ForeignKey("dbo.Garages", t => t.GarageId, cascadeDelete: true)
                .Index(t => t.GarageId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentStatus = c.Int(nullable: false),
                        DateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .Index(t => t.DriverId);
            
            AddColumn("dbo.Drivers", "GarageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Drivers", "GarageId");
            AddForeignKey("dbo.Drivers", "GarageId", "dbo.Garages", "GarageId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Drivers", "GarageId", "dbo.Garages");
            DropForeignKey("dbo.GarageChairmen", "GarageId", "dbo.Garages");
            DropForeignKey("dbo.DriverNextOfKins", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.DriverLicenses", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Payments", new[] { "DriverId" });
            DropIndex("dbo.GarageChairmen", new[] { "GarageId" });
            DropIndex("dbo.DriverNextOfKins", new[] { "DriverId" });
            DropIndex("dbo.Drivers", new[] { "GarageId" });
            DropIndex("dbo.DriverLicenses", new[] { "DriverId" });
            DropColumn("dbo.Drivers", "GarageId");
            DropTable("dbo.Payments");
            DropTable("dbo.GarageChairmen");
            DropTable("dbo.Garages");
            DropTable("dbo.DriverNextOfKins");
            DropTable("dbo.DriverLicenses");
        }
    }
}
