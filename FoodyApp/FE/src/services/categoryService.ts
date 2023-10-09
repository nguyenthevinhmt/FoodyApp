import axios from "axios";
import baseURL from "../utils/baseUrl";

class categoryService {
    static getAllCategory = () => {
        return axios.get(`${baseURL}/Category/get-category-paging?PageSize=10&PageIndex=1`);
    }
}
export default categoryService;