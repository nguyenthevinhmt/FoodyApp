import axios from "axios";

interface User {
  id: string;
  createdAt: string;
  username: string;
  password: string;
}
const BaseUrl = "https://64ed3b96f9b2b70f2bfb5909.mockapi.io/api/v1/";

const getAllUsers = axios.get(`${BaseUrl}/user`);
//   .then((res) => console.log(res.data))
//   .catch();

export default getAllUsers;
