CREATE TABLE Customer( 
 CustomerID SERIAL PRIMARY KEY, 
 Username VARCHAR(50) UNIQUE NOT NULL, 
 Email VARCHAR(100) UNIQUE NOT NULL,
 Password VARCHAR(32) NOT NULL, 
 FirstName VARCHAR(20) NOT NULL,
 LastName VARCHAR(20), 
 phoneNumber VARCHAR(13) UNIQUE,
 Address VARCHAR(255), 
 DateOfBirth DATE
 );


-- INSERT INTO Customer(Username,Email,Password,FirstName,LastName,phoneNumber,Address,DateOfBirth) 
-- VALUES
-- ("Malek-55","Malek@gmail.com","123456" ,"Malek","Malek","0555555555","address","2022-12-02"),
-- ("Arwa-22","arwa@gmail.com","123456" ,"arwa","arwa","0555555555","address","2022-12-02"),
-- ("Turki-11","Turki@gmail.com","123456" ,"Turki","Turki","0553355555","address","2022-11-07"),
-- ("Modi-33","Modi@gmail.com","123456" ,"Modi","Modi","0555555599","address","2022-12-02"),
-- ("Reem-88","Reem@gmail.com","123456" ,"Reem","Reem","0557555555","address","2022-08-02")
-- ;
    
