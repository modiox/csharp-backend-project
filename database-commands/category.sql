CREATE TABLE category(
    categoryID SERIAL PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL,
    description TEXT
    );

INSERT INTO category(name, description)
VALUES
('Smart Phones','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Headphones','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Tablets','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Power Banks','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Laptops','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Electronics','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Accessories','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('TVs','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Video Games','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.'),
('Books','Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porttitor, est in molestie eleifend, justo purus rhoncus ante, tortor velit sit amet elit.');