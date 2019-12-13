//import HTTP from './http-common';
import Axios from 'axios'; 

const RESOURCE_NAME = 'http://localhost:63834/api/monthi';
 
export default {
  getAll() {
    return Axios.get(RESOURCE_NAME+'/getall');
  },
 
  get(id) {
    return  Axios.get(`${RESOURCE_NAME}/${id}`);
  },
 
  create(data) {
    return Axios.post(RESOURCE_NAME+'/create', data);
  },
 
  update(id, data) {
    return Axios.put(`${RESOURCE_NAME}/${id}`, data);
  },
 
  delete(id) {
    return Axios.delete(`${RESOURCE_NAME+'/delete?id='}${id}`);
  }
};