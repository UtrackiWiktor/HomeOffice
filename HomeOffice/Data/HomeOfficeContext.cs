using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.classes.Users;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
namespace HomeOffice.Data
{
    class HomeOfficeContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        //then DbSets of tasks, units, user groups and task dictionaries
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("SERVER=homeoffice.c0pmexmy2ypg.eu-central-1.rds.amazonaws.com;PORT=3306;DATABASE=mydb;UID=admin;PASSWORD=hom3off1ce;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasColumnName("Name");
                entity.Property(e => e.Surname).IsRequired().HasColumnName("Surename");
                entity.Property(e => e.DateOfBirth).IsRequired().HasColumnName("DateOfBirth");
                entity.Property(e => e.Unit).IsRequired().HasColumnName("Units_Id");
                entity.Property(e => e.UserGroup).IsRequired().HasColumnName("UserRoles_Id");
            });


            
        }
        

    }
}
