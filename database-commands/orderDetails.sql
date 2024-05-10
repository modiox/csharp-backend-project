
---- In the latest design, we didn't include this table, but rather created it using many-to-many relationship between product and order 


CREATE TABLE orderDetails (
    orderID INT,
    productID INT,
    productQuantity INT NOT NULL,
    FOREIGN KEY (orderID) REFERENCES order (orderID),
    FOREIGN KEY (productID) REFERENCES product (productID)
);


ALTER TABLE orderDetails
DROP COLUMN orderDetailsID;

-- inserting some dummy data
INSERT INTO orderDetails(orderID, productID, productQuantity)
VALUES
(1, 1, 5),
(2, 2, 1),
(3, 3, 1),
(4, 3, 1),
(5, 3, 1);


-- some queries for testing

SELECT *
FROM orderDetails;

SELECT o.*, p.productName, p,price
FROM orderDetails o
INNER JOIN product p on p.productID = o.productID;

-- here if we using an array for productID

SELECT p.*
FROM product p 
JOIN (
    SELECT UNNEST(productIDs) as productID
    FROM orderDetails
    WHERE orderDetailsID = 1 
) o ON p.productID = o.productID;