<template>
    <div>
        <a v-bind:href="link.link" class="list-group-item list-group-item-action bg-light" v-for="link in links" v-bind:key="link.link">{{link.name}}</a>
    </div>
</template>

<script>
export default {
    props:[
        "links"
    ],
    computed:{
        getLink(obj){
            return obj.link;
        }
    }
}
</script>