using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace JSONAPI_WebApp
{
    public static class ProductsEndPoints
    {
        public static void MapProductsEndPoints(this WebApplication app)
        {
            
            app.MapGet("/servertime", () =>
            {
                var serverTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                return Results.Ok(new { serverTime });
            });

            app.MapGet("/filteredposts", async ([FromServices] HttpClient client) =>
            {
                var response = await client.GetAsync("posts");
                if (response.IsSuccessStatusCode)
                {
                    var posts = await response.Content.ReadFromJsonAsync<List<Post>>(); // Deserialize JSON
                    if (posts != null && posts.Any())
                    {
                        // Filter and sort the posts
                        var filteredPosts = posts
                            .Where(p => p.Body
                            .Contains("minima"))
                            .OrderBy(p => p.Id)
                            .ToList();
                        return Results.Ok(filteredPosts);
                    }
                    else
                    {
                        return Results.Ok(new List<Post>()); // Return an empty list if no posts found
                    }
                }
                return Results.BadRequest("Failed to retrieve posts from the external URL.");
            });
        }
    }
}
