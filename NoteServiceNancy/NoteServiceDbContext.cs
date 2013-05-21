using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using NoteServiceNancy.Model;

namespace NoteServiceNancy
{
    public class NoteServiceDbContext : DbContext
    {
        public NoteServiceDbContext()
            : base("NoteServiceDB") { }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasKey(p => p.ID)
                .Property(p => p.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }
    }
}
