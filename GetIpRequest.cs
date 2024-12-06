namespace ReactiveBlazor
{
    public class GetIpRequest: IRequest
    {
        public string Endpoint => "https://freeipapi.com/api/json";

        public string? QueryParams { get; set; }

        public string? Fragments { get; set; }

        public HttpMethod HttpMethod => HttpMethod.Post;
    }
}

