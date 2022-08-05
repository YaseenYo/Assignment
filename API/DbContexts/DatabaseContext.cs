using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Technician> Technicians { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Technician>().HasData(
                new Technician()
                {
                    TechnicianId = Guid.NewGuid(),
                    FirstName = "yaseen",
                    LastName = "Ahmed",
                    Status = true
                },
                new Technician()
                {
                    TechnicianId = Guid.NewGuid(),
                    FirstName = "Karan",
                    LastName = "Lala",
                    Status = true
                },
                new Technician()
                {
                    TechnicianId = Guid.NewGuid(),
                    FirstName = "yaseen",
                    LastName = "Ahmed",
                    Status = false,
                });

            modelBuilder.Entity<WorkOrder>().HasData(
                new WorkOrder(){
                    WorkOrderId = Guid.NewGuid(),
                    Place = "Bangalore Ramanagar",
                    DateTime = new DateTime(2022,01,10),
                },
                new WorkOrder(){
                    WorkOrderId = Guid.NewGuid(),
                    Place = "Bangalore magadi",
                    DateTime = new DateTime(2022,02,09)
                },
                new WorkOrder(){
                    WorkOrderId = Guid.NewGuid(),
                    Place = "Tumkur sira",
                    DateTime = new DateTime(2022,03,23)
                },
                new WorkOrder(){
                    WorkOrderId = Guid.NewGuid(),
                    Place = "Delhi kudur",
                    DateTime = new DateTime(2022,04,18),
                },
                new WorkOrder(){
                    WorkOrderId = Guid.NewGuid(),
                    Place = "Bangalore Kasargudu",
                    DateTime = new DateTime(2022,05,05),
                }

            );

            base.OnModelCreating(modelBuilder);
        }
    }
}