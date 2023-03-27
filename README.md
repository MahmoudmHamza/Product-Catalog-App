# Product Catalog API

This is a RESTful API built using .NET Core and Entity Framework that allows you to manage a catalog of products.
Features
Products

- Create a new product
- Retrieve a list of all products
- Retrieve a single product by ID
- Update a product
- Delete a product

Categories

- Create a new category
- Retrieve a list of all categories
- Retrieve a single category by ID
- Update a category
- Delete a category

Orders

- Create a new order
- Retrieve a list of all orders
- Retrieve a single order by ID
- Update an order
- Delete an order

Product Ratings

- Add a new rating for a product
- Retrieve the average rating for a product

Product Search

- Search for product by name or category

Product Recommendation

- Retrieve recommended products for user based on history and purchased items

Getting Started

- Clone this repository: git clone https://github.com/your-username/product-catalog-api.git
- Open the project in Visual Studio or your preferred IDE
- Run the application

## API Endpoints
### Products
Get all products

    GET /product

Returns a list of all products.
Get a single product

    GET /product/{id}

Returns a single product by ID.
Create a new product

    POST /product

Creates a new product.
Update a product

    PUT /product/{id}

Updates an existing product.
Delete a product

    DELETE /product/{id}

Deletes an existing product.
Get all products of a specific category

    GET /product/category/{categoryId}

Returns all products by category ID.
### Categories
Get all categories

    GET /category

Returns a list of all categories.
Get a single category

    GET /category/{id}

Returns a single category by ID.
Create a new category

    POST /category

Creates a new category.
Update a category

    PUT /category/{id}

Updates an existing category.
Delete a category

    DELETE /category/{id}

Deletes an existing category.
### Orders
Get all orders

    GET /order

Returns a list of all orders.
Get a single order

    GET /order/{id}

Returns a single order by ID.
Create a new order

    POST /order

Creates a new order.
Update an order

    PUT /order/{id}

Updates an existing order.
Delete an order

    DELETE /order/{id}

Deletes an existing order.
### Product Ratings
Add a new rating for a product

    POST /product/{id}/ratings

Adds a new rating for a product.
Get the average rating for a product

    GET /product/{id}/ratings/average

Returns the average rating for a product.
### Product Search
Get all products by name

    GET /product/search

Returns all products by name.
### Product Recommendation
Get product recommendations based on user history and purchased items by user id

    GET /user/{id}/recommendations

Returns product recommendations by user id.
