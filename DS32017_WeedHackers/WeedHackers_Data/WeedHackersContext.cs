using System.Data.Entity;
using WeedHackers_Data.Entities;
using WeedHackers_Data.ServiceProcess;
using WeedHackers_Data.UserTypes;

namespace WeedHackers_Data
{
    public class WeedHackersContext : DbContext
    {
        public WeedHackersContext() : base("WeedHackConn")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOptional(d => d.ManagingEmployee)
                .WithMany(e => e.ManagingDepartments);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithRequired(e => e.Department);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Services)
                .WithRequired(s => s.Department);

            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.EmployeeType)
                .WithMany(et => et.Employees);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignedAsLeadServiceRequests)
                .WithOptional(sr => sr.LeadEmployee);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignedServiceRequests)
                .WithMany(sr => sr.AssignedEmployees)
                .Map(config =>
                {
                    config.MapLeftKey("EmployeeId");
                    config.MapRightKey("ServiceRequestId");
                    config.ToTable("Employee_ServiceRequests");
                });

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AssignedAsServiceAdvisorServiceRequests)
                .WithRequired(sr => sr.ServiceAdvisor);

            modelBuilder.Entity<Cancellation>()
                .HasMany(c => c.ServiceRequests)
                .WithOptional(sr => sr.Cancellation);

            modelBuilder.Entity<Service>()
                .HasMany(s => s.ServiceRequests)
                .WithRequired(sr => sr.Service);

            modelBuilder.Entity<ServiceRequest>()
                .HasRequired(sr => sr.Customer)
                .WithMany(c => c.ServiceRequests);

            modelBuilder.Entity<ServiceRequest>()
                .HasMany(sr => sr.ServiceRequestStatusUpdates)
                .WithRequired(su => su.ServiceRequest);

            modelBuilder.Entity<ServiceStatus>()
                .HasMany(ss => ss.ServiceRequestStatusUpdates)
                .WithRequired(su => su.ServiceStatus);

            modelBuilder.Entity<CustomerType>()
                .HasMany(ct => ct.Customers)
                .WithRequired(c => c.CustomerType);

            modelBuilder.Entity<EmployeeType>()
                .HasMany(et => et.Employees)
                .WithRequired(c => c.EmployeeType);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceRequestStatusUpdate> ServiceRequestStatusUpdates { get; set; }
        public DbSet<ServiceStatus> ServiceStatuses { get; set; }
    }
}
