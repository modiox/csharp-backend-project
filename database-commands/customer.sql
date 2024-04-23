CREATE TABLE customer( 
 customerID SERIAL PRIMARY KEY, 
 username VARCHAR(50) UNIQUE NOT NULL, 
 email VARCHAR(100) UNIQUE NOT NULL,
 password VARCHAR(32) NOT NULL, 
 firstName VARCHAR(20) NOT NULL,
 lastName VARCHAR(20), 
 phoneNumber VARCHAR(13) UNIQUE,
 address VARCHAR(255), 
 birthDate DATE
 );


INSERT INTO customer(username,email,password,firstName,lastName,phoneNumber,address,birthDate) 
VALUES
('Malek-55','Malek@gmail.com','123456' ,'Malek','Malek','0555555555','address','2022-12-02'),
('Arwa-22','arwa@gmail.com','123456' ,'arwa','arwa','0555557555','address','2022-12-02'),
('Turki-11','Turki@gmail.com','123456' ,'Turki','Turki','0553355555','address','2022-11-07'),
('Modi-33','Modi@gmail.com','123456' ,'Modi','Modi','0555255599','address','2022-12-02'),
('Reem-88','Reem@gmail.com','123456' ,'Reem','Reem','0557555555','address','2022-08-02');

    