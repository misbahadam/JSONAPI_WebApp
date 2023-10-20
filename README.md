# JSONAPI_WebApp
A JSON API developed on ASP.NET Core Web related to Products

- Provide an endpoint which responds with the servertime in ISO8601 format
- Provide an endpoint which queries the URL “https://jsonplaceholder.typicode.com/posts”
and responds with an array of only the posts which contain the word “minima” anywhere
in the body. Maintain the sorting of the array.
- Create a database containing a “products” table with the fields
    - “id” (this is an auto increment field)
    - “product_id” (unique)
    - “product_name”
    - “stock_available”
    - “created_at” (default of CURRENT_TIMESTAMP)
    - “updated_at” (default of CURRENT_TIMESTAMP and set a triggerfor ON UPDATE CURRENT_TIMESTAMP or update this field in your Code).
- Provide an endpoint which queries the database and retrieves all available products from
the “products” table. It should respond with the data but omitting the fields
“stock_available”, “created_at” and “updated_at”.
- Provide an endpoint which receives a POST Request with the fields “product_id” and
“quantity” and responds whether or not the order could be fulfilled.
