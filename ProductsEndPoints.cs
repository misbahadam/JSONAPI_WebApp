using JSONAPI_WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using JSONAPI_WebApp.Data.Models;

namespace JSONAPI_WebApp;

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

        app.MapPost("/products", async (ProductDTO productToCreateDTO, AppDbContext dbContext) =>
        {
            // Let EF Core auto-increment the ID.
            Product ProductToCreate = new()
            {
                Id = 0,
                ProductId = productToCreateDTO.ProductId,
                ProductName = productToCreateDTO.ProductName,
                StockAvailable = productToCreateDTO.StockAvailable,
                CreatedAt = productToCreateDTO.CreatedAt,
                UpdatedAt = productToCreateDTO.UpdatedAt
            };

            dbContext.Products.Add(ProductToCreate);

            bool success = await dbContext.SaveChangesAsync() > 0;

            if (success)
            {
                return Results.Created($"/notes/{ProductToCreate.Id}", ProductToCreate);
            }
            else
            {
                // 500 = internal server error.
                return Results.StatusCode(500);
            }
        });

        app.MapGet("/products", async (AppDbContext dbContext) =>
        {
            var products = await dbContext.Products
                .Select(p => new
                {
                    p.Id,
                    p.ProductId,
                    p.ProductName
                })
                .ToListAsync();

            return Results.Ok(products);
        });
    }
}

