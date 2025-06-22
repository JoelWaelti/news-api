using news_api.Models;

namespace news_api.Services;

public interface INewsService
{
    Task<ICollection<Article>> GetArticlesAsync(int count);
}
