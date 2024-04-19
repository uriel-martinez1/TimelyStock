<template>
    <form v-on:submit.prevent="submitForm">
        <div class="field">
            <div id="itemName">
                <label for="name">Item Name:</label>
                <input type="text" id="name" name="name" v-model="updatedItem.ItemName"/>
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
                <input type="text" id="price" name="price" v-model="updatedItem.Price" />
            </div>

            <div>
                <label>Available Quantity: </label>
                <input type="text" id="available_quantity" name="available_quantity" v-model="updatedItem.AvailableQuantity" />
                <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->
            </div>

            <div>
                <label>Reorder Point: </label>
                <input type="text" id="reorder_point" name="reorder_point" v-model="updatedItem.ReorderPoint" />
                <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->
            </div>

            <div>
                <label>Reorder Quantity: </label>
                <input type="text" id="reorder_quantity" name="reorder_quantity" v-model="updatedItem.ReorderQuantity" />
            </div>

            <div>
                <label for="suppliers">Supplier: </label>
                <select id="suppliers" name="suppliers" v-model="updatedItem.SupplierId" v-on:change="handleSupplierCategoryChange">
                    <option disabled selected>Select a supplier</option>
                    <!--This is where we display all the existing supplier-->
                    <option v-for="supplier in suppliers" v-bind:value="supplier.supplierId" v-bind:key="supplier.supplierId">
                        {{ supplier.supplierName }}
                    </option>
                    <!--If the user wants to add a new supplier, then they must select the add new supplier dropdown-->
                    <option value="addSupplier">Add new supplier</option>
                </select>
            </div>
            <!--This is the form for the new supplier-->
            <form v-if="showAddSupplier">
                <label for="supplierName">Supplier Name: </label>
                <input type="text" id="supplierName" name="supplierName" v-model="newSupplier.SupplierName"/>
                <button v-on:click.prevent="saveNewSupplier" :disabled="validate">Save</button>
                <button v-on:click="resetAddForm">Cancel</button>
            </form>

            <div>
                <label for="categoriess">Categories: </label>
                <select id="categories" name="categories" v-model="updatedItem.CategoryId" v-on:change="handleSupplierCategoryChange">
                    <option disabled selected>Select a category</option>
                    <!--This is where we display all the existing categories-->
                    <option v-for="category in categories" v-bind:value="category.categoryId" v-bind:key="category.categoryId">
                        {{ category.categoryName }}
                    </option>
                    <!--If the user wants to add a new supplier, then they must select the add new supplier dropdown-->
                    <option value="addCategory">Add new category</option>
                </select>
            </div>
            <!--This is the form for the new category-->
            <form v-if="showAddCategory">
                <label for="categoryName">Category Name: </label>
                <input type="text" id="categoryName" name="categoryName" v-model="newCategory.CategoryName"/>
                <button v-on:click.prevent="saveNewCategory" :disabled="validate">Save</button>
                <button v-on:click="resetAddForm">Cancel</button>
            </form>
    
        </div>
        <button>Save</button>
        <button v-on:click="cancelForm">Cancel</button>
    </form>
</template>

<script>
import categoriesService from "../services/CategoriesService";
import supplierServices from "../services/SuppliersServices";
import inventoryService from "../services/InventoriesServices";

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
                itemId: this.item.ItemId,
                itemName: this.item.ItemName,
                ProductUrl: this.item.ProductUrl,
                SkuItemNumber: this.item.SkuItemNumber,
                Price: this.item.Price,
                AvailableQuantity: this.item.AvailableQuantity,
                ReorderPoint: this.item.ReorderPoint,
                ReorderQuantity: this.item.ReorderQuantity,
                CategoryId: this.item.CategoryId,
                SupplierId: this.item.SupplierId,
            },
            showAddSupplier: false,
            showAddCategory: false,
            updatedItem: {},
            suppliers: [],
            categories: [],
            newSupplier: {
                SupplierName: "",
            },
            newCategory: {
                CategoryName: "",
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
                inventoryService.addItemByInventoryId(this.updatedItem, this.$route.params.inventoryId)
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
            supplierServices
            .addSupplier(this.newSupplier)
            .then((response) =>{
                console.log(response);
                // Update the updatedItem.SupplierId with the newly created Supplier Id 
                this.updatedItem.SupplierId = response.data.supplierId;
                this.fetchSuppliers();
                this.resetAddForm();
                this.showAddSupplier = false;
            })
        },
        saveNewCategory() {
            // we need the category service here for create category
            categoriesService
            .addCategory(this.newCategory)
            .then((response) => {
                console.log(response);
                this.updatedItem.CategoryId = response.data.categoryId;
                this.fetchCategories();
                this.resetAddForm();
                this.showAddCategory = false;
            })
        },
        resetAddForm() {
            this.showAddSupplier = false;
            this.newSupplier = {
                SupplierName: "",
            };
        },
        // this will handle the showing of the form for suppliers and categories
        handleSupplierCategoryChange(event) {
            if (event.target.value === "addSupplier") {
                this.showAddSupplier = true;
            } else if (event.target.value === "addCategory") {
                this.showAddCategory = true;
            }
        },
    },
};
</script>


<style></style>