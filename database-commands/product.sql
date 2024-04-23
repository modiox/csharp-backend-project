CREATE TABLE Product(

ProductID serial PRIMARY KEY,
productName  VARCHAR(50) NOT NULL,
description TEXT,
quantity INT NOT NULL ,
price DECIMAL(10, 2) NOT NULL,
FOREIGN KEY (CategoryID) SERIAL REFERENCES Category(CategoryID),
CreatedDate  TIMESTAMP Default CURRENT_TIMESTAMP 
)
 INSERT INTO Product(productName,description,quantity,price,categoryID)
 VALUES
 ('Apple','MobilePhone white (256GB)',5,4500,1),
 ('Apple','Ipad Yello (512GB)',10,6000,2),
 ('Samsung','GalaxyTab Black (128GB)',6,3000,3);