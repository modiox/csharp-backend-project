CREATE TABLE cart (
    cartID SERIAL PRIMARY KEY,
    productID INT,
    customerID INT, 
    FOREIGN KEY (productID) REFERENCES product (productID),
    FOREIGN KEY (customerID) REFERENCES customer (customerID)
);
-- INSERT INTO Cart(productID, customerID) VALUES(1, 1001),
--       (2, 1002),
--       (3, 1001);
