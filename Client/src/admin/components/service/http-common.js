import axios from 'axios';

export const HTTP = axios.create({
  baseURL: `http://localhost:63834/api/`,
  headers: {
    Authorization: 'Bearer {token}'
  }
})