import axios from 'axios';

axios.defaults.withCredentials = true
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Content-Type'] = 'application/json';

const getAPI = (history) => {

  let API = axios.create({
    baseURL: process.env.REACT_APP_API_BASE_URL
  })

  const UNAUTHORIZED = 401
  API.interceptors.response.use(
    response => response,
    error => {
      console.log(error)
      const { status } = error.response;
      if (status === UNAUTHORIZED) {
        history.push('/account/signin')
      }
      return Promise.reject(error)
    }
  );

  return API
}

export default getAPI