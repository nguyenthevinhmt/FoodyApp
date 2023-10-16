import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import { TokenResponse } from "../models/AuthModel";
import {baseURL} from "../utils/baseUrl";

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

//lấy thông tin sản phẩm theo id
export const getProductById = async (id: number) => {
    try {
        const response = await axios.get(`${baseURL}/Product/get-product-by-id/${id}`);
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null;
    }
}

//lấy tất cả sản phẩm được khuyến mãi
export const getProductDiscount = async () => {
    try {
        const params = {
            PageSize: 100,
            PageIndex: 1
        }
        const response = await axios.get(`${baseURL}/Product/get-product-discount-paging`, {params});
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null;
    }
}

class productService {
    static getListProduct = (pageIndex: number) => {
        const params = {
            startPrice: 0,
            endPrice: 9999999,
            PageSize: 20,
            PageIndex: pageIndex
        }
        return axios.get(`${baseURL}/Product/get-product-paging`, { params });
    }
}
export default productService;