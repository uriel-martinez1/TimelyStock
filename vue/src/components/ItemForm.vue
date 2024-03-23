<template>
    <h1>This is the item form</h1>
    <form>
        <div>
            <label for="name">Item Name:</label>
            <input type="text" id="name" name="name" />
            
            <label>Product Url: </label>
            <input type="url" id="url" name="url"/>

            <label>SKU: </label>
            <input type="text" id="sku" name="sku"/>

            <label>Price: </label>
            <input type="text" id="price" name="price"/>

            <label>Available Quantity: </label>
            <input type="text" id="available_quantity" name="available_quantity" />
            <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->

            <label>Reorder Quantity: </label>
            <input type="text" id="reorder_quantity" name="reorder_quantity" />

            <label for="suppliers">Supplier: </label>
            <select id="suppliers" name="suppliers">
                <option value="Add">Add new supplier</option>
                <!--We will need to run a loop to grab all the existing suppliers - connect it to the get method in the back that aligns to get suppliers in the backend route-->
            </select>

            <label for="categoriess">Categories: </label>
            <select id="categories" name="categories">
                <option value="Add">Add new category</option>
                <!--We will need to run a loop to grab all the existing categories - connect it to the get method in the back that aligns to get categories in the backend route-->
            </select>
        </div>
        <button>Save</button>
        <button v-on:click="cancelForm">Cancel</button>
    </form>
</template>

<script>
// we are going to import suppliers service 
// we are going to import category service

// we will eventually need to create a method that adds an calls a form that will create a supplier
</script>


<style></style>