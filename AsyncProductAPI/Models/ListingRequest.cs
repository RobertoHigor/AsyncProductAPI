// Lista de produtos que serão criados
namespace WebApi.Models;

public sealed class ListingRequest
{
    public int Id { get; set; }

    // Body que vai conter a lista de produtos
    public string? RequestBody { get; set; }

    // Dados para retorno (deveria estar em um Dto)
    // Retorna para o cliente ao realizar o request
    public string? EstimatedCompletionTime { get; set; }
    public string? RequestStatus { get; set; }

    // Utilizado para acompanhar um request específico
    public string? RequestId { get; set; } = Guid.NewGuid().ToString();
}