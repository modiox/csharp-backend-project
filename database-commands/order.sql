CREATE TYPE orderStatusEnum AS ENUM ('pending', 'processing', 'shipped', 'delivered');


CREATE TABLE order(
    orderID SERIAL Primary Key,
    orderStatus orderStatusEnum Default 'pending',
    createdDate TIMESTAMP Default CURRENT_TIMESTAMP,
    userID INT[],
    FOREIGN KEY(userID) REFERENCES users(userID)
); 



-- inserting some dummy data
INSERT INTO order (orderStatus, userID, payment)
VALUES
('pending', 1, '{"method": "Credit Card", "amount": 50}'),
('processing', 2, '{"method": "Credit Card", "amount": 100}'),
('pending', 3, '{"method": "Credit Card", "amount": 55}'),
('shipped', 4, '{"method": "Credit Card", "amount": 80}'),
('pending', 5, '{"method": "Credit Card", "amount": 50}');

-- some testing queries

-- start with simple one for testing
SELECT * 
FROM order;

-- it wil return customer id with his order details
select t1.userID, t1.orderID, t1.orderStatus, t2.productQuantity, t3.productName, t3.price
from order t1 
inner join orderDetails t2 on t1.orderID = t2.orderID
inner join product t3 on t2.productID=t3.productID;

