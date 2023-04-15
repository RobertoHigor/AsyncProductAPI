namespace WebApi.Dtos;

public sealed class ListingStatus
{
    public string? RequestStatus { get; set; }
    public string? EstimatedCompletionTime { get; set; }
    // URL para o resultado final
    public string? ResourceUrl { get; set; }
}