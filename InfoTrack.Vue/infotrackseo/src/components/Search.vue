<template>
  <div class="content">
    <text-input class="margin-bottom-small" v-model="url" input-id="url-input" :error-message="urlError" 
      placeholder="Enter a url" label="Url" @keyup.enter="search"/>
    <text-input class="margin-bottom-small" v-model="searchTerm" input-id="url-input" :error-message="searchTermError" 
      placeholder="Enter your search terms" label="Search Terms" @keyup.enter="search"/>
    <button class="button" @click="search">Search</button>
    <p v-if="showResults">{{resultsString}}</p>
  </div>
</template>

<script>
import textInput from './TextInput'
import axios from 'axios'

export default {
  name: 'search',
  data () {
    return {
      url: "",
      urlError: "",
      searchTerm: "",
      searchTermError: "",
      results: new Array(),
      showResults: false
    }
  },
  computed: {
    resultsString() {
      if(this.results.length === 0)
        return "Your url did not appear in the top 100 google results for your search terms.";

      return `Your url was found in the following positions for your search terms: ${this.getResultsString()}`
    }
  },
  methods: {
    search(){
      this.urlError = "";
      this.searchTermError = "";
      let valid = this.validateInputs();
      if(!valid)
        return;

      let vueObj = this;
      axios
        .get(`http://localhost:5000/check?url=${encodeURIComponent(this.url)}&searchString=${encodeURIComponent(this.searchTerm)}`)
        .then(function(response) {
          vueObj.results = response.data;
          vueObj.showResults = true;
        });
    },
    validateInputs(){
      let validUrl = this.url && this.url !== "";
      let validSearchTerm = this.searchTerm && this.searchTerm !== "";
      
      if(!validUrl)
        this.urlError = "please enter a url";

      if(!validSearchTerm)
        this.searchTermError = "please enter your search terms";

      return validUrl && validSearchTerm;
    },
    getResultsString(){
      if(this.results.length === 1)
        return this.results[0];

      let resultsString = ""
      for(let i = 0; i < this.results.length; i++){
        if(i === 0){
          resultsString += this.results[i];
          continue;
        }

        if(i === this.results.length - 1){
          resultsString += ` and ${this.results[i]}`;
          continue; 
        }

        resultsString += `, ${this.results[i]}`
      }

      return resultsString;
    }
  },
  components: {
    textInput
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .content { width: 70%; margin: 0 auto; padding-top: 2rem; }
  .margin-bottom-small { margin-bottom: 1rem; }
  .button { font-size: 1.5rem; color: white; background-color: #003366; padding: 0.4rem; border-radius: 4px; cursor: pointer; }
</style>
