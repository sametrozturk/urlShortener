# Simple URL Shortening Service

This is a simple URL shortening service implemented as a standalone .NET application with a RESTful API. The service provides the following features:

## Installation and Usage

1. Clone this repository.
2. Navigate to the `urlShortener\src\Presentation\Api` directory and locate the `appsettings.json` file.
3. Configure the database connection in the `appsettings.json` file under the `ConnectionStrings` section:

    **Note:** If you want to disable certificate validation when connecting to SQL Server, add the following to your connection string: `TrustServerCertificate=true;`
        
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "your_connection_string_here"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*"
    }

4. Set Api as a Startup Project 
5. Build and run the .NET application.
6. Use the provided RESTful API endpoints to shorten URLs and perform redirection.

## Technologies Used

- **.NET 7.0**: The application is built using .NET 7.0, a modern version of the .NET framework.
- **Microsoft SQL Server**: MSSQL is used as the database to persist URL data.
- **MediatR**: MediatR is used for implementing the Mediator pattern, enabling loose coupling between components.
- **Domain-Driven Design (DDD)**: The project follows DDD principles for designing and organizing the codebase.

## Shortening

Takes a long URL and returns a much shorter URL.

- The input URL must be in a valid format.
- The maximum character length for the hash portion of the URL is limited to 6.
- The service returns a meaningful message with a suitable HTTP status code.

**Example:**

- Input URL: `https://www.sample-site.com/careers/experienced/direct-entry/`
- Shortened URL: `http://sample.site/GUKA8w/`

## Redirection

Takes a short URL and redirects to the original URL.

**Example:**

- Short URL: `http://sample.site/GUKA8w/`
- Redirects to: `https://www.sample-site.com/careers/experienced/direct-entry/`

## Custom URL

Allows users to pick a custom shortened URL.

**Example:**

- Original URL: `http://www.sample-site.com/careers/job-search/`
- Custom Shortened URL: `http://sample.site/jobs`

## API Endpoints

- **Shortening Requests**:
  - `POST /api/shortenUrl`: Shorten a URL.
    - Request body: `{ "url": "your_long_url_here" }`
    - Response: `{ "url": "http://sample.site/hash" }`

- **Shortening Requests with Custom Hash**:
  - `POST /api/shortenUrlWithCustomHash`: Shorten a URL.
    - Request body: `{ "url": "your_long_url_here", "customHash": "customHash" }`
    - Response: `{ "url": "http://sample.site/customHash" }`

- **Redirection Requests**:
  - `GET /api/{hash}`: Redirect to the original URL.

## Example Usage

- To shorten a URL:
  ```http
  POST /api/shortenUrl
  Request Body:
  {
      "url": "https://www.sample-site.com/careers/experienced/direct-entry/"
  }
  
- To shorten a URL with Custom Hash:
  ```http
  POST /api/shortenUrlWithCustomHash
  Request Body:
  {
      "url": "https://www.sample-site.com/careers/experienced/direct-entry/",
      "customUrl": "jobs"
  }
  
- To redirect a Shorten Url:
  ```http
  GET /api/{hash}
  
## Repository

You can find the code and implementation details in [this GitHub repository](https://github.com/sametrozturk/UrlShortener).

Feel free to reach out if you have any questions or need further assistance with the implementation.
