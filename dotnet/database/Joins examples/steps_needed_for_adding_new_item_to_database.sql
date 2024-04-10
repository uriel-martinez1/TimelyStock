SELECT items.item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id FROM items 
  JOIN inventory_items ON items.item_id = inventory_items.item_id
  JOIN inventories ON inventory_items.inventory_id = inventories.inventory_id 
 WHERE inventories.inventory_id = 2;

SELECT * FROM items

SELECT * FROM inventory_items

INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id)
OUTPUT inserted.item_id
VALUES (@itemName, @productUrl, @skuNumber, @price, @availableQty, @reorderQty, @categoryId, @supplierId)

-- we might need a sql statement that will join items and suppliers to grab its id
SELECT suppliers.supplier_id from suppliers
JOIN items ON suppliers.supplier_id = items.supplier_id
WHERE item_id = 100;

-- we need to update the cross table between the items and invetory table to connect or link the newly created items
-- item_id = 107
-- inventory_id = 2 

 --INSERT INTO inventory_items(inventory_id, item_id)
 --VALUES (2, 107);


 -- 1. To add a new item, when we first fill out the form
 -- 2. if the category does not exist
	-- 2A. We need to create the category
	-- 2B. We need to add the newly created category_id to the new item 
 -- 3. If the supplier does not exist 
	-- 3A. we need to create the supplier
	-- 3B. We need to add the newly created category_id to the new item 
 -- 4. We need to add the item into the items table
 -- 5. We then need to update the inventory_items cross table include and match 
 --	   the inventory_id with the new item_id