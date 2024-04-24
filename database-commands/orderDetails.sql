-- ! I think is better to use an array for productId but I do not know how we can reference it to an integer 
CREATE TABLE orderDetails (
    orderDetailsID SERIAL PRIMARY KEY,
    orderID INT,
    productID INT,
    productQuantity INT NOT NULL,
    FOREIGN KEY (orderID) REFERENCES customerOrder (orderID),
    FOREIGN KEY (productID) REFERENCES product (productID)
);


-- ! use this query to change the name of orderDetails to orderDetailsID
ALTER TABLE orderDetails 
RENAME COLUMN orderDetails TO orderDetailsID;

-- ! I believe we need to delete the orderDetailsID since we are not using it
-- this is the query for deleting the column
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