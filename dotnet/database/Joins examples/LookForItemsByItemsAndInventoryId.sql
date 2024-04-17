SELECT * FROM items

SELECT * FROM inventories

SELECT * FROM inventory_items

UPDATE items
SET item_name = 'updated name 2', product_url = 'www.thisisanotherstore.com', sku_item_number = '7654321', price = 2.99, available_quantity = 1, reorder_point = 5, reorder_quantity = 12, category_id = 2, supplier_id = 1
WHERE item_id = 109;

SELECT items.item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id FROM items
JOIN inventory_items ON items.item_id = inventory_items.item_id
JOIN inventories ON inventories.inventory_id = inventory_items.inventory_id
WHERE inventories.inventory_id = 1 AND items.item_id = 100;