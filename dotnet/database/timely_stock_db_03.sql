USE [master];
GO

DROP DATABASE IF EXISTS TimelyStock;

-- Create the database
CREATE DATABASE TimelyStock;
GO

USE TimelyStock;
GO

-- Atomic build
BEGIN TRANSACTION;

-- Create the tables
CREATE TABLE users (
	user_id INT IDENTITY(1,1) NOT NULL,
	username VARCHAR(50) NOT NULL,
	password_hash VARCHAR(200) NOT NULL,
	salt VARCHAR(200) NOT NULL,
	user_role VARCHAR(50) NOT NULL,

	-- Primary Key
	CONSTRAINT [PK_users] PRIMARY KEY(user_id),

	-- All usernames need to be unique
	CONSTRAINT [UQ_username] UNIQUE(username)
);

CREATE TABLE inventories (
	inventory_id INT IDENTITY(1,1) NOT NULL,
	user_id INT,
	inventory_name VARCHAR(200) NOT NULL

	-- Primary Key
	CONSTRAINT [PK_inventories] PRIMARY KEY(inventory_id)

)

CREATE TABLE items
(
	item_id INT IDENTITY(100,1),
	item_name NVARCHAR(200) NOT NULL,
	available_quantity INT NOT NULL,
	reorder_quantity INT NOT NULL,
	product_url VARCHAR(2083) NULL,
	sku_item_number INT NULL,
	price DECIMAL NULL,
	category_id INT,
	supplier_id INT,

	-- Primary key 
	CONSTRAINT [PK_items] PRIMARY KEY(item_id)
);
 

CREATE TABLE categories
(
	category_id INT IDENTITY(1,1),
	category_name NVARCHAR(200) NOT NULL,

	-- Primary key
	CONSTRAINT [PK_categories] PRIMARY KEY(category_id),

	-- All department names need to be unique
	CONSTRAINT [UQ_name] UNIQUE(category_name)
);

CREATE TABLE suppliers
(
	supplier_id INT IDENTITY(1,1),
	supplier_name NVARCHAR(200) NOT NULL,

	-- Primary Key
	CONSTRAINT [PK_suppliers] PRIMARY KEY(supplier_id)
);


-- Foreign keys
ALTER TABLE items ADD CONSTRAINT FK_items_categories FOREIGN KEY(category_id) REFERENCES categories(category_id);
ALTER TABLE items ADD CONSTRAINT FK_items_suppliers FOREIGN KEY(supplier_id) REFERENCES suppliers(supplier_id);
ALTER TABLE inventories ADD CONSTRAINT FK_inventories_users FOREIGN KEY(user_id) REFERENCES users(user_id);

-- Adding data here 

-- first we need to add suppliers
INSERT INTO suppliers(supplier_name) VALUES('PetSmart'), ('Petco'), ('Amazon'), ('Aldi');

-- we can now add departments
INSERT INTO departments(department_name) VALUES ('Pets'), ('Laundry'), ('Cleaning'), ('Paper products'), ('Toiletries'), ('Water');


-- Here is where we create the items
INSERT INTO items(item_name, available_quantity, reorder_quantity, product_url, item_number, department_id, supplier_id)
VALUES('Tuna - Purina Wet Cat Food', 49, 24, 'https://www.petsmart.com/cat/food-and-treats/wet-food/purina-pro-plan-complete-essentials-adult-wet-cat-food---antioxidants-high-protein-3-oz-5108685.html', 5108685, (SELECT department_id FROM departments WHERE department_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, available_quantity, reorder_quantity, product_url, item_number, department_id, supplier_id)
VALUES('Sole & Vegetables - Purina Wet Cat Food', 48, 24, 'https://www.petsmart.com/cat/food-and-treats/wet-food/purina-pro-plan-complete-essentials-adult-wet-cat-food---antioxidants-high-protein-3-oz-5152985.html', 5152985, (SELECT department_id FROM departments WHERE department_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, available_quantity, reorder_quantity, product_url, item_number, department_id, supplier_id)
VALUES('Blue Buffalo Dry Cat Food', 2, 1, 'https://www.petsmart.com/cat/food-and-treats/dry-food/blue-buffalo-wildernessandtrade--adult-dry-cat-food---grain-free-chicken-5173407.html', 5173405
, (SELECT department_id FROM departments WHERE department_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, available_quantity, reorder_quantity, product_url, item_number, department_id, supplier_id)
VALUES('Cat Litter - World''s Best', 3, 1, 'https://www.petsmart.com/cat/litter-and-waste-disposal/litter/worlds-best-andtrade-clumping-multi-cat-corn-cat-litter---lightweight-low-dust-natural-5178276.html', 5178276
, (SELECT department_id FROM departments WHERE department_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));

COMMIT;
--ROLLBACK TRANSACTION;