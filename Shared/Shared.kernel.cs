namespace Shared
{
public record ItemPedidoDto(Guid ProdutoId, string Nome, int Quantidade, decimal ValorUnitario);
public record CriarPedidoDto(Guid ClienteId, List<ItemPedidoDto> Itens);


public record PedidoCriadoEvent(Guid PedidoId, Guid ClienteId, List<ItemPedidoEvento> Itens);
public record ItemPedidoEvento(Guid ProdutoId, int Quantidade);


public record ReservaEstoqueResultadoEvent(Guid PedidoId, bool Sucesso, string? Motivo);
}