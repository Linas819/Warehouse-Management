# Warehouse Management

## Description
A .NET React.JS web app for warehouse management. Uses Entity Framework and MySQL database

## Use Case
1. Person can check warehouse inventory.
2. Person can add/delete items in warehouse inventory.
3. Person can edit item's attributes (name, price, quantity).
4. Person can check the history of item's price/quantity.

## Services used
### Front-End
1. React.JS.
2. [Semantic UI React](https://react.semantic-ui.com/).
3. [AG Grid](https://www.ag-grid.com/react-data-grid/getting-started/).
4. [Axios](https://www.npmjs.com/package/react-axios).
### Back-End
.NET (C#) 7.
MySQL Entity Framework 7.

## To Do:
### Front-End
1. Develop the page to show all items in inventory.
2. create a form to add new items.
3. Create functions to edit or delete items.
### Back-End
Create database communication to achieve front-end functionalities.

## DB scaffold command

dotnet ef dbcontext scaffold "Server=localhost;Database=warehouse;user=root;password=;" MySql.EntityFrameworkCore -o WarehouseContext -f