# Warehouse Management

## Description
A .NET React.JS web app for warehouse management. Uses Entity Framework and MySQL database

## Use Case
1. Person can check warehouse inventory.
2. Person can add/delete items in warehouse inventory.
3. Person can edit item's attributes (name, price, quantity).
4. Person can check the history of item's price/quantity.
5. Person can register new users and assign them accesability rights for features in the web app.
6. Person can check orders.
7. Person can add, delete and change orders.
8. Person can complete orders and save payslips of the order inside the project directory.

## Services used
### Front-End
1. React.JS.
2. [Semantic UI React](https://react.semantic-ui.com/).
3. [AG Grid](https://www.ag-grid.com/react-data-grid/getting-started/).
4. [Axios](https://www.npmjs.com/package/react-axios).
5. Redux
### Back-End
.NET (C#) 7.
MySQL Entity Framework 7.

## DB scaffold commands
### Users
dotnet ef dbcontext scaffold "Server=localhost;Database=users;user=root;password=;" MySql.EntityFrameworkCore -o UsersDB -f
### Warehouse
dotnet ef dbcontext scaffold "Server=localhost;Database=warehouse;user=root;password=;" MySql.EntityFrameworkCore -o WarehouseDB -f
### Orders
dotnet ef dbcontext scaffold "Server=localhost;Database=orders;user=root;password=;" MySql.EntityFrameworkCore -o OrdersDB -f