using news_api.Models;

namespace news_api.Services;

public interface INewsService
{
    Task<ICollection<Article>> GetTrendingArticlesAsync(int count, DateTime? from, DateTime? to);
}
