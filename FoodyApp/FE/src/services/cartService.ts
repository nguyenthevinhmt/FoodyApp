import { TokenResponse } from './../models/AuthModel';
import AsyncStorage from "@react-native-async-storage/async-storage";
import { getAccessToken } from './authService';
import axios from "axios";
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

//lấy tất cả sản phẩm tồn tại trong cửa hàng, tổng giá
export const getCartByUser = async () => {
    try {
        const token = await getAccessToken();
        const response = await axios.get(`${baseURL}/Cart/get-cart-by-user`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null;
    }
}

//thêm sản phẩm vào cart
export const  addProductToCart = async (id: number) => {
    try {
        const token = await getAccessToken();
        const response = await axios.post(`${baseURL}/Cart/add-product-to-cart`, {
            'productId': id
        });
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null;
    }
}
