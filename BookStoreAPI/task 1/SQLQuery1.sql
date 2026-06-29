CREATE DATABASE BookStore;
GO

USE BookStore;
GO

CREATE TABLE Categories
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500),
    CreatedAt DATETIME2
     DEFAULT GETDATE()
);


CREATE TABLE Authors
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Name NVARCHAR(100) NOT NULL,

    CreatedAt DATETIME2 DEFAULT GETDATE()
);

CREATE TABLE Customers
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    FirstName NVARCHAR(100) NOT NULL,

    LastName NVARCHAR(100) NOT NULL,

    Email NVARCHAR(255) NOT NULL UNIQUE,

    City NVARCHAR(100) NOT NULL,

    CreatedAt DATETIME2 DEFAULT GETDATE()
);

CREATE TABLE Books
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    Title NVARCHAR(200) NOT NULL,

    Price DECIMAL(10,2) NOT NULL,

    Stock INT NOT NULL,

    CategoryId INT NOT NULL,

    AuthorId INT NOT NULL,

    CreatedAt DATETIME2 DEFAULT GETDATE(),

    CONSTRAINT CK_Book_Price
        CHECK (Price > 0),

    CONSTRAINT CK_Book_Stock
        CHECK (Stock >= 0),

    CONSTRAINT FK_Book_Category
        FOREIGN KEY(CategoryId)
        REFERENCES Categories(Id),

    CONSTRAINT FK_Book_Author
        FOREIGN KEY(AuthorId)
        REFERENCES Authors(Id)
);

CREATE TABLE Purchases
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    CustomerId INT NOT NULL,

    PurchaseDate DATETIME2 DEFAULT GETDATE(),

    CONSTRAINT FK_Purchase_Customer
        FOREIGN KEY(CustomerId)
        REFERENCES Customers(Id)
);

CREATE TABLE PurchaseItems
(
    PurchaseId INT NOT NULL,

    BookId INT NOT NULL,

    Quantity INT NOT NULL,

    UnitPrice DECIMAL(10,2) NOT NULL,

    CONSTRAINT PK_PurchaseItems
        PRIMARY KEY(PurchaseId, BookId),

    CONSTRAINT FK_PurchaseItems_Purchase
        FOREIGN KEY(PurchaseId)
        REFERENCES Purchases(Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_PurchaseItems_Book
        FOREIGN KEY(BookId)
        REFERENCES Books(Id),

    CONSTRAINT CK_PurchaseItems_Quantity
        CHECK(Quantity > 0),

    CONSTRAINT CK_PurchaseItems_UnitPrice
        CHECK(UnitPrice > 0)
);
--/////////////////////////////////////////////////////////////////////////////////////////////////--
INSERT INTO Categories (Name, Description)
VALUES
('Programming', 'Programming books'),
('Database', 'Database books'),
('Artificial Intelligence', 'AI books'),
('Networking', 'Networking books'),
('Cyber Security', 'Security books');


INSERT INTO Authors (Name)
VALUES
('Robert C. Martin'),
('Martin Fowler'),
('Andrew Tanenbaum'),
('Thomas H. Cormen'),
('Ian Sommerville'),
('Charles Petzold');

INSERT INTO Customers (FirstName, LastName, Email, City)
VALUES
('Ahmed', 'Yasser', 'ahmed@gmail.com', 'Cairo'),
('Ali', 'Mohamed', 'ali@gmail.com', 'Cairo'),
('Sara', 'Hassan', 'sara@gmail.com', 'Alexandria'),
('Omar', 'Mahmoud', 'omar@gmail.com', 'Giza'),
('Mona', 'Ibrahim', 'mona@gmail.com', 'Cairo'),
('Youssef', 'Khaled', 'youssef@gmail.com', 'Mansoura'),
('Nour', 'Samir', 'nour@gmail.com', 'Alexandria'),
('Kareem', 'yasser', 'kareem@gmail.com', 'Tanta'),
('Laila', 'Adel', 'laila@gmail.com', 'Giza'),
('Hossam', 'Fathy', 'hossam@gmail.com', 'Cairo');

INSERT INTO Books
(Title, Price, Stock, CategoryId, AuthorId)
VALUES
('Clean Code',450,20,1,1),
('Clean Architecture',500,15,1,1),
('Refactoring',600,10,1,2),
('Patterns of Enterprise',550,12,1,2),
('Computer Networks',700,18,4,3),
('Modern Operating Systems',650,8,4,3),
('Introduction to Algorithms',900,30,1,4),
('Software Engineering',750,14,1,5),
('Code Complete',620,16,1,5),
('Programming Windows',400,11,1,6),
('SQL Fundamentals',350,25,2,2),
('Database Systems',580,17,2,4),
('AI Basics',800,13,3,5),
('Machine Learning',950,9,3,5),
('Ethical Hacking',850,22,5,6);

INSERT INTO Purchases (CustomerId, PurchaseDate)
VALUES
(1,'2026-01-10'),
(2,'2026-01-15'),
(3,'2026-02-03'),
(1,'2026-02-20'),
(5,'2026-03-05'),
(6,'2026-03-15'),
(2,'2026-04-02'),
(8,'2026-04-18'),
(4,'2026-05-01'),
(1,'2026-05-12'),
(5,'2026-06-08'),
(7,'2026-06-19');

INSERT INTO PurchaseItems
(PurchaseId, BookId, Quantity, UnitPrice)
VALUES
(1,1,1,450),
(1,11,2,350),

(2,7,1,900),
(2,5,1,700),

(3,3,1,600),
(3,12,1,580),

(4,1,2,450),
(4,2,1,500),

(5,13,1,800),
(5,14,1,950),

(6,5,2,700),
(6,15,1,850),

(7,1,1,450),
(7,8,1,750),

(8,7,1,900),
(8,11,1,350),

(9,15,2,850),

(10,1,3,450),
(10,3,1,600),

(11,6,1,650),
(11,13,2,800),

(12,7,1,900),
(12,9,2,620);
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task3--

SELECT *
FROM Books
ORDER BY Price DESC;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task4--
SELECT
UPPER(B.Title) AS TITLE,
LOWER(A.Name) AS AUTHORS
FROM Books B
INNER JOIN Authors A
ON B.AuthorId=A.Id;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task5--
SELECT
B.Title,
C.Name AS Category,
A.Name AS Author
FROM Books B
INNER JOIN Categories C
ON B.CategoryId=C.Id
INNER JOIN Authors A
ON B.AuthorId=A.Id;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task6--
SELECT
C.FirstName,
C.LastName,
COUNT(P.Id) AS TotalPurchases
FROM Customers C
LEFT JOIN Purchases P
ON C.Id=P.CustomerId
GROUP BY
C.FirstName,
C.LastName;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task7--

SELECT TOP 5
B.Title,SUM(PI.Quantity) AS TotalSold
FROM PurchaseItems PI
INNER JOIN Books B
ON PI.BookId = B.Id
GROUP BY B.Title
ORDER BY TotalSold DESC;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task8--
SELECT TOP 1 City,COUNT(*) AS TotalCustomers
FROM Customers
GROUP BY City
ORDER BY TotalCustomers DESC;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task9--

SELECT C.Name,COUNT(B.Id) AS TotalBooks
FROM Categories C
INNER JOIN Books B
ON C.Id = B.CategoryId
GROUP BY C.Name
HAVING COUNT(B.Id) > 5;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task10--

SELECT *
FROM Books
WHERE Price >
(
    SELECT AVG(Price)
    FROM Books
);
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task11--

SELECT C.FirstName,C.LastName
FROM Customers C
LEFT JOIN Purchases P
ON C.Id = P.CustomerId
WHERE P.Id IS NULL;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task12--

SELECT MONTH(P.PurchaseDate) AS MonthNumber,SUM(PI.Quantity * PI.UnitPrice) AS Revenue
FROM Purchases P
INNER JOIN PurchaseItems PI
ON P.Id = PI.PurchaseId
GROUP BY MONTH(P.PurchaseDate)
ORDER BY MonthNumber;
--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task13--
GO
CREATE VIEW vw_BookDetails
AS
SELECT B.Title,C.Name AS Category,A.Name AS Author,B.Price
FROM Books B
INNER JOIN Categories C
  ON B.CategoryId = C.Id
INNER JOIN Authors A
  ON B.AuthorId = A.Id;

--////////////////////////////////////////////////////////////////////////////////////////////////////////////--
--task14--
GO
CREATE PROCEDURE sp_GetCustomerPurchases
    @CustomerId INT
AS
BEGIN
SELECT P.Id AS PurchaseId,P.PurchaseDate,SUM(PI.Quantity * PI.UnitPrice) AS Total
FROM Purchases P
INNER JOIN PurchaseItems PI
  ON P.Id = PI.PurchaseId
  WHERE P.CustomerId = @CustomerId
  GROUP BY
        P.Id,
        P.PurchaseDate;
END;
