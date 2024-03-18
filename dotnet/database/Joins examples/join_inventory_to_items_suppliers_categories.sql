select * from inventories

select * from inventory_items

select * from items

select * from categories

select * from suppliers

SELECT item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, categories.category_name, suppliers.supplier_name 
  FROM inventories
  JOIN inventory_items ON inventory_items.inventory_id = inventories.inventory_id
  JOIN items ON items.item_id = inventory_items.item_id
  JOIN suppliers ON items.supplier_id = suppliers.supplier_id
  JOIN categories ON items.category_id = categories.category_id
 WHERE inventories.inventory_id = 2;

SELECT item_name, suppliers.supplier_name 
  FROM items
  JOIN suppliers ON items.supplier_id = suppliers.supplier_id
 WHERE suppliers.supplier_id = 1;