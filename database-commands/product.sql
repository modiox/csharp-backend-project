CREATE TABLE Product(

ProductID serial PRIMARY KEY,
Name  VARCHAR(50) NOT NULL,
Description TEXT,
Quantity INT NOT NULL ,
Price DECIMAL(10, 2) NOT NULL,
CreatedDate  TIMESTAMP default ,
FOREIGN KEY (CategoryID) SERIAL REFERENCES Category(CategoryID) 
)