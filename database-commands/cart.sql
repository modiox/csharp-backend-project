

CREATE TABLE cart (
    productID INT,
    userID INT, 
    FOREIGN KEY (productID) REFERENCES product (productID),
    FOREIGN KEY (userID) REFERENCES users (userID)
);


