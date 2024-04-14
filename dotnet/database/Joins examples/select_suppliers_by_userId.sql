SELECT * from suppliers

SELECT * FROM users

SELECT DISTINCT suppliers.supplier_id, supplier_name FROM suppliers 
                JOIN items ON suppliers.supplier_id = items.supplier_id 
                JOIN inventory_items ON items.item_id = inventory_items.item_id 
                JOIN inventories ON inventory_items.inventory_id = inventories.inventory_id
                JOIN users ON inventories.user_id = users.user_id 
                WHERE users.user_id = 2;
