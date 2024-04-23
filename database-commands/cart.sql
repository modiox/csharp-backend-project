CREATE TABLE cart (
    cartID SERIAL PRIMARY KEY,
    productID INT,
    customerID INT, 
    FOREIGN KEY (productID) REFERENCES product (productID),
    FOREIGN KEY (customerID) REFERENCES customer (customerID)
);

