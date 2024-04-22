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


--INSERT INTO Customer(Username,Email,Password,FirstName,LastName,phoneNumber,Address,DateOfBirth) 
--VALUES("arwa","arwa@gmail.com","123456" ,"arwa","alattas","0555555555","saudiArabia","22/22/2022");
    
