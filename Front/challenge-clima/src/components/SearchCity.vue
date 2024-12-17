<script setup>
import { ref } from "vue";

const emit = defineEmits(["selectCity"]);
import { cities } from "../data/cities";

const filteredCities = ref([]);
const searchQuery = ref("");


const filterCities = () => {
  filteredCities.value = cities.filter((city) =>
    city.toLowerCase().includes(searchQuery.value.toLowerCase())
  );
};

const handleCitySelection = (city) => {
  searchQuery.value = city;
  filteredCities.value = [];
  emit("selectCity", city);
};

</script>

<template>
  <!-- Buscador -->
  <div>
    <label for="city-search" class="form-label text-white"
      >Buscar Ciudad:</label
    >
    <input
      id="city-search"
      type="text"
      class="form-control"
      v-model="searchQuery"
      @input="filterCities"
      placeholder="Ingresa una ciudad"
    />
    <ul class="list-group mt-2" v-if="filteredCities.length">
      <li
        class="list-group-item"
        v-for="city in filteredCities"
        :key="city"
        @click="handleCitySelection(city)"
      >
        {{ city }}
      </li>
    </ul>
  </div>
</template>

