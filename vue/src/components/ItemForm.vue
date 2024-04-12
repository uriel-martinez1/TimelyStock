<template>
    <form v-on:submit.prevent="submitForm">
        <div class="field">
            <div id="itemName">
                <label for="name">Item Name:</label>
                <input type="text" id="name" name="name" v-model="updatedItem.itemName"/>
            </div>

            <div id="productUrl">
                <label>Product Url: </label>
                <input type="url" id="url" name="url" v-model="updatedItem.ProductUrl" />
            </div>

            <div id="itemSku">
                <label>SKU: </label>
                <input type="text" id="sku" name="sku" v-model="updatedItem.SkuItemNumber" />
            </div>

            <div>
                <label>Price: </label>
                <input type="text" id="price" name="price" v-model="updatedItem.price" />
            </div>

            <div>
                <label>Available Quantity: </label>
                <input type="text" id="available_quantity" name="available_quantity" v-model="updatedItem.AvailableQuantity" />
                <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->
            </div>

            <div>
                <label>Reorder Quantity: </label>
                <input type="text" id="reorder_quantity" name="reorder_quantity" v-model="updatedItem.ReorderQuantity" />
            </div>

            <div>
                <label for="suppliers">Supplier: </label>
                <select id="suppliers" name="suppliers" v-model="updatedItem.SupplierId">
                    <option disabled selected>Select a supplier</option>
                    <!--This is where we display all the existing supplier-->
                    <option v-for="supplier in suppliers" v-bind:value="supplier.supplierId" v-bind:key="supplier.supplierId">
                        {{ supplier.supplierName }}
                    </option>
                    <option value="add" v-if="!showAddSupplier" v-on:select="showAddSupplier = true">Add new supplier</option>
                    <form v-if="showAddSupplier">
                        <label for="supplierName">Supplier Name: </label>
                        <input type="text" id="supplierName" name="supplierName" v-model="newSupplier.name"/>
                        <button v-on:click.prevent="saveNewSupplier" :disabled="validate">Save</button>
                        <button v-on:click="resetAddForm">Cancel</button>
                    </form>
                </select>
            </div>

            <div>
                <label for="categoriess">Categories: </label>
                <select id="categories" name="categories" v-model="updatedItem.CategoryId">
                    <option disabled selected>Select a category</option>
                    <!--This is where we display all the existing categories-->
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
import categoriesService from "../services/CategoriesService";
import supplierServices from "../services/SuppliersServices";
import itemService from "../services/ItemService";

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
            showAddSupplier: false,
            updatedItem: {},
            suppliers: [],
            categories: [],
            newSupplier: {
                name: "",
            },
            newCategory: {
                name: "",
            }

        };
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
        },
        submitForm(){
            // create new item
            console.log(this.updatedItem);
            if (this.editItem.itemId === undefined || this.editItem.itemId === 0){
                console.log(this.updatedItem)
                // we should be able to grab the inventory id from the url 
                itemService.addItem(this.updatedItem, this.$route.params.inventoryId)
                .then(response =>{
                    if(response.status === 201){
                        // if successful, lets go back to the inventory view
                        this.$router.push({name: 'InventoryView', params: {inventoryId: this.$route.params.inventoryId}});
                    }
                })
            }
        },
        saveNewSupplier() {
            // we need the supplier service here for create supplier
            //supplierService
            //.addSupplier(this.newSupplier)
            //.then((response) =>{
                //this.fetchSuppliers();
                //this.resetAddForm();
            //})
        },
        resetAddForm() {
            this.showAddSupplier = false;
            this.newSupplier = {
                name: "",
            };
        }

    }
}
</script>


<style></style>