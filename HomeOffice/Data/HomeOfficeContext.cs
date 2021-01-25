using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using HomeOffice.classes.Users;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using HomeOffice.classes.Tasks;
using HomeOffice.classes.Passwords;

using HomeOffice.classes.Units;
namespace HomeOffice.Data
{
    class HomeOfficeContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<classes.Tasks.Task> Tasks { get; set; }
        public DbSet<TaskDictionary> TaskDictionary { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Password> Passwords { get; set; }
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
                entity.Property(e => e.Surname).IsRequired().HasColumnName("Surname");
                entity.Property(e => e.DateOfBirth).IsRequired().HasColumnName("DateOfBirth");
                entity.Property(e => e.Unit).IsRequired().HasColumnName("Units_Id");
                entity.Property(e => e.UserGroup).IsRequired().HasColumnName("UserRoles_Id");
                entity.Property(e => e.PESEL).IsRequired().HasColumnName("PESEL");
            });

            modelBuilder.Entity<classes.Tasks.Task>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.TaskDictionaryID).IsRequired().HasColumnName("TaskDictionary_ID");
                entity.Property(e => e.UsersID).IsRequired().HasColumnName("Users_ID");
                entity.Property(e => e.Status).IsRequired().HasColumnName("Status");
            });

            modelBuilder.Entity<TaskDictionary>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.TaskDescription).IsRequired().HasColumnName("TaskDescription");
                entity.Property(e => e.TaskName).IsRequired().HasColumnName("TaskName");
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Password_).IsRequired().HasColumnName("Password");
            });
        }
    }
}
