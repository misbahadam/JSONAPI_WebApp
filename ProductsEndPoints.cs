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
        }
    }
}
