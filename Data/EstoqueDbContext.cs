using Microsoft.EntityFrameworkCore;
using Microservicos.Models;

namespace ProductService.Infrastructure.Data
{
    public class EstoqueDbContext : DbContext
    {
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<EstoqueItem> EstoqueItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Descricao).HasMaxLength(255);
                entity.Property(p => p.Preco).HasColumnType("decimal(18,2)");
            });

            // EstoqueItem
            modelBuilder.Entity<EstoqueItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantidade).IsRequired();

                entity.HasIndex(e => e.ProdutoId).IsUnique(); // Um estoque por produto

                entity.HasOne<Produto>()
                      .WithMany()
                      .HasForeignKey(e => e.ProdutoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
