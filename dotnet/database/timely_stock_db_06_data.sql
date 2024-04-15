-- Atomic build
BEGIN TRANSACTION;

-- This is the data script that populates the categories, suppliers, inventory and items to the meelo user

-- double check that meelo user_id matches 1, if not change accordingly BEFORE running this script

-- Adding inventories
INSERT INTO inventories(user_id, inventory_name) VALUES (1, 'Cat Supplies'), (1, 'Office Supplies'), (1, 'Bathroom'), (1, 'Laundry Room');

-- Adding categories
INSERT INTO categories(user_id, category_name) VALUES (1, 'Pets'), (1, 'Laundry'), (1, 'Cleaning'), (1, 'Paper products'), (1, 'Toiletries');

-- Adding suppliers
INSERT INTO suppliers(user_id, supplier_name) VALUES(1, 'PetSmart'), (1, 'Petco'), (1, 'Amazon'), (1, 'Aldi');


-- Items

-- Creating items with Category = Pets and suppliers PetSmart/Petco
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Tuna - Purina Wet Cat Food', 'https://www.petsmart.com/cat/food-and-treats/wet-food/purina-pro-plan-complete-essentials-adult-wet-cat-food---antioxidants-high-protein-3-oz-5108685.html', 5108685, 1.41, 51, 12, 24, (SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Sole & Vegetables - Purina Wet Cat Food', 'https://www.petsmart.com/cat/food-and-treats/wet-food/purina-pro-plan-complete-essentials-adult-wet-cat-food---antioxidants-high-protein-3-oz-5152985.html', 5152985, 1.41, 48, 12, 24, (SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Blue Buffalo Dry Cat Food', 'https://www.petsmart.com/cat/food-and-treats/dry-food/blue-buffalo-wildernessandtrade--adult-dry-cat-food---grain-free-chicken-5173407.html', 5173405, 27.54, 2, 1, 1, (SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Cat Litter - World''s Best', 'https://www.petsmart.com/cat/litter-and-waste-disposal/litter/worlds-best-andtrade-clumping-multi-cat-corn-cat-litter---lightweight-low-dust-natural-5178276.html', 5178276, 32.10, 3, 1, 1,(SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'PetSmart'));
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Temptations Classics Savory Salmon Flavor, Cat Treats 30oz', 'https://www.petco.com/shop/en/petcostore/product/temptations-classic-crunchy-and-soft-cat-treats-savory-salmon-flavor-30-oz-3875304?cm_mmc=PSH%7CGGL%7CCMB%7CSBU02%7CPM%7C0%7Cwrv5258EgPZ8v7qXH7GfCW%7C%7C%7C0%7C0%7C%7C%7C20704176874&gad_source=1&gclid=Cj0KCQjwq86wBhDiARIsAJhuphmWdI1y96m7TwsgjRTkcIEHU-0uN8mGboNhmPJhBEtdTi_1VoydUjsaAg7oEALw_wcB&gclsrc=aw.ds', 3875304, 15.48, 0, 1, 2,(SELECT category_id FROM categories WHERE category_name = 'Pets'), (SELECT supplier_id from suppliers WHERE supplier_name = 'Petco'));


-- Creating items with category = Laundry and supplier = Amazon
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Gain Detergent 1.44 Gallon', 'https://www.amazon.com/Gain-Laundry-Detergent-Original-Compatible/dp/B0CD1DWZXW/ref=sr_1_3_sspa?crid=3C9UJ054CTO2S&dib=eyJ2IjoiMSJ9.c37Osy6_fwX_Q9wpsCMriTs2e-teZn77EXzPSLyY7blYISWh5GtyMA6PQbgjc3H6t5BVjWbOW6itwxTDypuLCj-JCD8i7Nhr9WvqRtyBzaavQwANpBTtFznrsgJs9e6CjLpbwMWMkiKYUkmVfPSeiqcKvpaKU8S2Bw3wWj7PXBF0-G10qUEAtECtf9lw9dYJ3WeGP0MRERN5LWkK4pFK6MHFF4bYG_Yn67znuclIllufqQuHNhq6cc1mniAiTh-42XevcjHWVnEbPthQmhfyv0rrCNXZUO8PmV23xbFtBRM.w3QXVxRD3D6WC_7hsC5mAf-83EJJ_bn6rqnk6hENu5c&dib_tag=se&keywords=laundry%2Bdetergent&qid=1713137097&rdc=1&sprefix=laun%2Caps%2C136&sr=8-3-spons&sp_csd=d2lkZ2V0TmFtZT1zcF9hdGY&th=1', 30772080085, 17.99, 0, 1, 1, (SELECT category_id FROM categories WHERE category_name = 'Laundry'), (SELECT supplier_id from suppliers WHERE supplier_name = 'Amazon'));

-- Creating items with category = Cleaning and supplier = Aldi
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Fabuloso Multi-Purpose Cleaner', 'https://www.aldi.us/products/household-essentials/soap-cleaner/detail/ps/p/fabuloso-multi-purpose-cleaner/', 46491, 2.98, 0, 1, 2, (SELECT category_id FROM categories WHERE category_name = 'Cleaning'), (SELECT supplier_id from suppliers WHERE supplier_name = 'Aldi'));

-- Creating items with category = Paper products and supplier = Amazon
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('HP Printer Paper - 1 Ream (500 Sheets)', 'https://www.amazon.com/HP-Printer-Paper-Premium32-Letter/dp/B000099O2W/ref=sr_1_2_sspa?c=ts&dib=eyJ2IjoiMSJ9.H3R2WcrEqjM6ipu3nB2BiCqktYqFkgVNn3GddYW3V4U10795LHRimqVqMWS2RzcXdDvh2nwMr2EteLH0vL_3XHJJ4FVsGU8AHiI7Lr0VStaLSu6BbDRIUa6d90nawNge78Zg1sSCKbtrXaT_ppFVtiuV96GwwGfQhIUaw9qHy7G2RzbYSyo8aKW68BTWZMybEpnsyyA8G4weLgcmO6KjUkTgKYvwnHnueaD6qlBKyf4gZDFhMQDZVEJOXL1r4mtICs36sNyHwKAJPRSdo_pwXm64ufotu4o6Zscc93O-GuI._9bo4j-6wcw9suVtY1eWXzndFAg1xCbvZxWNeU2pIXU&dib_tag=se&keywords=Paper+%26+Printable+Media&qid=1713136259&s=office-products&sr=1-2-spons&ts_id=1069664&sp_csd=d2lkZ2V0TmFtZT1zcF9hdGY&psc=1', 113100, 19.98, 0, 1, 2, (SELECT category_id FROM categories WHERE category_name = 'Paper products'), (SELECT supplier_id from suppliers WHERE supplier_name = 'Amazon'));

-- Creating items with category = Toiletries and supplier = Aldi
INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id)
VALUES('Crest Complete Plus Extra Whitening Toothpaste', 'https://www.aldi.us/products/personal-care/oral-care/detail/ps/p/crest-complete-plus-extra-whitening-or-scope-tooth/', 803482, 3.24, 0, 1, 3, (SELECT category_id FROM categories WHERE category_name = 'Toiletries'), (SELECT supplier_id from suppliers WHERE supplier_name = 'Aldi'));



-- Add items to Cat Supplies inventory
INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Cat Supplies'),(SELECT item_id FROM items WHERE item_name = 'Tuna - Purina Wet Cat Food'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Cat Supplies'),(SELECT item_id FROM items WHERE item_name = 'Sole & Vegetables - Purina Wet Cat Food'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Cat Supplies'),(SELECT item_id FROM items WHERE item_name = 'Blue Buffalo Dry Cat Food'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Cat Supplies'),(SELECT item_id FROM items WHERE item_name = 'Cat Litter - World''s Best'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Cat Supplies'),(SELECT item_id FROM items WHERE item_name = 'Temptations Classics Savory Salmon Flavor, Cat Treats 30oz'));

-- Add items to Office Supplies inventory
INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Office Supplies'),(SELECT item_id FROM items WHERE item_name = 'HP Printer Paper - 1 Ream (500 Sheets)'));

-- Add items to Bathroom inventory
INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Bathroom'),(SELECT item_id FROM items WHERE item_name = 'Fabuloso Multi-Purpose Cleaner'));

INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Bathroom'),(SELECT item_id FROM items WHERE item_name = 'Crest Complete Plus Extra Whitening Toothpaste'));

-- Add items to Laundry Room inventory
INSERT INTO inventory_items(inventory_id, item_id)
VALUES((SELECT inventory_id FROM inventories WHERE inventory_name = 'Laundry Room'),(SELECT item_id FROM items WHERE item_name = 'Gain Detergent 1.44 Gallon'));

COMMIT;
--ROLLBACK TRANSACTION;