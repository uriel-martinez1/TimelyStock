<template>
    <div>
        <h1>Add New Item</h1>
        <item-form v-bind:item="item" />
    </div>
</template>

<script>
import ItemForm from '../components/ItemForm.vue';

export default {
    components: {
        ItemForm,
    }, 
    data() {
        return {
            item: {
                itemId: 0,
                itemName: "",
                productUrl: "",
                skuItemNumber: "",
                price: 0,
                availableQuantity: 0,
                reorderPoint: 0,
                reorderQuantity: 0,
                categoryId: 0,
                supplierId: 0
            },
        };
    },
};

</script>