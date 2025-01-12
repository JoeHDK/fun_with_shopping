IF OBJECT_ID('OrderLines', 'U') IS NOT NULL DROP TABLE OrderLines;
IF OBJECT_ID('Orders', 'U') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID('Cart', 'U') IS NOT NULL DROP TABLE Cart;
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
GO

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(1000) NULL,
    ImageUrl NVARCHAR(MAX) NULL,
    Category NVARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(5, 2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Cart (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(Id),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    SessionId NVARCHAR(100) NOT NULL,
    AddedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalPrice DECIMAL(15, 2) NOT NULL,
    SessionId NVARCHAR(100) NOT NULL
);

CREATE TABLE OrderLines (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(Id),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Price DECIMAL(10, 2) NOT NULL
);

-- Demo data
INSERT INTO Products (Name, Description, ImageUrl, Category, Price, Discount)
VALUES
('Laptop', 'A powerful laptop computer', 'https://example.com/laptop.jpg', 'Electronics', 15000.00, 10),
('T-shirt', 'A soft T-shirt', 'https://example.com/tshirt.jpg', 'Clothing', 200.00, 5),
('Headphones', 'Noise-cancelling headphones', 'https://example.com/headphones.jpg', 'Electronics', 1200.00, 15),
('Book', 'An interesting novel', 'https://example.com/book.jpg', 'Books', 150.00, 0);
GO

CREATE INDEX IX_Products_Name ON Products (Name);
CREATE INDEX IX_Products_Description ON Products (Description);
CREATE INDEX IX_Products_Category ON Products (Category);
