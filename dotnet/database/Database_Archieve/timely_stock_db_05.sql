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
CREATE TABLE users 
(
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

CREATE TABLE inventories 
(
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
	product_url VARCHAR(2083) NULL,
	sku_item_number INT NULL,
	price DECIMAL NULL,
	available_quantity INT NOT NULL,
	reorder_quantity INT NOT NULL,
	category_id INT NOT NULL,
	supplier_id INT NOT NULL,

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


CREATE TABLE inventory_items
(
	inventory_id INT NOT NULL,
	item_id INT NOT NULL,

	-- Compound primary key
	CONSTRAINT PK_inventory_items PRIMARY KEY(inventory_id, item_id)
);

-- Foreign keys
ALTER TABLE items ADD CONSTRAINT FK_items_categories FOREIGN KEY(category_id) REFERENCES categories(category_id);
ALTER TABLE items ADD CONSTRAINT FK_items_suppliers FOREIGN KEY(supplier_id) REFERENCES suppliers(supplier_id);
ALTER TABLE inventories ADD CONSTRAINT FK_inventories_users FOREIGN KEY(user_id) REFERENCES users(user_id);
ALTER TABLE inventory_items ADD CONSTRAINT FK_inventory_items_inventories FOREIGN KEY(inventory_id) REFERENCES inventories(inventory_id);
ALTER TABLE inventory_items ADD CONSTRAINT FK_inventory_items_items FOREIGN KEY(item_id) REFERENCES items(item_id);

INSERT INTO users (username, password_hash, salt, user_role)
VALUES('Bob', 'hashed_password', 'salt_value', 'user');

-- we need to add suppliers
INSERT INTO suppliers(supplier_name) VALUES('PetSmart'), ('Petco'), ('Amazon'), ('Aldi');

-- we can now add departments
INSERT INTO categories(category_name) VALUES ('Pets'), ('Laundry'), ('Cleaning'), ('Paper products'), ('Toiletries'), ('Water');


-- Here is where we create the items
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id)
VALUES('Tuna - Purina Wet Cat Food', 'https://www.petsmart.com/cat/food-and-treats/wet-food/purina-pro-plan-complete-essentials-adult-wet-cat-food---antioxidants-high-protein-3-oz-5108685.html', 5108685, 1.41, 51, 24, (SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id)
VALUES('Sole & Vegetables - Purina Wet Cat Food', 'https://www.petsmart.com/cat/food-and-treats/wet-food/purina-pro-plan-complete-essentials-adult-wet-cat-food---antioxidants-high-protein-3-oz-5152985.html', 5152985, 1.41, 48, 24, (SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id)
VALUES('Blue Buffalo Dry Cat Food', 'https://www.petsmart.com/cat/food-and-treats/dry-food/blue-buffalo-wildernessandtrade--adult-dry-cat-food---grain-free-chicken-5173407.html', 5173405, 27.54, 2, 1, (SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id)
VALUES('Cat Litter - World''s Best', 'https://www.petsmart.com/cat/litter-and-waste-disposal/litter/worlds-best-andtrade-clumping-multi-cat-corn-cat-litter---lightweight-low-dust-natural-5178276.html', 5178276, 32.10, 3, 1,(SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));

-- We can add items to inventories
INSERT INTO inventories(user_id, inventory_name) VALUES(1, 'Home shelter inventory');

-- we can add items to inventory
INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Home shelter inventory'),(SELECT item_id FROM items WHERE item_name = 'Tuna - Purina Wet Cat Food'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Home shelter inventory'),(SELECT item_id FROM items WHERE item_name = 'Sole & Vegetables - Purina Wet Cat Food'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Home shelter inventory'),(SELECT item_id FROM items WHERE item_name = 'Blue Buffalo Dry Cat Food'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Home shelter inventory'),(SELECT item_id FROM items WHERE item_name = 'Cat Litter - World''s Best'));

--COMMIT;
ROLLBACK TRANSACTION;