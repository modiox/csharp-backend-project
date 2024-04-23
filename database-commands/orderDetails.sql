CREATE TABLE orderDetails (
    orderDetails SERIAL PRIMARY KEY,
    orderID INT,
    productID INT,
    productQuantity INT NOT NULL,
    FOREIGN KEY (orderID) REFERENCES customerOrder (orderID),
    FOREIGN KEY (productID) REFERENCES product (productID)
);