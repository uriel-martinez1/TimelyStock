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
                ItemId: 0,
                ItemName: '',
                ProductUrl: 0,
                SkuItemNumber: 0,
                Price: 0,
                AvailableQuantity: 0,
                ReorderQuantity: 0,
                CategoryId: 0,
                SupplierId: 0
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