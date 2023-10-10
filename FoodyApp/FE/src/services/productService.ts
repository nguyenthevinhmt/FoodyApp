import axios from "axios";
import {baseURL} from "../utils/baseUrl";

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