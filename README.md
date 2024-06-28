# AgriEnergyConnect

##Description
This application is build as the prototype for a much larger application for farmers and agri culture enthusiasts. 
This prototype shows the basic design of the application as well ass limited functionality to ser ve as a proof of concept


## Installation

#### Clone the repository
```
git clone https://github.com/pineapple-pizza25/AgriEnergyConnect/new/main
cd your-repo
```

#### Restore dependancies
```
dotnet restore
```


## Setting up the database

```
CREATE DATABASE AgriEnergyConnect;

CREATE TABLE employee ( id VARCHAR(100) NOT NULL PRIMARY KEY, first_name VARCHAR(50) NOT NULL, last_name VARCHAR(50) NOT NULL, admin_address VARCHAR(100) NOT NULL, phone_number VARCHAR(20) NOT NULL, email VARCHAR(50) NOT NULL, date_of_birth DATE NOT NULL, gender VARCHAR(20) NOT NULL, );

CREATE TABLE farmer_category ( id INT NOT NULL PRIMARY KEY, category_name VARCHAR(50) NOT NULL, details TEXT NOT NULL );

CREATE TABLE farmer ( id VARCHAR(100) NOT NULL PRIMARY KEY, first_name VARCHAR(50) NOT NULL, last_name VARCHAR(50) NOT NULL, farmer_address VARCHAR(100) NOT NULL, phone_number VARCHAR(20) NOT NULL, email VARCHAR(50) NOT NULL, date_of_birth DATE NOT NULL, gender VARCHAR(20) NOT NULL, farmer_category_id INT FOREIGN KEY REFERENCES farmer_category(id) );

select * from farmer;

CREATE TABLE farm( id INT NOT NULL PRIMARY KEY, farmer_id VARCHAR(100) NOT NULL FOREIGN KEY REFERENCES farmer(id), farm_name VARCHAR(100) NOT NULL, farm_description TEXT, farm_address VARCHAR(200) NOT NULL, );

CREATE TABLE product_category ( id INT NOT NULL PRIMARY KEY, category_name VARCHAR(50) NOT NULL, details TEXT NOT NULL );

CREATE TABLE product ( id INT NOT NULL PRIMARY KEY, product_name VARCHAR(100) NOT NULL, details TEXT NULL, price DECIMAL(10, 2) NOT NULL, production_date DATE NOT NULL, quantity INT NOT NULL, unit VARCHAR(20) NOT NULL, expiration_date DATE, farmer_id VARCHAR(100) NOT NULL FOREIGN KEY REFERENCES farmer(id), product_category_id INT NOT NULL FOREIGN KEY REFERENCES product_category(id) ); select * from product; INSERT INTO employee (id, first_name, last_name, admin_address, phone_number, email, date_of_birth, gender) VALUES ('1', 'John', 'Doe', '123 Main St, Anytown, USA', '1234567890', 'john.doe@email.com', '1980-05-15', 'Male'), ('2', 'Jane', 'Smith', '456 Elm St, Somecity, USA', '9876543210', 'jane.smith@email.com', '1985-09-22', 'Female'); INSERT INTO farmer_category (id, category_name, details) VALUES (1, 'Crop Farmer', 'Farmers who primarily grow crops like grains, vegetables, and fruits.'), (2, 'Livestock Farmer', 'Farmers who raise livestock such as cattle, poultry, and sheep.'), (3, 'Mixed Farmer', 'Farmers who engage in both crop and livestock farming.');

INSERT INTO product_category (id, category_name, details) VALUES (1, 'Fruits and Vegetables', 'Fresh produce including fruits, vegetables, and herbs.'), (2, 'Meat and Poultry', 'Meat and poultry products from livestock farming.'), (3, 'Dairy and Eggs', 'Dairy products and eggs from livestock farming.'), (4, 'Grains and Legumes', 'Grains, cereals, and legumes grown as crops.');

INSERT INTO farmer (id, first_name, last_name, farmer_address, phone_number, email, date_of_birth, gender) VALUES ('1', 'John', 'Doe', '123 Main St, Anytown, USA', '1234567890', 'john.doe@email.com', '1980-05-15', 'Male'), ('2', 'Jane', 'Smith', '456 Elm St, Somecity, USA', '9876543210', 'jane.smith@email.com', '1985-09-22', 'Female'), ('3', 'Michael', 'Johnson', '789 Oak Rd, Anothertown, USA', '5555555555', 'michael.johnson@email.com', '1975-03-10', 'Male'), ('4', 'Emily', 'Davis', '321 Pine Ave, Mytown, USA', '1112223333', 'emily.davis@email.com', '1990-11-07', 'Female'), ('5', 'David', 'Wilson', '654 Maple Ln, Yourcity, USA', '4445556666', 'david.wilson@email.com', '1982-07-25', 'Male');

INSERT INTO product (id, product_name, details, price, production_date, quantity, unit, expiration_date, farmer_id, product_category_id) VALUES (1, 'Organic Apples', 'Fresh, crisp, and juicy apples grown without pesticides.', 2.99, '2023-05-01', 100, 'lb', '2023-06-30', 1, 1), (2, 'Grass-fed Beef', 'High-quality beef from cows raised on lush pastures.', 8.99, '2023-04-15', 50, 'lb', '2023-07-15', 2, 2), (3, 'Local Honey', 'Pure, raw honey harvested from local beekeepers.', 5.99, '2023-03-20', 25, 'jar', '2024-03-19', 3, 3), (4, 'Heirloom Tomatoes', 'Flavorful and colorful tomatoes from prized heirloom varieties.', 3.49, '2023-05-10', 75, 'lb', '2023-06-15', 1, 1), (5, 'Free-range Eggs', 'Farm-fresh eggs from happy, free-roaming chickens.', 4.99, '2023-05-05', 120, 'dozen', '2023-06-30', 2, 3);

INSERT INTO farm (id, farmer_id, farm_name, farm_description, farm_address) VALUES (1, 1, 'Sunny Meadows Farm', 'A family-owned farm specializing in organic produce.', '789 Oak Rd, Anothertown, USA'), (2, 2, 'Green Pastures Ranch', 'A sustainable ranch raising grass-fed cattle and free-range poultry.', '321 Pine Ave, Mytown, USA'), (3, 3, 'Harvest Haven', 'A mixed farm growing a variety of crops and raising livestock.', '654 Maple Ln, Yourcity, USA');

select * from product;
```



## Additional
It is also necessary to create an environment variable named: FirebaseAgriEnergy with the following value : AIzaSyDyx_VW7Y6u50LnuxD0tgpcrMxzdJp7TO4
