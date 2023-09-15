using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSmileSoft.Models;
using Microsoft.AspNetCore.Identity;


namespace WebSmileSoft.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options)
        {
        }

        public object Doctors { get; internal set; }
        public object Patients { get; internal set; }
    }
    //public DbSet<Doctor> Doctors { get; set; }
    //public DbSet<Patient> Patients { get; set; }
    //public DbSet<Modules> Modules{get; set; }
    //public DbSet<MedicalHistory> MedicalHistories { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Doctor>().ToTable("Doctor");
    //    modelBuilder.Entity<Patient>().ToTable("Patient");
    //    modelBuilder.Entity<Modules>().ToTable("Modules");
    //    modelBuilder.Entity<MedicalHistory>().ToTable("MedicalHistory");

    //}

}