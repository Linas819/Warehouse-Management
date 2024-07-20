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
1. ~Develop the page to show all items in inventory.~
2. ~Create a form to add new items.~
3. ~Create functions to edit or delete items.~
4. ~Make Quantity and Price fields editable to change database data~
5. ~Show Quantiyt and Price changes in the Product View Modal~
6. ~Show a specific number of changes for Price/Quantity~
7. ~Create Log in page and menu~
8. Create orders page
9. Create order creation and assigning page
10. Create order payslip generation
11. ~Create an error modal~
12. ~Create functionality based on user access~
13. Create registration page
### Back-End
1. ~Create database communication to achieve front-end functionalities.~
2. ~Update database of any changes for Item's price/quantity~
3. ~Log In authentication~
4. ~Add Users database~
5. Create User back-end functionaity (Log in, register, assign rights)
6. Add orders database
7. Add order creation functions.
8. Add Payslip pdf generation and saving
9. ~Set access and send back to Fornt-End~
10. Create registration to back-end

## DB scaffold commands
### Warehouse
dotnet ef dbcontext scaffold "Server=localhost;Database=warehouse;user=root;password=;" MySql.EntityFrameworkCore -o WarehouseDB -f
### Users
dotnet ef dbcontext scaffold "Server=localhost;Database=users;user=root;password=;" MySql.EntityFrameworkCore -o UsersDB -f