using FlugDemo.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlugDemo.Data
{
    public class FlugDbContext: DbContext
    {
        public DbSet<Flug> Fluege { get; set; }

        public DbSet<Buchung> Buchungen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV12;Initial Catalog=FlugDemoDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Flug>()
                .Property(f => f.AblugOrt)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Flug>()
                        .HasMany(f => f.Buchungen)
                        .WithOne(b => b.Flug)
                        .HasForeignKey(b => b.FlugId)
                        .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
