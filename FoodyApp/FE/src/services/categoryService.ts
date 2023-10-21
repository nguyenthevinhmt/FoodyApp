import axios from "axios";
import { baseURL } from "../utils/baseUrl";

export const getAllCategory = async (Name: string | null, PageSize: number, PageIndex: number) => {
    try {
        const params = {
            Name: Name,
            PageSize: PageSize,
            PageIndex: PageIndex
        }
        const response = await axios.get(`${baseURL}/Category/get-category-paging`, { params });
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null;
    }
}
