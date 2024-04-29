CREATE TYPE orderStatusEnum AS ENUM ('pending', 'processing', 'shipped', 'delivered');

-- Old customerOrder table definition

CREATE TABLE customerOrder(
    orderID SERIAL Primary Key,
    orderStatus orderStatusEnum Default 'pending',
    createdDate TIMESTAMP Default CURRENT_TIMESTAMP,
    customerID INT[],
    FOREIGN KEY(customerID) REFERENCES customer(customerID)
); 
-- New customerOrder table definition 
CREATE TABLE customerOrder(
    orderID SERIAL Primary Key,
    orderStatus orderStatusEnum Default 'pending',
    createdDate TIMESTAMP Default CURRENT_TIMESTAMP,
    customerID INT[],
    FOREIGN KEY(userID) REFERENCES users(userID)
); 

-- The new Table: need to run this query before inserting records  
ALTER TABLE customerOrder
ADD payment JSONB;

ALTER TABLE customerOrder
DROP CONSTRAINT customerOrder_customerid_fkey; 

--Alter queries entered in pgAdmin instead of dropping the entire table 

ALTER TABLE customerOrder 
DROP COLUMN customerID; 

ALTER TABLE customerOrder
ADD COLUMN userID INT, 
ADD CONSTRAINT customer_userID_fkey FOREIGN KEY (userID) REFERENCES users(userID);

-- inserting some dummy data
INSERT INTO customerOrder (orderStatus, customerID, payment)
VALUES
('pending', 1, '{"method": "Credit Card", "amount": 50}'),
('processing', 2, '{"method": "Credit Card", "amount": 100}'),
('pending', 3, '{"method": "Credit Card", "amount": 55}'),
('shipped', 4, '{"method": "Credit Card", "amount": 80}'),
('pending', 5, '{"method": "Credit Card", "amount": 50}');

-- some testing queries

-- start with simple one for testing
SELECT * 
FROM customerOrder;

-- it wil return customer id with his order details
select t1.customerID, t1.orderID, t1.orderStatus, t2.productQuantity, t3.productName, t3.price
from customerOrder t1 
inner join orderDetails t2 on t1.orderID = t2.orderID
inner join product t3 on t2.productID=t3.productID;

