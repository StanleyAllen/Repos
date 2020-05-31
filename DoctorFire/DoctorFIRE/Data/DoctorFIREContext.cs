using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorFIRE.Models;

namespace DoctorFIRE.Models
{
    public class DoctorFIREContext : DbContext
    {
        public DoctorFIREContext (DbContextOptions<DoctorFIREContext> options)
            : base(options)
        {
        }

        public DbSet<Context> Contexts { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<CCbyID> CCbyIDs { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseRecord> CaseRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Context>().ToTable("Context");
            modelBuilder.Entity<Content>().ToTable("Content");
            modelBuilder.Entity<CCbyID>().ToTable("CCbyID");
            modelBuilder.Entity<Case>().ToTable("Cases");
            modelBuilder.Entity<CaseRecord>().ToTable("CaseRecords");
        }

    }
}
