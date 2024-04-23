CREATE TABLE product (
    productID SERIAL PRIMARY KEY,
    productName VARCHAR(50) NOT NULL,
    description TEXT,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    categoryID INT,
    FOREIGN KEY (categoryID) REFERENCES category (categoryID) 
);