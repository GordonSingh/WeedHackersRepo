namespace WeedHackers_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cancellations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reason = c.String(nullable: false, maxLength: 500),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Reason, unique: true);
            
            CreateTable(
                "dbo.ServiceRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        ServiceAdvisorId = c.Int(nullable: false),
                        FrequencyInMonths = c.Int(nullable: false),
                        IsResolved = c.Boolean(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        UnitQuantity = c.Double(),
                        LeadEmployeeId = c.Int(),
                        CancellationId = c.Int(),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.LeadEmployeeId)
                .ForeignKey("dbo.Employees", t => t.ServiceAdvisorId, cascadeDelete: false)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: false)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Cancellations", t => t.CancellationId)
                .Index(t => t.ServiceId)
                .Index(t => t.ServiceAdvisorId)
                .Index(t => t.CustomerId)
                .Index(t => t.LeadEmployeeId)
                .Index(t => t.CancellationId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        EmployeeTypeId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeTypes", t => t.EmployeeTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.DepartmentId)
                .Index(t => t.EmployeeTypeId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 500),
                        ManagerId = c.Int(),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .Index(t => t.DepartmentName, unique: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(nullable: false, maxLength: 500),
                        PricePerUnit = c.Double(nullable: false),
                        UnitDescription = c.String(nullable: false),
                        UnitSuffix = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .Index(t => t.ServiceName, unique: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.EmployeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Surname = c.String(nullable: false, maxLength: 500),
                        Email = c.String(nullable: false, maxLength: 500),
                        Password = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        SuperAdmin = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Name, t.Surname }, unique: true, name: "IX_FullName")
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        CustomerTypeId = c.Int(nullable: false),
                        EmailVerified = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerTypes", t => t.CustomerTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CustomerTypeId);
            
            CreateTable(
                "dbo.CustomerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceRequestStatusUpdates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceRequestId = c.Int(nullable: false),
                        ServiceStatusId = c.Int(nullable: false),
                        Message = c.String(nullable: false),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceStatuses", t => t.ServiceStatusId, cascadeDelete: false)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequestId, cascadeDelete: false)
                .Index(t => t.ServiceRequestId)
                .Index(t => t.ServiceStatusId);
            
            CreateTable(
                "dbo.ServiceStatuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Timestamp = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Deleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Employee_ServiceRequests",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        ServiceRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.ServiceRequestId })
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequestId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.ServiceRequestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceRequests", "CancellationId", "dbo.Cancellations");
            DropForeignKey("dbo.ServiceRequestStatusUpdates", "ServiceRequestId", "dbo.ServiceRequests");
            DropForeignKey("dbo.ServiceRequestStatusUpdates", "ServiceStatusId", "dbo.ServiceStatuses");
            DropForeignKey("dbo.ServiceRequests", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Employees", "Id", "dbo.Users");
            DropForeignKey("dbo.Customers", "Id", "dbo.Users");
            DropForeignKey("dbo.Customers", "CustomerTypeId", "dbo.CustomerTypes");
            DropForeignKey("dbo.Employees", "EmployeeTypeId", "dbo.EmployeeTypes");
            DropForeignKey("dbo.Services", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ServiceRequests", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Departments", "ManagerId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employee_ServiceRequests", "ServiceRequestId", "dbo.ServiceRequests");
            DropForeignKey("dbo.Employee_ServiceRequests", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ServiceRequests", "ServiceAdvisorId", "dbo.Employees");
            DropForeignKey("dbo.ServiceRequests", "LeadEmployeeId", "dbo.Employees");
            DropIndex("dbo.Employee_ServiceRequests", new[] { "ServiceRequestId" });
            DropIndex("dbo.Employee_ServiceRequests", new[] { "EmployeeId" });
            DropIndex("dbo.ServiceStatuses", new[] { "Name" });
            DropIndex("dbo.ServiceRequestStatusUpdates", new[] { "ServiceStatusId" });
            DropIndex("dbo.ServiceRequestStatusUpdates", new[] { "ServiceRequestId" });
            DropIndex("dbo.Customers", new[] { "CustomerTypeId" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", "IX_FullName");
            DropIndex("dbo.Services", new[] { "DepartmentId" });
            DropIndex("dbo.Services", new[] { "ServiceName" });
            DropIndex("dbo.Departments", new[] { "ManagerId" });
            DropIndex("dbo.Departments", new[] { "DepartmentName" });
            DropIndex("dbo.Employees", new[] { "EmployeeTypeId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "Id" });
            DropIndex("dbo.ServiceRequests", new[] { "CancellationId" });
            DropIndex("dbo.ServiceRequests", new[] { "LeadEmployeeId" });
            DropIndex("dbo.ServiceRequests", new[] { "CustomerId" });
            DropIndex("dbo.ServiceRequests", new[] { "ServiceAdvisorId" });
            DropIndex("dbo.ServiceRequests", new[] { "ServiceId" });
            DropIndex("dbo.Cancellations", new[] { "Reason" });
            DropTable("dbo.Employee_ServiceRequests");
            DropTable("dbo.ServiceStatuses");
            DropTable("dbo.ServiceRequestStatusUpdates");
            DropTable("dbo.CustomerTypes");
            DropTable("dbo.Customers");
            DropTable("dbo.Users");
            DropTable("dbo.EmployeeTypes");
            DropTable("dbo.Services");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.ServiceRequests");
            DropTable("dbo.Cancellations");
        }
    }
}
