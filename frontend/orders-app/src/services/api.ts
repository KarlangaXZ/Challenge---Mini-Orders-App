import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5193/api', 
})

export default api
