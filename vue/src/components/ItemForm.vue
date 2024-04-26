<template>
    <form v-on:submit.prevent="submitForm">
        <div class="field">
            <div id="itemName">
                <label for="name">Item Name:</label>
                <input type="text" id="name" name="name" v-model="updatedItem.itemName" autocomplete="off"/>
            </div>

            <div id="productUrl">
                <label>Product Url: </label>
                <input type="url" id="url" name="url" v-model="updatedItem.productUrl" />
            </div>

            <div id="itemSku">
                <label>SKU: </label>
                <input type="text" id="sku" name="sku" v-model="updatedItem.skuItemNumber" />
            </div>

            <div>
                <label>Price: </label>
                <input type="text" id="price" name="price" v-model="updatedItem.price" />
            </div>

            <div>
                <label>Available Quantity: </label>
                <input type="text" id="available_quantity" name="available_quantity" v-model="updatedItem.availableQuantity" />
                <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->
            </div>

            <div>
                <label>Reorder Point: </label>
                <input type="text" id="reorder_point" name="reorder_point" v-model="updatedItem.reorderPoint" />
                <!--For the available quantity we are going to need a call some method that grabs all the avaialble quantity at the time of the call-->
            </div>

            <div>
                <label>Reorder Quantity: </label>
                <input type="text" id="reorder_quantity" name="reorder_quantity" v-model="updatedItem.reorderQuantity" />
            </div>

            <div>
                <label for="suppliers">Supplier: </label>
                <select id="suppliers" name="suppliers" v-model="updatedItem.supplierId" v-on:change="handleSupplierCategoryChange">
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
                <button v-on:click.prevent="saveNewSupplier" :disabled="validateSupplier">Save</button>
                <button v-on:click="resetAddForm">Cancel</button>
            </form>

            <div>
                <label for="categoriess">Categories: </label>
                <select id="categories" name="categories" v-model="updatedItem.categoryId" v-on:change="handleSupplierCategoryChange">
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
                <button v-on:click.prevent="saveNewCategory" :disabled="validateCategory">Save</button>
                <button v-on:click="resetAddForm">Cancel</button>
            </form>
    
        </div>
        <button>Save</button>
        <button v-on:click.prevent="cancelForm">Cancel</button>
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
            required: true,
        },
    },

    data() {
        return {
            updatedItem: {
                itemId: this.item.itemId,
                itemName: this.item.itemName,
                productUrl: this.item.productUrl,
                skuItemNumber: this.item.skuItemNumber,
                price: this.item.price,
                availableQuantity: this.item.availableQuantity,
                reorderPoint: this.item.reorderPoint,
                reorderQuantity: this.item.reorderQuantity, 
                categoryId: this.item.categoryId,
                supplierId: this.item.supplierId
            },
            showAddSupplier: false,
            showAddCategory: false,
            suppliers: [],
            categories: [],
            newSupplier: {
                SupplierName: "",
            },
            newCategory: {
                CategoryName: "",
            },
        };
    },
    mounted() {
        this.fetchData();
    },
    computed: {
        validateSupplier() {
            return !this.newSupplier.SupplierName;
        },
        validateCategory() {
            return !this.newCategory.CategoryName;
        }
    },
    methods: {
        // lets grab the suppliers and categories
        async fetchData() {
            try {
                const [suppliersResponse, categoriesResponse] = await Promise.all([
                    supplierServices.getSuppliers(),
                    categoriesService.getCategories()
                ]);
                this.suppliers = suppliersResponse.data;
                this.categories = categoriesResponse.data;
                console.log("This is where item should be null" + this.updatedItem)
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        },
        submitForm(){
            if (this.updatedItem.itemId === 0) {
                // Create new item 
                inventoryService.addItemByInventoryId(this.updatedItem, this.$route.params.inventoryId)
                .then((response) =>{
                    if(response.status === 201){
                        // if successful, lets go back to the inventory view
                        this.$router.push({name: 'InventoryView', params: {inventoryId: this.$route.params.inventoryId}});
                    }
                });
            } else {
                // edit existing item
                let inventoryId = this.$route.params.inventoryId;
                let itemId = this.$route.params.itemId;
                inventoryService.updateItemByInventoryId(inventoryId, itemId, this.updatedItem)
                .then((response) => {
                    if (response.status === 200) {
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
                this.fetchData();
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
                this.fetchData();
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
        cancelForm() {
            this.$router.back();
        },
    },
};
</script>


<style></style>