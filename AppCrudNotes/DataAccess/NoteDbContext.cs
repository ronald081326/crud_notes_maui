

using AppCrudNotes.Models;
using AppCrudNotes.Utils;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppCrudNotes.DataAccess
{
    public class NoteDbContext : DbContext
    {
        public DbSet<Note> Note { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conectionDb = $"Filename={ConectionDB.getRoute("notes.db")}";
            optionsBuilder.UseSqlite(conectionDb);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(col => col.id);
                entity.Property(col => col.id).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
