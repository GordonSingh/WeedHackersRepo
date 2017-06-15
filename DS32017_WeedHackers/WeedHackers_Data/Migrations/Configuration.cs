using System.Collections.Generic;
using System.Data.Entity.Validation;
using WeedHackers_Data.Entities;
using WeedHackers_Data.ServiceProcess;
using WeedHackers_Data.UserTypes;

namespace WeedHackers_Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WeedHackers_Data.WeedHackersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WeedHackers_Data.WeedHackersContext context)
        {
            #region CustomerTypes

            var individualCustomerType = new CustomerType
            {
                Name = "Individual Customer",
                Timestamp = DateTime.Now,
                Deleted = false
            };
            var companyCustomerType = new CustomerType
            {
                Name = "Corporate/Company ",
                Timestamp = DateTime.Now,
                Deleted = false
            };

            context.CustomerTypes.AddOrUpdate(ct => ct.Name, individualCustomerType);
            context.CustomerTypes.AddOrUpdate(ct => ct.Name, companyCustomerType);

            context.SaveChanges();

            #endregion

            #region EmployeeTypes
            var managerEmployeeType = new EmployeeType
            {
                Name = "Manager",
                Timestamp = DateTime.Now,
                Deleted = false
            };
            var employeeEmployeeType = new EmployeeType
            {
                Name = "Employee",
                Timestamp = DateTime.Now,
                Deleted = false
            };
            var serviceAdvisorEmployeeType = new EmployeeType
            {
                Name = "Service Advisor",
                Timestamp = DateTime.Now,
                Deleted = false
            };
            var adminEmployeeType = new EmployeeType
            {
                Name = "Admin",
                Timestamp = DateTime.Now,
                Deleted = false
            };

            context.EmployeeTypes.AddOrUpdate(et => et.Name, managerEmployeeType);
            context.EmployeeTypes.AddOrUpdate(et => et.Name, employeeEmployeeType);
            context.EmployeeTypes.AddOrUpdate(et => et.Name, serviceAdvisorEmployeeType);
            context.EmployeeTypes.AddOrUpdate(et => et.Name, adminEmployeeType);

            context.SaveChanges();

            #endregion

            #region Departments

            var wasteManagementDepartment = new Department
            {
                DepartmentName = "Waste Management",
                Timestamp = DateTime.Now,
            };
            var industrialCleaningDepartment = new Department
            {
                DepartmentName = "Industrial Cleaning",
                Timestamp = DateTime.Now,
            };
            var estateMaintenanceDepartment = new Department
            {
                DepartmentName = "Estate Maintenance",
                Timestamp = DateTime.Now,
            };

            context.Departments.AddOrUpdate(d => d.DepartmentName, wasteManagementDepartment, industrialCleaningDepartment, estateMaintenanceDepartment);

            context.SaveChanges();

            #endregion

            #region Users
            var adminArinSingh = new User
            {
                Name = "Arin Gordon",
                Surname = "Singh",
                Email = "agsingh@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "081 432 1100",
                SuperAdmin = true
            };

            var austinRyan = new User
            {
                Name = "Austin Ryan",
                Surname = "Govender",
                Email = "argov@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "081 223 2536",
            };

            var tristan = new User
            {
                Name = "Tristan",
                Surname = "Sukhnandan",
                Email = "tsukh@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "072 342 8999",
            };

            var nikita = new User
            {
                Name = "Nikita",
                Surname = "Ramroop",
                Email = "nikir@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "062 155 7738",
            };
            var adhikesh = new User
            {
                Name = "Adhikesh",
                Surname = "Bheejan",
                Email = "adbee@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "062 155 7738",
            };
            var serviceAdvisorNevesh = new User
            {
                Name = "Nevesh",
                Surname = "Dwarika",
                Email = "nevd@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "062 155 7738",
            };
            var serviceAdvisorMark = new User
            {
                Name = "Mark",
                Surname = "Hoppus",
                Email = "mhoppus@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "062 155 7738",
            };
            var serviceAdvisorTom = new User
            {
                Name = "Tom",
                Surname = "Delonge",
                Email = "tommy@gmail.com",
                Password = "735a0838bc6dbf708bfa928fc956e5af7dd3e64d5abfd27a537985f2334e3fcbc79cf2d01888d00427343ff868ac72d312628cc5e41e91c3e600f737554cb02e",
                PhoneNumber = "062 155 7738",
            };

            context.Users.AddOrUpdate(u => u.Email, austinRyan, tristan, nikita, adhikesh, adminArinSingh, serviceAdvisorNevesh,serviceAdvisorTom,serviceAdvisorMark);

            context.SaveChanges();

            //try
            //{
            //    if (System.Diagnostics.Debugger.IsAttached == false)
            //    {

            //        //System.Diagnostics.Debugger.Launch();

            //    }
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    // Retrieve the error messages as a list of strings.
            //    var errorMessages = ex.EntityValidationErrors
            //            .SelectMany(x => x.ValidationErrors)
            //            .Select(x => x.ErrorMessage);

            //    // Join the list to a single string.
            //    var fullErrorMessage = string.Join("; ", errorMessages);

            //    // Combine the original exception message with the new one.
            //    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

            //    // Throw a new DbEntityValidationException with the improved exception message.
            //    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            //}

            #endregion

            #region Employees
            var adminEmp = new Employee
            {
                Id = adminArinSingh.Id,
                EmployeeTypeId = adminEmployeeType.Id,
                DepartmentId = wasteManagementDepartment.Id
            };

            var emp1 = new Employee
            {
                Id = austinRyan.Id,
                EmployeeTypeId = managerEmployeeType.Id,
                DepartmentId = wasteManagementDepartment.Id,
            };
            var emp2 = new Employee
            {
                Id = tristan.Id,
                EmployeeTypeId = managerEmployeeType.Id,
                DepartmentId = industrialCleaningDepartment.Id

            };

            var emp3 = new Employee
            {
                Id = nikita.Id,
                EmployeeTypeId = managerEmployeeType.Id,
                DepartmentId = estateMaintenanceDepartment.Id

            };
            var serviceAdvisorEmp = new Employee
            {
                Id = serviceAdvisorNevesh.Id,
                EmployeeTypeId = serviceAdvisorEmployeeType.Id,
                DepartmentId = estateMaintenanceDepartment.Id
            };
            var serviceAdvisorEmp2 = new Employee
            {
                Id = serviceAdvisorMark.Id,
                EmployeeTypeId = serviceAdvisorEmployeeType.Id,
                DepartmentId = wasteManagementDepartment.Id
            };
            var serviceAdvisorEmp3 = new Employee
            {
                Id = serviceAdvisorTom.Id,
                EmployeeTypeId = serviceAdvisorEmployeeType.Id,
                DepartmentId = industrialCleaningDepartment.Id
            };
            var emp4 = new Employee
            {
                Id = adhikesh.Id,
                EmployeeTypeId = employeeEmployeeType.Id,
                DepartmentId = estateMaintenanceDepartment.Id
            };

            context.Employees.AddOrUpdate(eu => eu.Id, adminEmp, emp2, emp3, emp4, emp1, serviceAdvisorEmp,serviceAdvisorEmp2,serviceAdvisorEmp3);

            context.SaveChanges();

            #endregion

            #region DepartmentManagerAllocation

            wasteManagementDepartment.ManagerId = emp1.Id;
            estateMaintenanceDepartment.ManagerId = emp3.Id;
            industrialCleaningDepartment.ManagerId = emp2.Id;
            context.Departments.AddOrUpdate(d=>d.Id,wasteManagementDepartment,estateMaintenanceDepartment,industrialCleaningDepartment);
            context.SaveChanges();
            #endregion

            #region Services

            var service1 = new Service
            {
                ServiceName = "Pest and Weed Control",
                Timestamp = DateTime.Now,
                DepartmentId = wasteManagementDepartment.Id,
                PricePerUnit = 20.00,
                UnitDescription = "Litre",
                UnitSuffix = "lt"
            };
            var service2 = new Service
            {
                ServiceName = "Medical Waste Removal",
                Timestamp = DateTime.Now,
                DepartmentId = wasteManagementDepartment.Id,
                PricePerUnit = 10000.00,
                UnitDescription = "Ton",
                UnitSuffix = "T"
            };
            var service3 = new Service
            {
                ServiceName = "Sanitation Services",
                Timestamp = DateTime.Now,
                DepartmentId = industrialCleaningDepartment.Id,
                PricePerUnit = 1500,
                UnitDescription = "Blockage",
                UnitSuffix = "Blocked Pipes"
            };
            var service4 = new Service
            {
                ServiceName = "Oil Spill Cleanup and Containment",
                Timestamp = DateTime.Now,
                DepartmentId = industrialCleaningDepartment.Id,
                PricePerUnit = 1500,
                UnitDescription = "Area Covered",
                UnitSuffix = "m^2"
            };
            var service5 = new Service
            {
                ServiceName = "Lawn Maintenance",
                Timestamp = DateTime.Now,
                DepartmentId = estateMaintenanceDepartment.Id,
                PricePerUnit = 20.00,
                UnitDescription = "Grass per square meter",
                UnitSuffix = "m^2"
            };
            var service6 = new Service
            {
                ServiceName = "Tree Felling",
                Timestamp = DateTime.Now,
                DepartmentId = estateMaintenanceDepartment.Id,
                PricePerUnit = 3000,
                UnitDescription = "Per Tree",
                UnitSuffix = "Trees"
            };
            var service7 = new Service
            {
                ServiceName = "Fumigation",
                Timestamp = DateTime.Now,
                DepartmentId = estateMaintenanceDepartment.Id,
                PricePerUnit = 2000,
                UnitDescription = "Area Covered",
                UnitSuffix = "m^2"
            };

            context.Services.AddOrUpdate(sv => sv.ServiceName, service1, service2, service3, service3, service4, service5, service6, service7);

            context.SaveChanges();

            #endregion

            #region Service Request Statuses
            
            var createdUpdate = new ServiceStatus
            {
                Name = "Created"
            };

            var inspectedUpdate = new ServiceStatus
            {
                Name = "Inspected"
            };

            var acceptedUpdate = new ServiceStatus
            {
                Name = "Accepted"
            };
            var rejectedUpdate = new ServiceStatus
            {
                Name = "Rejected"
            };
            var progressUpdate = new ServiceStatus
            {
                Name = "In Progress"
            };
            var completeUpdate = new ServiceStatus
            {
                Name = "Completed"
            };

            context.ServiceStatuses.AddOrUpdate(ss=>ss.Name,createdUpdate,inspectedUpdate,acceptedUpdate,rejectedUpdate,progressUpdate,completeUpdate);

            context.SaveChanges();
            #endregion
        }
    }
}
