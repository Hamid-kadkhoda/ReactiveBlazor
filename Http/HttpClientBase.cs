using System.Text;
using System.Text.Json;

namespace System.Net.Http;

public class HttpClientBase(HttpClient http): IHttpClientBase
{
    public async Task<T?> SendAsync<T>(IRequest req)
    {
        try
        {
            var interfaceProps = typeof(IRequest).GetProperties().Select(p => p.Name).ToHashSet();
            var objectProps = req.GetType()
                .GetProperties()
                .Where(p => !interfaceProps.Contains(p.Name));

            var requestUri = req.Endpoint;

            if (req.HttpMethod == HttpMethod.Get && objectProps.Any())
            {
                var queryParams = objectProps
                    .Select(p => $"{Uri.EscapeDataString(char.ToLowerInvariant(p.Name[0]) + p.Name[1..])}" +
                                $"={Uri.EscapeDataString(p.GetValue(req)?.ToString() ?? "")}")
                    .Where(q => !string.IsNullOrEmpty(q));

                requestUri = $"{req.Endpoint}?{string.Join("&", queryParams)}";
            }

            var request = new HttpRequestMessage(req.HttpMethod, requestUri);

            if (req.HttpMethod != HttpMethod.Get)
            {
                var serializableObj = new
                {
                    Data = objectProps.ToDictionary(
                        p => char.ToLowerInvariant(p.Name[0]) + p.Name[1..],
                        p => p.GetValue(req)
                    )
                }.Data;

                string jsonContent = JsonSerializer.Serialize(serializableObj);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                request.Content = content;
            }

            var response = await http.SendAsync(request);

            var context = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(context);
        }
        catch
        {
            throw;
        }
    }
}
