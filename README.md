# News API

This is a simple news API to fetch news articles. It uses the GNews API to get the articles.

## Endpoints

### Articles Endpoint
**Endpoint:** `/news`

Gets trending articles.

**Query Parameters:**

| Parameter | Default | Description                                                                                               |
|-----------|---------|-----------------------------------------------------------------------------------------------------------|
| date      | None    | Filters articles by date.<br>Format: YYYY-MM-DD<br>E.g. 2025-06-22                                        |
| count     | 10      | Specifies the max number of articles to be returned.<br>Can be between 1 and 10. Other values are ignored |

### Search Endpoint

**Endpoint:** `/news/search`

Let's you search articles by keywords in title, description and/or content.

**Query Parameters:**

| Parameter | Default                   | Description                                                                                                                                                                |
|-----------|---------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| keywords  | None                      | **Mandatory**<br>The keywords to search for.<br>The keywords are directly forwarded to the GNews API.<br>For more details see https://gnews.io/docs/v4?csharp#query-syntax |
| searchIn  | title,description,content | Specifies where to search for the keywords.<br>Valid options are:<br>- title<br>- description<br>- content<br><br>Different options can be combined with a coma.           |
| date      | None                      | Filters articles by date.<br>Format: YYYY-MM-DD<br>E.g. 2025-06-22                                                                                                         |
| count     | 10                        | Specifies the max number of articles to be returned.<br>Can be between 1 and 10. Other values are ignored                                                                  |

## Running the application
The application can be run using the provided Dockerfile.

Run the app with the following commands:
```bash
cd <PATH TO REPO>/news-api
docker build -f news-api/Dockerfile -t news-api .
docker run -d --name news-api -p 8080:8080 -e GNEWS__APIKEY=<API KEY> news-api
```
This runs the application as a Docker container on https://localhost:8080.

INFO: You need to specify a GNews API key (Sign up under https://gnews.io to get one).
