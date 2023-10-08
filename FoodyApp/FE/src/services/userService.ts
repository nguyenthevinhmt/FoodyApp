import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import { TokenResponse } from "../models/AuthModel";
import baseURL from "../utils/baseUrl";

axios.interceptors.request.use(
    function (config) {
      // Do something before request is sent
      return config;
    },
    function (error) {
      // Do something with request error
      return Promise.reject(error);
    }
  );

axios.interceptors.response.use(
    function (response) {
        return response;
    },
    function (error) {
        console.error(error);
        return Promise.reject(error);
    }
);

//lấy thông tin người dùng theo id
export const getById = async (id: number) => {
    try {
        const response = await axios.get(`${baseURL}/User/get-by-id/${id}`);
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null
    }
}

//cập nhật thông tin người dùng
export const update = async (id: number, firstName: string, lastName: string, phoneNumber: string) => {
    try {
        const response = await axios.put(`${baseURL}/User/update-user-infor`, {
            id,
            firstName,
            lastName,
            phoneNumber
        });
        if (response.status == 200) {
            return response;
        }
    }
    catch (error) {
        console.log(error);
        return null
    }
}