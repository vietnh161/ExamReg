import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
const qs = require('qs');

Vue.use(Vuex)

export default new Vuex.Store({

	state: {
		status: '',
		token: localStorage.getItem('token') || '',
		user:{
			fullName:"",
			role:localStorage.getItem('role') || '',
		}

	},
	mutations: {
		getUserInfo(state, item){
			state.status = 'success'
			state.user = item
		},
		auth_request(state) {
			state.status = 'loading'
		},
		auth_success(state, item) {
			state.status = 'success'
			state.token = item[0]
			state.user = item[1]
		},
		auth_error(state) {
			state.status = 'error'
		},
		logout(state) {
			state.status = ''
			state.token = ''
		},
	},
	actions: {
		login({ commit }, user) {
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({ url: 'http://localhost:63834/oauth/token', headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, data: qs.stringify(user), method: 'POST' })
					.then(resp => {

						axios
							.get("http://localhost:63834/api/kithi/getall")
							.then(response => {
								let items = response.data;
								let data = new Array();
								items.forEach(element => {
									let obj = {
										value: element.kiThiId,
										text: element.name
									};
									data.push(obj);
								});

								if (data != null) {
									localStorage.kiThiId = data[0].value;;
								}
							})
							.catch(error => {
								console.log(error.response.data);
							});


						const token = resp.data.access_token
						//console.log(resp.data.fullname)
						let user = {
							fullname: resp.data.fullname,
							role: resp.data.role
						};
						var item = [token, user];
						localStorage.setItem('token', token)
						localStorage.setItem('role', user.role)
						// Add the following line:
						axios.defaults.headers.common['Authorization'] ='Bearer '+ token;
						commit('auth_success', item)
						resolve(resp)
					})
					.catch(err => {
						commit('auth_error')
						localStorage.removeItem('token')
						localStorage.removeItem('kiThiId')
				localStorage.removeItem('role')
						reject(err)
					})
			})
		},
		logout({ commit }) {
			return new Promise((resolve, reject) => {
				commit('logout')
				localStorage.removeItem('token')
				localStorage.removeItem('kiThiId')
				localStorage.removeItem('role')
				delete axios.defaults.headers.common['Authorization']
				resolve()
			})
		},
		getUserInfo({ commit }){
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({ url: 'http://localhost:63834/api/account/getUserlogged', headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, method: 'GET' })
					.then(resp => {
						let user = {
							fullname: resp.data.fullname,
							role: resp.data.role
						};
						commit('getUserInfo', user)
						resolve(resp)
					})
					.catch(err =>{
						reject(err)
					})
			})
		}
	},
	getters: {
		isLoggedIn: state => !!state.token,
		authStatus: state => state.status,
		userRole: state => state.user.role
	}

})