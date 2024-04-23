CREATE TYPE orderStatusEnum AS ENUM ('pending', 'processing', 'shipped', 'delivered');

CREATE TABLE customerOrder(
    orderID SERIAL Primary Key,
    orderStatus orderStatusEnum,
    createdDate TIMESTAMP Default CURRENT_TIMESTAMP,
    customerID INT,
    FOREIGN KEY(customerID) REFERENCES customer(customerID)
);

INSERT INTO customerOrder (orderStatus, customerID)
VALUES
('pending', 1),
('processing', 2),
('pending', 3),
('shipped', 4),
('pending', 5);

-- SELECT * 
-- FROM customerOrder;