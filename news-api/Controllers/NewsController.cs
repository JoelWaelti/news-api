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
    public async Task<IActionResult> GetArticles(DateOnly? date, int count = 10)
    {
        logger.LogInformation("Getting articles... (count = {count}, date = {date})", count, date);

        DateTime? from = null, to = null;

        if (date.HasValue)
        {
            from = date.Value.ToDateTime(TimeOnly.MinValue);
            to = date.Value.ToDateTime(TimeOnly.MaxValue);
        }

        var articles = await newsService.GetTrendingArticlesAsync(count, from, to);
        return Ok(articles);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchArticles(string keywords, DateOnly? date, int count = 10, string searchIn = "title,description,content")
    {
        logger.LogInformation("Searching articles... (count = {count}, date = {date}, keywords = {keywords})", count, date, keywords);

        DateTime? from = null, to = null;

        if(date.HasValue)
        {
            from = date.Value.ToDateTime(TimeOnly.MinValue);
            to = date.Value.ToDateTime(TimeOnly.MaxValue);
        }

        var articles = await newsService.SearchArticlesAsync(keywords, from, to, count, searchIn);
        return Ok(articles);
    }
}