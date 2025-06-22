using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetArticles(int count)
    {
        var articles = await newsService.GetArticlesAsync(count);
        return Ok(articles);
    }
}