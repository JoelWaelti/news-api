using news_api.Models;

namespace news_api.Services;

public interface INewsService
{
    Task<ICollection<Article>> GetTrendingArticlesAsync(int count, DateTime? from, DateTime? to);
    Task<ICollection<Article>> SearchArticlesAsync(string? keywords, DateTime? from, DateTime? to, int count, string searchIn);
}
