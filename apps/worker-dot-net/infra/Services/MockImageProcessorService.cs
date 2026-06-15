using Microsoft.Extensions.Logging;
using DermePlan.Worker.Domain.Services;

namespace DermePlan.Worker.Infra.Services;

public class MockImageProcessorService : IImageProcessorService
{
    private readonly ILogger<MockImageProcessorService> _logger;

    public MockImageProcessorService(ILogger<MockImageProcessorService> logger)
    {
        _logger = logger;
    }

    public async Task<ProcessedImageResult> ProcessImageAsync(string imageUrl, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Processando imagem (MOCK): {ImageUrl}", imageUrl);

        await Task.Delay(100, cancellationToken);

        var mockImageData = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };

        return new ProcessedImageResult
        {
            ProcessedImageUrl = $"{imageUrl}-processed",
            ImageData = mockImageData,
            ContentType = "image/jpeg",
            Width = 1024,
            Height = 768
        };
    }
}
