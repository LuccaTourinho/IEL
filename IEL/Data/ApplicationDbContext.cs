using IEL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IEL.Data
{
    /// <summary>
    /// Representa o contexto do banco de dados da aplicação, gerenciando as entidades e interações com o banco de dados.
    /// </summary>
    public class ApplicationDbContext: DbContext
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="ApplicationDbContext"/> com as opções configuradas.
        /// </summary>
        /// <param name="options">Configurações para o contexto do banco de dados.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        /// <summary>
        /// Representa o conjunto de entidades <see cref="Estudante"/> gerenciado pelo contexto.
        /// </summary>
        public DbSet<Estudante> Estudantes { get; set; }

        /// <summary>
        /// Configura o modelo de dados durante a criação do modelo.
        /// </summary>
        /// <param name="modelBuilder">Construtor de modelos usado para configurar entidades e relações.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chama a implementação base para garantir configurações padrão
            base.OnModelCreating(modelBuilder);

            //Garantir as unicidades de Nome e CPF para o modelo Estudante
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

        /// <summary>
        /// Sobrescreve o método padrão de salvamento de alterações no banco de dados para adicionar lógica personalizada.
        /// Atualiza os campos "CreatedAt" e "UpdatedAt" automaticamente antes de persistir as alterações.
        /// </summary>
        /// <returns>O número de registros afetados no banco de dados.</returns>
        public override int SaveChanges()
        {
            // Identifica as entidades "Estudante" que foram adicionadas ou modificadas
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Estudante &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    // Define a data de criação e atualização para novas entidades
                    ((Estudante)entry.Entity).CreatedAt = DateTime.Now;
                    ((Estudante)entry.Entity).UpdatedAt = DateTime.Now;
                }

                if(entry.State == EntityState.Modified)
                {
                    // Define a data de atualização para entidades alteradas
                    ((Estudante)entry.Entity).UpdatedAt = DateTime.Now;
                }
            }

            // Chama a implementação base para salvar as alterações no banco de dados
            return base.SaveChanges();
        }


    }
}
