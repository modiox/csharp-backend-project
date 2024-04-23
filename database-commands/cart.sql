CREATE TABLE Cart (
    cartID SERIAL PRIMARY KEY,
    FOREIGN KEY(productID) REFERENCES product(productID),
    FOREIGN KEY(customerID) REFERENCES customer(customerID)
);

-- INSERT INTO Cart(productID, customerID) VALUES(1, 1001),
--       (2, 1002),
--       (3, 1001);
-- DROP TABLE cart; -- TRUNCATE TABLE cart;