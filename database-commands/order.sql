CREATE TABLE Order(
    orderID SERIAL Primary Key,
    orderStatus ENUM,
    createdDate TIMESTAMP Default CURRENT_TIMESTAMP,
    customerID INT,
    FOREIGN KEY(customerID) REFERENCES Customer(customerID)
)