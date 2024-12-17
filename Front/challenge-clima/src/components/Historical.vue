<script setup>
import { ref, watch } from "vue";
import axios from "axios";

const props = defineProps(["weather", "apiKey"]);

const historical = ref(null);
const location = ref(null)
const selectedDate = ref("");
let debounceTimer = null;

const fetchHistorical = async () => {
  if (!selectedDate.value) return; 

  try {
    const response = await axios.get(
      `https://api.weatherapi.com/v1/history.json?key=${props.apiKey}&q=${props.weather.location.name}&dt=${selectedDate.value}`
    );

    if (response.data.forecast && response.data.forecast.forecastday.length > 0) {
      location.value = response.data.location;
      historical.value = response.data.forecast.forecastday[0];
    } else {
      historical.value = null;
    }

  } catch (error) {
    historical.value = null;
  }
};

watch(selectedDate, () => {
  if (debounceTimer) clearTimeout(debounceTimer);

  debounceTimer = setTimeout(() => {
    fetchHistorical();
  }, 1500); 
});

watch(
  () => props.weather,
  (newWeather) => {
    if (newWeather) {
      if (selectedDate.value) {
        fetchHistorical();
      }
    }
  }
);

</script>

<template>
  <div v-if="weather" class="my-4">
    <div class="mb-4">
      <label for="date-picker" class="form-label fw-bold  text-white">Seleccionar Fecha Histórica:</label>
      <input
        id="date-picker"
        type="date"
        class="form-control"
        v-model="selectedDate"
      />
    </div>

    <div v-if="historical" class="historical-card p-3 rounded shadow-sm bg-light">
      <div class="d-flex flex-column flex-md-row justify-content-between align-items-center">
        <div class="d-flex align-items-center mb-3 mb-md-0">
          <img
            :src="`https:${historical.day.condition.icon}`"
            alt="Condición climática"
            class="weather-icon me-3"
          />
          <div>
            <p class="display-6 fw-bold m-0">{{ historical.day.maxtemp_c }}°C</p>
            <p class="text-muted m-0">{{ historical.day.condition.text }}</p>
          </div>
        </div>

        <div class="d-flex flex-column">
          <p class="text-start m-0"><strong>Max:</strong> {{ historical.day.maxtemp_c }}°C</p>
          <p class="text-start m-0"><strong>Min:</strong> {{ historical.day.mintemp_c }}°C</p>
          <p class="text-start m-0"><strong>Humedad:</strong> {{ historical.day.avghumidity }}%</p>
          <p class="text-start m-0"><strong>Prob Lluvia:</strong> {{ historical.day.daily_chance_of_rain }}%</p>

        </div>
      </div>
    </div>



  </div>
</template>

<style scoped>
/* Icono del clima */
.weather-icon {
  width: 64px;
  height: 64px;
}

/* Card histórica */
.historical-card {
  border: 1px solid #ddd;
  background-color: #f8f9fa;
}
</style>
