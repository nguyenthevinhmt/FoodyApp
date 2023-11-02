import AsyncStorage from "@react-native-async-storage/async-storage";
import axios, { AxiosRequestConfig } from "axios";

const instance: any = axios.create();

const token = AsyncStorage.getItem("accessToken");
//request
instance.interceptors.request.use(
    (config: AxiosRequestConfig) => {
    if (token) {
        if (config.headers)
            config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error: any) => {
    return Promise.reject(error);
  }
);

export default instance;