# Product Catalog API

This is a RESTful API built using .NET Core and Entity Framework that allows you to manage a catalog of products.
Features
Products

    Create a new product
    Retrieve a list of all products
    Retrieve a single product by ID
    Update a product
    Delete a product

Categories

    Create a new category
    Retrieve a list of all categories
    Retrieve a single category by ID
    Update a category
    Delete a category

Orders

    Create a new order
    Retrieve a list of all orders
    Retrieve a single order by ID
    Update an order
    Delete an order

Product Ratings

    Add a new rating for a product
    Retrieve a list of all ratings for a product
    Retrieve the average rating for a product

Getting Started

    Clone this repository: git clone https://github.com/your-username/product-catalog-api.git
    Open the project in Visual Studio or your preferred IDE
    Run the application

API Endpoints
Products
Get all products

    GET /api/products

Returns a list of all products.
Get a single product

    GET /api/products/{id}

Returns a single product by ID.
Create a new product

    POST /api/products

Creates a new product.
Update a product

    PUT /api/products/{id}

Updates an existing product.
Delete a product

    DELETE /api/products/{id}

Deletes an existing product.
Categories
Get all categories

    GET /api/categories

Returns a list of all categories.
Get a single category

    GET /api/categories/{id}

Returns a single category by ID.
Create a new category

    POST /api/categories

Creates a new category.
Update a category

    PUT /api/categories/{id}

Updates an existing category.
Delete a category

    DELETE /api/categories/{id}

Deletes an existing category.
Orders
Get all orders

    GET /api/orders

Returns a list of all orders.
Get a single order

    GET /api/orders/{id}

Returns a single order by ID.
Create a new order

    POST /api/orders

Creates a new order.
Update an order

    PUT /api/orders/{id}

Updates an existing order.
Delete an order

    DELETE /api/orders/{id}

Deletes an existing order.
Product Ratings
Get all ratings for a product

    GET /api/products/{id}/ratings

Returns a list of all ratings for a product.
Add a new rating for a product

    POST /api/products/{id}/ratings

Adds a new rating for a product.
Get the average rating for a product

    GET /api/products/{id}/ratings/average

Returns the average rating for a product.
