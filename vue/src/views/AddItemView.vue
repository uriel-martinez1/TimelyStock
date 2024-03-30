<template>
    <div>
        <h1>Add new item</h1>
        <!--We will be binding to the parent and feed the data to the child(ItemForm)-->
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
                id: 0,
                name: '',
                sku: 0,
                price: 0,
                availableQuantity: 0,
                reorderQuantity: 0,
                categoryId: 0,
                supplierId: 0
            },

            supplier: {
                id: 0,
                name: ''
            }
        };
    }
}

</script>

<style>
</style>