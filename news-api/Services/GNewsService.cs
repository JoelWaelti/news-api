using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using news_api.Models;

namespace news_api.Services;

public class GNewsService : INewsService
{
    private readonly GNewsOptions gNewsOptions;
    private readonly HttpClient httpClient;

    public GNewsService(IOptions<GNewsOptions> gNewsOptions, HttpClient httpClient)
    {
        this.gNewsOptions = gNewsOptions.Value;
        this.httpClient = httpClient;
    }

    public async Task<ICollection<Article>> GetTrendingArticlesAsync(int count, DateTime? from, DateTime? to)
    {
        var query = new Dictionary<string, string?>()
        {
            ["max"] = count.ToString(),
            ["from"] = from?.ToString(),
            ["to"] = to?.ToString()
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
            ["from"] = from?.ToString(),
            ["to"] = to?.ToString()
        };

        return await FetchArticlesAsync("/search", query);
    }

    private async Task<ICollection<Article>> FetchArticlesAsync(string path, Dictionary<string, string?> queryParams)
    {
        queryParams["apikey"] = gNewsOptions.ApiKey;

        var uri = BuildUri(path, queryParams);
        var response = await httpClient.GetAsync(uri);
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
}