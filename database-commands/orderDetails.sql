CREATE TABLE orderDetails(
    orderDetails SERIAL Primary Key,
    orderID INT,
    productID INT,
    productQuantity INT NOT NULL,
    FOREIGN KEY(orderID) REFERENCES Order(orderID),
    FOREIGN KEY(productID) REFERENCES Product(productID)
)