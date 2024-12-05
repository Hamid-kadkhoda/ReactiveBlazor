using System.Reactive.Linq;
using System.Text.Json;
using System.Text;

public class ObservableHttpClient : IDisposable
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _defaultSerializerOptions;

    public ObservableHttpClient(HttpClient http, JsonSerializerOptions? defaultSerializerOptions = null)
    {
        _http = http ?? throw new ArgumentNullException(nameof(http));
        _defaultSerializerOptions = defaultSerializerOptions ?? new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public IObservable<HttpResponseMessage> Send(IRequest req)
    {
        return Observable.FromAsync(async () =>
        {
            var request = new HttpRequestMessage(req.HttpMethod, req.Endpoint);

            // Only serialize and set content if it's not a GET request
            if (req.HttpMethod != HttpMethod.Get)
            {
                string jsonContent = JsonSerializer.Serialize(req, _defaultSerializerOptions);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            try
            {
                return await _http.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Network error occurred: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Request failed: {ex.Message}", ex);
            }
        });
    }

    public IObservable<T> Send<T>(IRequest req, JsonSerializerOptions? options = null)
    {
        return Send(req)
            .SelectMany(async response =>
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // If the response is not successful, wrap the error details
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(
                            $"Request failed with status {response.StatusCode}: {content}",
                            null,
                            response.StatusCode
                        );
                    }

                    // Handle empty response
                    if (string.IsNullOrEmpty(content))
                    {
                        return default(T)!;
                    }

                    // Deserialize with provided options or fall back to default options
                    return JsonSerializer.Deserialize<T>(
                        content, 
                        options ?? _defaultSerializerOptions
                    ) ?? throw new JsonException("Deserialization resulted in null object");
                }
                catch (JsonException ex)
                {
                    throw new InvalidOperationException($"Failed to parse response: {ex.Message}", ex);
                }
            });
    }

    public void Dispose()
    {
        _http?.Dispose();
        GC.SuppressFinalize(this);
    }
}