CREATE TABLE cart (
    productID INT,
    customerID INT, 
    FOREIGN KEY (productID) REFERENCES product (productID),
    FOREIGN KEY (customerID) REFERENCES customer (customerID)
);

ALTER TABLE cart
DROP CONSTRAINT cart_customerid_fkey; 

ALTER TABLE cart 
DROP COLUMN customerID; 

ALTER TABLE cart
ADD COLUMN userID INT, 
ADD CONSTRAINT cart_userID_fkey FOREIGN KEY (userID) REFERENCES users(userID);
