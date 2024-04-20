<template>
    <div>
        <h1>Add new item</h1>
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
            item: {},
        };
    }
}

</script>

<style>
</style>