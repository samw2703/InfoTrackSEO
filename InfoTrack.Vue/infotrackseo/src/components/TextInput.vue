<template>
  <div>
    <label :for="inputId">{{label}}</label>
    <input :id="inputId" class="text-large rounded-corners" type="text" :placeholder="placeholder" 
      :value="value" @input="$emit('input', $event.target.value)" @keyup="$emit('keyup', $event)">
    <div class="text-end">
      <span v-if="showError" class="text-error">{{errorMessage}}</span>
    </div>
  </div>
</template>

<script>

export default {
  name: 'text-input',
  props: {
    label: { type: String, required: true },
    errorMessage: { type: String, required: true },
    value: { type: String, required: true },
    inputId: { type: String, required: true },
    placeholder: { type: String, required: true }
  },
  computed: {
    showError() {
      return this.errorMessage != null && this.errorMessage !== "";
    }
  },
  model: {
    prop: "value",
    event: "input"
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.content { width: 70%; margin: 0 auto; padding-top: 2rem; }
.text-error { color: red; }
.text-small { font-size: 0.75rem; }
.text-large { font-size: 1.5rem; }
.text-end { text-align: right; }
.rounded-corners { border-radius: 4px; }
input { width: 100%; padding: 0.25rem; }
</style>
