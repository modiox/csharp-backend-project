CREATE TABLE product (
    productID SERIAL PRIMARY KEY,
    productName VARCHAR(50) NOT NULL,
    description TEXT,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    createdDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    categoryID INT,
    FOREIGN KEY (categoryID) REFERENCES category (categoryID) 
);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple iPhone X','The Apple iPhone X is a flagship smartphone featuring a stunning OLED display, Face ID facial recognition, dual rear cameras, and a powerful A11 Bionic chip.',5,4500,14);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple MacBook Pro','The Apple MacBook Pro is a high-performance laptop with a brilliant Retina display, Touch Bar for enhanced productivity, powerful processors, and ample storage for demanding tasks.',15,6000,15);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES ('Samsung Galaxy S21','The Samsung Galaxy S21 is a flagship smartphone that features a stunning Dynamic AMOLED display, powerful Exynos or Snapdragon processor, versatile camera system, and 5G connectivity.',13,3000,16);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple Watch Series 6','The Apple Watch Series 6 is a premium smartwatch with advanced health and fitness tracking capabilities, an always-on display, built-in GPS, and an array of stylish bands to choose from.',11,2000,2);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES ('Apple iPad Air','The Apple iPad Air is a sleek and versatile tablet that combines a stunning Retina display, powerful A14 Bionic chip, Touch ID fingerprint sensor, and support for the Apple Pencil.',10,1500,6);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES ('Samsung Galaxy Tab S7','The Samsung Galaxy Tab S7 is a premium Android tablet with a large and vibrant Super AMOLED display, powerful Snapdragon processor, S Pen support, and a long-lasting battery.',35,2000,31);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple AirPods Pro','The Apple AirPods Pro are premium wireless earbuds that offer active noise cancellation, a customizable fit for comfort, immersive sound quality, and seamless integration with Apple devices.',31,3000,9);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple iMac Pro','The Apple iMac Pro is a powerful all-in-one desktop computer designed for professional users, featuring a stunning 27-inch Retina 5K display, workstation-class performance, and advanced graphics capabilities.',28,1000,7);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES ('Samsung Galaxy Watch 4','The Samsung Galaxy Watch 4 is a sophisticated smartwatch that combines stylish design with advanced health and fitness tracking features, including heart rate monitoring, sleep tracking, and built-in GPS.',24,3500,10);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple HomePod','The Apple HomePod is a smart speaker that delivers high-fidelity audio, intelligent Siri voice assistance, and seamless integration with Apple Music, allowing you to control your home and enjoy immersive sound.',22,2700,50);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple Magic Keyboard','The Apple Magic Keyboard is a sleek and wireless keyboard with a scissor mechanism for precise typing, a rechargeable battery, and a built-in trackpad for effortless navigation.',20,1000,44);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Samsung QLED Q90T','The Samsung QLED Q90T is a high-end 4K smart TV with Quantum Dot technology, delivering vibrant and lifelike colors, impressive contrast, and an immersive viewing experience.',16,3000,16);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple TV 4K','The Apple TV 4K is a media streaming device that brings your favorite movies, TV shows, and apps to the big screen with stunning 4K HDR visuals, immersive sound, and the power of tvOS.',14,3200,13);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Apple AirTag','The Apple AirTag is a small and lightweight accessory that helps you keep track of your belongings. Attach an AirTag to your keys, wallet, or bag, and use the Find My app to locate them with ease.',10,2800,68);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Samsung Galaxy Buds Pro','The Samsung Galaxy Buds Pro are premium wireless earbuds that offer active noise cancellation, crystal-clear sound, customizable fit, and seamless integration with Samsung devices.',17,1900,39);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Huawei P40 Pro','The Huawei P40 Pro is a flagship smartphone that features a stunning OLED display, powerful Kirin processor, Leica quad-camera system, and innovative AI capabilities.',22,3400,36);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Samsung Odyssey G9','The Samsung Odyssey G9 is an ultrawide gaming monitor with a curved QLED display, high refresh rate, fast response time, and immersive HDR visuals for an enhanced gaming experience.',11,1250,16);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Samsung Galaxy Book Flex 2','The Samsung Galaxy Book Flex 2 is a convertible laptop that combines powerful performance, a vibrant QLED display, S Pen support, and long battery life in a sleek and portable design.)',18,3000,30);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Samsung SSD 980 Pro','The Samsung SSD 980 Pro is a high-speed solid-state drive that offers blazing-fast read and write speeds, excellent reliability, and ample storage capacity for faster data access and transfer.',19,3000,35);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Samsung SmartThings Hub','The Samsung SmartThings Hub is a central control hub for your smart home devices, allowing you to automate and control lights, thermostats, cameras, and more from a single app.',6,3000,28);
INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Samsung POWERbot R7040','The Samsung POWERbot R7040 is a robotic vacuum cleaner with powerful suction, intelligent mapping, and Wi-Fi connectivity, making it easy to keep your floors clean with minimal effort.',21,2700,16);
 INSERT INTO product(productName,description,quantity,price,categoryID)
 VALUES('Huawei Watch GT 2','The Huawei Watch GT 2 is a stylish smartwatch that offers advanced fitness tracking features, long battery life, and a variety of watch faces to suit your personal style.',5,1400,22);
 
 SELECT * FROM product;
 SELECT * FROM product WHERE productName='Apple';
 SELECT description FROM product WHERE quantity=10;
 SELECT * FROM product WHERE categoryID > 14;
 SELECT * FROM product WHERE categoryID BETWEEN 3 AND 18;
