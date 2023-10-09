import axios from "axios";
import baseURL from "../utils/baseUrl";

class productService {
    static getAllProduct = () => {
        let params = '/Product/get-product-paging?startPrice=0&endPrice=999999999&PageSize=99&PageIndex=1'

        return axios.get(`${baseURL}${params}`);
    }
}
export default productService;