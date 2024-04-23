CREATE TYPE orderStatusEnum AS ENUM ('pending', 'processing', 'shipped', 'delivered');

CREATE TABLE customerOrder(
    orderID SERIAL Primary Key,
    orderStatus orderStatusEnum,
    createdDate TIMESTAMP Default CURRENT_TIMESTAMP,
    customerID INT,
    FOREIGN KEY(customerID) REFERENCES customer(customerID)
);