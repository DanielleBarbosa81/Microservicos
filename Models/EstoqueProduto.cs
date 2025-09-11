using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservicos.Models
{
 
   public class Produto
{
public Guid Id { get; set; }
public string Nome { get; set; } = string.Empty;
public string Descricao { get; set; } = string.Empty;
public decimal Preco { get; set; }
}


public class EstoqueItem
{
public Guid Id { get; set; }
public Guid ProdutoId { get; set; }
public int Quantidade { get; private set; }


public void Adicionar(int qtd) => Quantidade += qtd;
public bool Reservar(int qtd)
{
if (qtd <= 0) return false;
if (Quantidade < qtd) return false;
Quantidade -= qtd;
return true;
}
public void Liberar(int qtd) => Quantidade += qtd;
}
}
