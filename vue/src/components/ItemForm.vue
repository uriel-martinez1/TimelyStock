<template>
    <form>
        <div class="field">
            <label for="name">Item Name:</label>
            <input type="text" id="name" name="name" v-bind:value="item.itemName" />
            
            <label>Product Url: </label>
            <input type="url" id="url" name="url" v-bind:value="item.itemUrl" />

            <label>SKU: </label>
            <input type="text" id="sku" name="sku" v-bind:value="item.itemSku" />

            <label>Price: </label>
            <input type="text" id="price" name="price" v-bind:value="item.itemPrice" />

            <label>Available Quantity: </label>
            <input type="text" id="available_quantity" name="available_quantity" v-bind:value="item.itemAvailableQty" />
            <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->

            <label>Reorder Quantity: </label>
            <input type="text" id="reorder_quantity" name="reorder_quantity" v-bind:value="item.itemReorderQty" />

            <label for="suppliers">Supplier: </label>
            <select id="suppliers" name="suppliers" v-for="supplier in suppliers" v-bind:key="supplier.supplierId">
                <!--We will need to run a loop to grab all the existing suppliers - connect it to the get method in the back that aligns to get suppliers in the backend route-->
                <option value="{{ supplier.supplierName }}">{{ supplier.supplierName }}</option>
                <option value="add">Add new supplier</option>
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
import categoriesService from "../services/CategoriesService";

export default {
    props: {
        item: {
            type: Object,
            required: true
        },
        supplier: {
            type: Object,
            required: true
        }
    },

    data() {
        return {
            editItem: {
                itemId: this.item.itemId,
                itemName: this.item.itemName,
                itemUrl: this.item.itemUrl,
                itemSku: this.item.itemSku,
                itemPrice: this.item.itemPrice,
                itemAvailableQty: this.item.itemAvailableQty,
                itemReorderQty: this.item.itemReorderQty,
                itemCategory: this.item.itemCategory,
                itemSupplier: this.item.itemSupplier,
            }
        }
    }

}
</script>


<style></style>