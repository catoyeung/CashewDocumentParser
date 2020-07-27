import axios from 'axios';

axios.defaults.withCredentials = true

let API = axios.create({
  baseURL: process.env.REACT_APP_API_BASE_URL
})

const UNAUTHORIZED = 401
API.interceptors.response.use(
  response => response,
  error => {
    const { status } = error.response;
    if (status === UNAUTHORIZED) {
      localStorage.removeItem("isAuthenticated")
      window.location.href = '/account/session-timeout'
    }
    return Promise.reject(error)
  }
);

export default API