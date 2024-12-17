<script setup>
import axios from "axios";
import { ref } from "vue";

import CurrentWeatherCard from "./components/CurrentWeatherCard.vue";
import NextFewDays from "./components/NextFewDays.vue";
import Historical from "./components/Historical.vue";
import SearchCity from "./components/SearchCity.vue";

const cache = new Map(); 
const cacheDuration = 2 * 60 * 1000; 

const selectedCity = ref(null);
const apiKey = "ce7e58e20bbe42d884c223247241312";
const weather = ref(null);
let cacheKey = null;



const selectCity = (city) => {
  selectedCity.value = city; 
  fetchWeather(city);
};

const checkCache = (city) => {
  cacheKey = city.toLowerCase();

  if (cache.has(cacheKey)) {
    const cachedData = cache.get(cacheKey);
    const now = Date.now();

    if (now - cachedData.timestamp < cacheDuration) {
      console.log(`Usando datos en caché para ${city}`);
      weather.value = cachedData.weather;
      return true; 
    } else {
      cache.delete(cacheKey); 
    }
  }

  return false; 
};

const fetchWeather = async (city) => {
  if (checkCache(city)) {
    return; 
  }

  try {
    const forecastResponse = await axios.get(
      `https://api.weatherapi.com/v1/forecast.json?key=${apiKey}&q=${city}&days=5`
    );

    weather.value = forecastResponse.data;

    cache.set(cacheKey, {
      weather: weather.value,
      timestamp: Date.now(),
    });

  } catch (error) {
    console.error("Error al obtener datos del clima:", error);
  }
};
</script>


<template>
  <div class="container my-4">
    <h1 class="text-center mb-4 text-white">Aplicación del Clima</h1>

    <SearchCity @selectCity="selectCity($event)"/>
    <CurrentWeatherCard :weather="weather" />
    <NextFewDays :weather="weather"/>
    <Historical :weather="weather" :apiKey="apiKey" />
  </div> 
</template>

<style>
.container {
  max-width: 900px;
  margin: 0 auto;
}
.card {
  margin-bottom: 20px;
}
</style>
