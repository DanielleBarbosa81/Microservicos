using System;

namespace Microservicos.Models
{
    public class EstoqueItem
    {
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }

        // Construtor protegido para EF
        protected EstoqueItem() { }

        public EstoqueItem(Guid produtoId, int quantidadeInicial)
        {
            if (quantidadeInicial < 0) throw new ArgumentException("Quantidade inicial inválida");

            Id = Guid.NewGuid();
            ProdutoId = produtoId;
            Quantidade = quantidadeInicial;
        }

        public void Adicionar(int qtd)
        {
            if (qtd <= 0) throw new ArgumentException("Quantidade inválida");
            Quantidade += qtd;
        }

        public bool Reservar(int qtd)
        {
            if (qtd <= 0 || Quantidade < qtd) return false;
            Quantidade -= qtd;
            return true;
        }

        public void Liberar(int qtd)
        {
            if (qtd <= 0) throw new ArgumentException("Quantidade inválida");
            Quantidade += qtd;
        }
    }
}
