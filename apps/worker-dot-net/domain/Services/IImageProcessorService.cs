namespace ServiceWorker.Domain.Services;

public interface IImageProcessorService
{
    Task<ProcessedImageResult> ProcessImageAsync(string imageUrl, CancellationToken cancellationToken = default);
}

public record ProcessedImageResult
{
    public required string ProcessedImageUrl { get; init; }
    public required byte[] ImageData { get; init; }
    public required string ContentType { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
}
