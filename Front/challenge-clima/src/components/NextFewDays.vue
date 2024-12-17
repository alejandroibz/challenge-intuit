<script setup>
  const props = defineProps(["weather"]);
</script>

<template>
  <div v-if="weather">
    <ul class="list-group">
      <li
        class="list-group-item d-flex justify-content-between align-items-center py-3"
        v-for="day in weather.forecast.forecastday"
        :key="day.date"
      >
        <div class="d-flex flex-column align-items-start">
          <span class="fw-bold">{{ new Date(day.date).toLocaleDateString("es-ES", { weekday: 'short', day: '2-digit', month: '2-digit' }) }}</span>
          <small>{{ day.date }}</small>
        </div>

        <div class="d-flex align-items-center">
          <img
            :src="`https:${day.day.condition.icon}`"
            alt="Icono de clima"
            class="me-3"
            style="width: 50px; height: 50px;"
          />
          <div class="text-end">
            <p class="m-0 fw-bold">{{ day.day.maxtemp_c }}°C</p>
            <small class="text-muted">{{ day.day.mintemp_c }}°C</small>
          </div>
        </div>

        <div class="d-flex flex-column text-end">
          <p class="m-0 fw-bold">{{ day.day.condition.text }}</p>
          <small class="text-muted">Humedad: {{ day.day.avghumidity }}%</small>
        </div>
      </li>
    </ul>
  </div>
</template>

<style scoped>
.list-group-item {
  border: none;
  border-bottom: 1px solid #ddd;
}

.list-group-item:last-child {
  border-bottom: none;
}

img {
  object-fit: cover;
}
</style>
