Database Setup
This application requires a SQL database to function. Follow the steps below to set up the database:

Create a SQL Database:

Use any SQL database management tool (e.g., SQL Server Management Studio) to create a database named CustomerOrderManagement.
Create Tables:

The database should include the following tables:

Customers Table:
CREATE TABLE Customers (
    ID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Phone NVARCHAR(15)
);

Orders Table:
CREATE TABLE Orders (
    ID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(ID),
    OrderDate DATE,
    ProductName NVARCHAR(100)
);
Connection String:Update the connection string in the application to point to your database. The connection string is located in the App.config file under the <connectionStrings> section.
Test the Connection:

Ensure that the database is running and accessible from the application.
