import axios from "axios";

const baseURL = "http://192.168.1.171:5010/api/Pong";

export const ping = () => {
  axios
    .get(baseURL)
    .then((response) => console.log(response.data))
    .catch((err) => console.log(err));
};
