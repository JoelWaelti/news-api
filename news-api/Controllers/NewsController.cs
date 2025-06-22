using Microsoft.AspNetCore.Mvc;
using news_api.Models;
using news_api.Services;

namespace news_api.Controllers;

/// <summary>
/// Controller to fetch public news articles
/// </summary>
[ApiController]
[Route("[controller]")]
public class NewsController : ControllerBase
{
    private readonly ILogger<NewsController> logger;
    private readonly INewsService newsService;

    public NewsController(ILogger<NewsController> logger, INewsService newsService)
    {
        this.logger = logger;
        this.newsService = newsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetArticles(DateOnly? date, int count = 10)
    {
        DateTime? from = null, to = null;

        if (date.HasValue)
        {
            from = date.Value.ToDateTime(TimeOnly.MinValue);
            to = date.Value.ToDateTime(TimeOnly.MaxValue);
        }

        var articles = await newsService.GetTrendingArticlesAsync(count, from, to);
        return Ok(articles);
    }
}