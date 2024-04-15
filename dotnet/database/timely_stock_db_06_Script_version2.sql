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

-- Create the users table
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

-- Create the inventories table
CREATE TABLE inventories 
(
	inventory_id INT IDENTITY(1,1) NOT NULL,
	user_id INT FOREIGN KEY REFERENCES users(user_id) NOT NULL, -- foreign key user id
	inventory_name VARCHAR(200) NOT NULL

	-- Primary Key
	CONSTRAINT [PK_inventories] PRIMARY KEY(inventory_id),

	-- All inventory names need to be unique
	CONSTRAINT [UQ_inventory_name] UNIQUE(inventory_name)
);

-- Create the categories table
CREATE TABLE categories
(
	category_id INT IDENTITY(1,1),
	user_id INT FOREIGN KEY REFERENCES users(user_id) NOT NULL, -- foreign key user id
	category_name NVARCHAR(200) NOT NULL,

	-- Primary key
	CONSTRAINT [PK_categories] PRIMARY KEY(category_id),

	-- All category names need to be unique
	CONSTRAINT [UQ_category_name] UNIQUE(category_name)
);

-- Create the suppliers table
CREATE TABLE suppliers
(
	supplier_id INT IDENTITY(1,1),
	user_id INT FOREIGN KEY REFERENCES users(user_id) NOT NULL, -- foreign key user id
	supplier_name NVARCHAR(200) NOT NULL,

	-- Primary Key
	CONSTRAINT [PK_suppliers] PRIMARY KEY(supplier_id),

	-- All supplier names need to be unique
	CONSTRAINT [UQ_supplier_name] UNIQUE(supplier_name)
);


-- Create items table
CREATE TABLE items 
(
	item_id INT IDENTITY(100,1),
	item_name NVARCHAR(200) NOT NULL,
	product_url VARCHAR(2083) NULL,
	sku_item_number INT NULL,
	price DECIMAL NULL,
	available_quantity INT NOT NULL,
	reorder_point INT NOT NULL,
	reorder_quantity INT NOT NULL,
	category_id INT NOT NULL FOREIGN KEY REFERENCES categories(category_id), -- foreign key category id
	supplier_id INT NOT NULL FOREIGN KEY REFERENCES suppliers(supplier_id),	-- foreign key supplier id

	-- Primary key 
	CONSTRAINT [PK_items] PRIMARY KEY(item_id)
);

-- Create the cross table inventory_items (many-to-many relationship between inventories table and items table)
CREATE TABLE inventory_items
(
	inventory_id INT NOT NULL,
	item_id INT NOT NULL,

	-- Compound primary key
	CONSTRAINT PK_inventory_items PRIMARY KEY(inventory_id, item_id),
);

-- Foreign for the inventory_items table
ALTER TABLE inventory_items ADD CONSTRAINT FK_inventory_items_inventories FOREIGN KEY(inventory_id) REFERENCES inventories(inventory_id);
ALTER TABLE inventory_items ADD CONSTRAINT FK_inventory_items_items FOREIGN KEY(item_id) REFERENCES items(item_id);

COMMIT;
--ROLLBACK TRANSACTION;