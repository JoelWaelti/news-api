using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using news_api.Models;

namespace news_api.Services;

public class GNewsService : INewsService
{
    private readonly GNewsOptions gNewsOptions;
    private readonly HttpClient httpClient;
    private readonly ILogger<GNewsService> logger;

    public GNewsService(IOptions<GNewsOptions> gNewsOptions, HttpClient httpClient, ILogger<GNewsService> logger)
    {
        this.gNewsOptions = gNewsOptions.Value;
        this.httpClient = httpClient;
        this.logger = logger;
    }

    public async Task<ICollection<Article>> GetTrendingArticlesAsync(int count, DateTime? from, DateTime? to)
    {
        var query = new Dictionary<string, string?>()
        {
            ["max"] = count.ToString(),
            ["from"] = ToGNewsDateString(from),
            ["to"] = ToGNewsDateString(to)
        };

        return await FetchArticlesAsync("/top-headlines", query);
    }

    public async Task<ICollection<Article>> SearchArticlesAsync(string? keywords, DateTime? from, DateTime? to, int count, string searchIn)
    {
        var query = new Dictionary<string, string?>()
        {
            ["q"] = keywords,
            ["in"] = searchIn,
            ["max"] = count.ToString(),
            ["from"] = ToGNewsDateString(from),
            ["to"] = ToGNewsDateString(to)
        };

        return await FetchArticlesAsync("/search", query);
    }

    private async Task<ICollection<Article>> FetchArticlesAsync(string path, Dictionary<string, string?> queryParams)
    {
        queryParams["apikey"] = gNewsOptions.ApiKey;

        var uri = BuildUri(path, queryParams);

        logger.LogInformation("Making GNews API request: GET {uri}", uri.ToString());

        var response = await httpClient.GetAsync(uri);

        logger.LogInformation("GNews API request returned: {statusCode}", response.StatusCode.GetDisplayName());

        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<ApiResponse>();

        return data?.Articles ?? new List<Article>();
    }

    private string BuildUri(string path, Dictionary<string, string?> queryParams)
    {
        var baseUrl = gNewsOptions.BaseUrl.TrimEnd('/');
        path = path.TrimStart('/');
        string url = QueryHelpers.AddQueryString($"{baseUrl}/{path}", queryParams);
        return url;
    }

    private string? ToGNewsDateString(DateTime? date)
    {
        return date?.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}