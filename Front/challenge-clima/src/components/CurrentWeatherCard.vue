<script setup>
  const props = defineProps(["weather"]);
</script>

<template>
  <div v-if="weather" class="card my-4 p-3 shadow-sm">
    <!-- Encabezado -->
    <div class="border-bottom d-flex justify-content-between mb-3">
      <h5 class="text-start fw-light">El tiempo en: {{ weather.location.name }}</h5>
      <p class="text-end fw-light">Actualizado: {{ weather.current.last_updated }}</p>
    </div>

    <!-- Contenido Principal -->
    <div class="d-flex flex-column flex-md-row justify-content-between align-items-center">
      <!-- Icono y Temperatura -->
      <div class="d-flex align-items-center mb-3 mb-md-0">
        <img
          :src="`https:${weather.current.condition.icon}`"
          alt="Condición climática"
          class="weather-icon me-3"
        />
        <div>
          <p class="card-title m-0">{{ weather.current.temp_c }}°C</p>
          <p class="condition-text text-muted m-0">{{ weather.current.condition.text }}</p>
        </div>
      </div>

      <!-- Información Adicional -->
      <div class="d-flex flex-column">
        <p class="text-start m-0"><strong>Max:</strong> {{ weather.forecast.forecastday[0].day.maxtemp_c }}</p>
        <p class="text-start m-0"><strong>Min:</strong> {{ weather.forecast.forecastday[0].day.mintemp_c }}</p>
        <p class="text-start m-0"><strong>Humedad:</strong> {{ weather.forecast.forecastday[0].avghumidity }}%</p>
        <p class="text-start m-0"><strong>Prob Lluvia:</strong> {{ weather.forecast.forecastday[0].day.daily_chance_of_rain }}%</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.weather-icon {
  width: 64px;
  height: 64px;
}

.card-title {
  font-size: 3rem; /* Ajustado para mejor visualización en pantallas pequeñas */
  font-weight: bold;
}

.condition-text {
  font-size: 1rem;
}
</style>
