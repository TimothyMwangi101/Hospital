using System;
using System.Collections.Generic;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public partial class ChdbContext : DbContext
{
    public ChdbContext()
    {
    }

    public ChdbContext(DbContextOptions<ChdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Medication> Medications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=CHDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medication>(entity =>
        {
            entity.HasKey(e => e.MedicationId).HasName("PK__medicati__DD94789BE1A77686");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
