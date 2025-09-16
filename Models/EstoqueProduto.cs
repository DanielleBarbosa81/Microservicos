using System;

namespace Microservicos.Models
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public decimal Preco { get; private set; }

        // Construtor protegido para EF
        protected Produto() { }

        public Produto(string nome, string descricao, decimal preco)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório");
            if (preco <= 0) throw new ArgumentException("Preço deve ser maior que zero");

            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
        }

        public void AtualizarPreco(decimal novoPreco)
        {
            if (novoPreco <= 0) throw new ArgumentException("Preço inválido");
            Preco = novoPreco;
        }
    }
}

