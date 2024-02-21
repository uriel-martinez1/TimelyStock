USE [master];
GO

DROP DATABASE IF EXISTS PurrfectInventory;

-- Create the database
CREATE DATABASE PurrfectInventory;
GO

USE PurrfectInventory;
GO

-- Atomic build
BEGIN TRANSACTION;


-- Create the tables
CREATE TABLE items
(
	item_id INT IDENTITY(100,1),
	item_name NVARCHAR(200) NOT NULL,
	available_quantity INT NOT NULL,
	reorder_quantity INT NOT NULL,
	reorder_item BIT DEFAULT 0 NOT NULL,
	product_url VARCHAR(2083) NULL,
	item_number INT NULL,
	department_id int,
	supplier_id int,

	-- Primary key 
	CONSTRAINT [PK_items] PRIMARY KEY(item_id)
);

CREATE TABLE departments
(
	department_id INT IDENTITY(1,1),
	department_name NVARCHAR(200) NOT NULL,

	-- Primary key
	CONSTRAINT [PK_departments] PRIMARY KEY(department_id),

	-- All department names need to be unique
	CONSTRAINT [UC_name] UNIQUE(department_name)
);

CREATE TABLE suppliers
(
	supplier_id INT IDENTITY(1,1),
	supplier_name NVARCHAR(200) NOT NULL,

	-- Primary Key
	CONSTRAINT [PK_suppliers] PRIMARY KEY(supplier_id)
);


-- Foreign keys
ALTER TABLE items ADD CONSTRAINT FK_items_departments FOREIGN KEY(department_id) REFERENCES departments(department_id);
ALTER TABLE items ADD CONSTRAINT FK_items_suppliers FOREIGN KEY(supplier_id) REFERENCES suppliers(supplier_id);

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