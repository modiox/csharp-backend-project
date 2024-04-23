CREATE TABLE orderDetails (
    orderDetailsID SERIAL PRIMARY KEY,
    orderID INT,
    productID INT,
    productQuantity INT NOT NULL,
    FOREIGN KEY (orderID) REFERENCES customerOrder (orderID),
    FOREIGN KEY (productID) REFERENCES product (productID)
);

INSERT INTO orderDetails(orderID, productID, productQuantity)
VALUES
(1, 4, 5),
(2, 5, 1),
(3, 6, 1),
(4, 4, 1),
(5, 5, 1);

-- ALTER TABLE orderDetails RENAME COLUMN orderDetails TO orderDetailsID;

-- SELECT *
-- FROM orderDetails;

-- SELECT o.*, p.productName, p,price
-- FROM orderDetails o
-- INNER JOIN product p on p.productID = o.productID;