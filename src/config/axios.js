import axios from 'axios';

// let token=localStorage.getItem("token");
 const getToken = () => localStorage.getItem("token")
  ? localStorage.getItem("token")
  : null;

 const getAuthorizationHeader = () => `Bearer ${getToken()}`;

 export default (function axiosClient() {
  function api() {
    let token = localStorage.getItem("token");
    if (token == null) {
      return axios.create({
        baseURL: import.meta.env.VITE_BASEURL,
      });
    } else {
      const x = axios.create({
        baseURL: import.meta.env.VITE_BASEURL,
        headers: {
          Authorization: "Bearer " + token,
        },
      });
      // if (token) {
      //   verifyTokenExpiration(token);
      // }
      return x;
    }
  }

  return { api };
})();


