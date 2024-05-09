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
                <!--<button>Delete</button>-->
            </div>
            <!--This is the form for the new supplier-->
            <form v-if="showAddSupplier">
                <label for="supplierName">Supplier Name: </label>
                <input type="text" id="supplierName" name="supplierName" v-model="newSupplier.SupplierName"/>
                <button v-on:click.prevent="saveNewSupplier" :disabled="validateSupplier">Save</button>
                <button v-on:click="resetAddForm">Cancel</button>
            </form>

            <div>
                <label for="categoriess">Category: </label>
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

    <div v-if="notification" class="notification" :class="notification.type">
        {{ notification.message }}
    </div>
    
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
            latestSupplierId: 0,
            latestCategoryId: 0,
            submitNotification: null,
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
        },
        notification() {
            return this.$store.state.notification;
        },
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
                //console.log("This is where item should be null" + this.updatedItem)
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
                        // we will add a notification for when an item is added
                        this.$store.commit(
                            'SET_NOTIFICATION',
                            {
                                message: 'A new item was added to the inventory.',
                                type: 'success'
                            }
                        ); 
                        // if successful, lets go back to the inventory view
                        this.$router.push({name: 'InventoryView', params: {inventoryId: this.$route.params.inventoryId}});
                    }
                })
                // Its a callback function -- error is the object and we are going to call the handleErrorResponse method
                .catch (error => {
                    // we will add the handleErrorResponse method here
                    this.handleErrorResponse(error, 'adding');
                });
            } else {
                // edit existing item
                let inventoryId = this.$route.params.inventoryId;
                let itemId = this.$route.params.itemId;
                inventoryService.updateItemByInventoryId(inventoryId, itemId, this.updatedItem)
                .then((response) => {
                    if (response.status === 200) {
                        //lets send a notification when the item has been updated
                        this.$store.commit(
                            'SET_NOTIFICATION',
                            {
                                message: `${this.updatedItem.itemName} was updated.`,
                                type: 'success'
                            }
                        );
                        this.$router.push({name: 'InventoryView', params: {inventoryId: this.$route.params.inventoryId}});
                    }
                })
                .catch (error => {
                    // handle error for updating item
                    this.handleErrorResponse(error, 'updating');
                });
            }
        },
        saveNewSupplier() {
            // we need the supplier service here for create supplier
            supplierServices
            .addSupplier(this.newSupplier)
            .then((response) =>{
                // Update the updatedItem.SupplierId with the newly created Supplier Id 
                this.updatedItem.supplierId = response.data.supplierId;
                // Update the latestCategoryId to the newly created Category Id
                this.latestSupplierId = response.data.supplierId;

                this.fetchData().then(() => {
                    this.resetAddForm();
                    this.showAddSupplier = false;
                });
                // send notification
                this.$store.commit(
                    'SET_NOTIFICATION', 
                    {
                        message: 'New Supplier created successfully.',
                        type: 'success'
                    }
                );
            })
            .catch ((error) => {
                console.error("Error adding supplier", error);
                // send notification
                this.$store.commit(
                    'SET_NOTIFICATION',
                    {
                        message: 'Failed to create new Supplier. Please try again.',
                        type: 'error'
                    }
                );
            });
        },
        saveNewCategory() {
            // we need the category service here for create category
            categoriesService
            .addCategory(this.newCategory)
            .then((response) => {
                this.updatedItem.categoryId = response.data.categoryId;
                this.latestCategoryId = response.data.categoryId;

                this.fetchData().then(() => {
                    this.resetAddForm();
                    this.showAddCategory = false;
                });
                this.$store.commit(
                    'SET_NOTIFICATION',
                    {
                        message: 'New Category created successfully.',
                        type: 'success'
                    }
                );
            })
            .catch ((error) => {
                console.error("Error adding category", error);
                this.$store.commit(
                    'SET_NOTIFICATION',
                    {
                        message: 'Failed to create new Category. Please try again.',
                        type: 'error'
                    }
                );
            });
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
        handleErrorResponse(error, verb) {
            if (error.response) {
                this.$store.commit( 'SET_NOTIFICATION',
                "Error " + verb + "item. Response received was '" + error.response.statusText + "'.");
            } else if (error.request) {
                this.$store.commit('SET_NOTIFICATION', "Error " + verb + " item. Server could not be reached.");
            } else {
                this.$store.commit('SET_NOTIFICATION', "Error " + verb + " item. Request could not be created.");
            }
        },
        // we need to validate the form
        validateForm() {
            let msg = '';
            if (this.updatedItem.itemName.length === 0) {
                msg += 'The new item must have a name. ';
            } 
            // available Quantity
            if (this.updatedItem.availableQuantity === null || this.updatedItem.availableQuantity === undefined) {
                msg += 'The new item must have an available quantity. It can be zero. ';
            }
            // reorder point
            if (this.updatedItem.reorderPoint === null || this.updatedItem.reorderPoint === undefined) {
                msg += 'The new item must have a reorder point. It can be zero. ';
            }
            // reorder quantity
            if (this.updatedItem.reorderQuantity === null || this.updatedItem.reorderQuantity === undefined) {
                msg += 'The new item must have a reorder quantity. It can be zero. ';
            }
            // categoryId
            if (this.updatedItem.categoryId === null || this.updatedItem.categoryId === undefined) {
                msg += 'The new item must have a category. ';
            }
            // supplier id
            if (this.updatedItem.supplierId === null || this.updatedItem.supplierId === undefined) {
                msg += 'The new item must have a supplier. ';
            }
            if (msg.length > 0) {
                this.$store.commit('SET_NOTIFICATION', msg);
                return false;
            }
            return true;
        },
    },
};
</script>


<style>
.notification {
  position: fixed;
  top: 20px;
  right: 20px;
  padding: 10px 20px;
  border-radius: 5px;
  color: #fff;
  z-index: 9999;
}

.success {
  background-color: green;
}

.error {
  background-color: red;
}
</style>