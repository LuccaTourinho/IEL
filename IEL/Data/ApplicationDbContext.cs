using IEL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IEL.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        public DbSet<Estudante> Estudantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Garantir as unicidades de nome e cpf
            modelBuilder.Entity<Estudante>()
                .HasIndex(e => e.Nome)
                .IsUnique();
            modelBuilder.Entity<Estudante>()
                .HasIndex(e => e.CPF)
                .IsUnique();

            // Configure as propriedades CreatedAt e UpdateAt para o modelo Estudante
            modelBuilder.Entity<Estudante>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Estudante>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Estudante &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((Estudante)entry.Entity).CreatedAt = DateTime.Now;
                    ((Estudante)entry.Entity).UpdatedAt = DateTime.Now;
                }

                if(entry.State == EntityState.Modified)
                {
                    ((Estudante)entry.Entity).UpdatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }


    }
}
