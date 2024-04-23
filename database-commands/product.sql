CREATE TABLE product(

productID serial PRIMARY KEY,
name  VARCHAR(50) NOT NULL,
description TEXT,
quantity INT NOT NULL ,
price DECIMAL(10, 2) NOT NULL,
createdDate  TIMESTAMP Default CURRENT_TIMESTAMP ,
FOREIGN KEY (categoryID) SERIAL REFERENCES category(categoryID) 
);