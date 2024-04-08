<template>
    <form>
        <div class="field">
            <div id="itemName">
                <label for="name">Item Name:</label>
                <input type="text" id="name" name="name" v-bind:value="item.itemName" />
            </div>

            <div id="productUrl">
                <label>Product Url: </label>
                <input type="url" id="url" name="url" v-bind:value="item.itemUrl" />
            </div>

            <div id="itemSku">
                <label>SKU: </label>
                <input type="text" id="sku" name="sku" v-bind:value="item.itemSku" />
            </div>

            <div>
                <label>Price: </label>
                <input type="text" id="price" name="price" v-bind:value="item.itemPrice" />
            </div>

            <div>
                <label>Available Quantity: </label>
                <input type="text" id="available_quantity" name="available_quantity" v-bind:value="item.itemAvailableQty" />
                <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->
            </div>

            <div>
                <label>Reorder Quantity: </label>
                <input type="text" id="reorder_quantity" name="reorder_quantity" v-bind:value="item.itemReorderQty" />
            </div>

            <div>
                <label for="suppliers">Supplier: </label>
                <select id="suppliers" name="suppliers">
                    <option disabled selected>Select a supplier</option>
                    <!--This is where we display all the existing supplier-->
                    <option v-for="supplier in suppliers" v-bind:value="supplier.supplierId" v-bind:key="supplier.supplierId">
                        {{ supplier.supplierName }}
                    </option>
                    <option value="add">Add new supplier</option>
                </select>
            </div>

            <div>
                <label for="categoriess">Categories: </label>
                <select id="categories" name="categories">
                    <option disabled selected>Select a category</option>
                    <!--This is where we display all the existing supplier-->
                    <option v-for="category in categories" v-bind:value="category.categoryId" v-bind:key="category.categoryId">
                        {{ category.categoryName }}
                    </option>
                    <option value="Add">Add new category</option>
                </select>
            </div>
    
        </div>
        <button>Save</button>
        <button v-on:click="cancelForm">Cancel</button>
    </form>
</template>

<script>
// we are going to import suppliers service 
// we are going to import category service
import categoriesService from "../services/CategoriesService";
import supplierServices from "../services/SuppliersServices";

export default {
    props: {
        item: {
            type: Object,
            required: true
        },
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
            },
            suppliers: [],
            categories: []

        }
    },
    mounted() {
        // fetch suppliers when the component is mounted
        this.fetchSuppliers();
        this.fetchCategories();
    },

    methods: {
        async fetchSuppliers() {
            try {
                const response = await supplierServices.getSuppliers();
                console.log(response.data);
                this.suppliers = response.data;
            } catch (error) {
                console.error("Error fetching suppliers:", error);
            }
        },
        async fetchCategories() {
            try {
                const response = await categoriesService.getCategories();
                console.log(response.data);
                this.categories = response.data;
            } catch (error) {
                console.error("Error fetching categories:", error);
            }
        }
    }
}
</script>


<style></style>