using Microsoft.Extensions.Options;
using news_api.Models;
using Newtonsoft.Json;

namespace news_api.Services;

public class GNewsService : INewsService
{
    private readonly GNewsOptions gNewsOptions;

    public GNewsService(IOptions<GNewsOptions> gNewsOptions)
    {
        this.gNewsOptions = gNewsOptions.Value;
    }

    public async Task<ICollection<Article>> GetArticlesAsync(int count)
    {
        string url = $"{gNewsOptions.BaseUrl}/top-headlines?max={count}&apikey={gNewsOptions.ApiKey}";

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        List<Article> articles = data.Articles;

        return articles;
    }
}