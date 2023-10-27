import axios from "axios";
import { baseURL } from "../utils/baseUrl";

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
        const response = await axios.get(`${baseURL}/Product/get-product-discount-paging`, { params });
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null;
    }
}

//lấy tất cả sản phẩm theo tên, danh mục
export const getListProduct = async (Name: string | null, CategoryId: number | null, PageSize: number, pageIndex: number) => {
    try {
        const params = {
            Name: Name,
            CategoryId: CategoryId,
            startPrice: 0,
            endPrice: 9999999,
            PageSize: PageSize,
            PageIndex: pageIndex
        }
        const response = await axios.get(`${baseURL}/Product/get-product-paging`, { params });
        if (response.status == 200) {
            return response;
        }
    } catch (error) {
        console.log(error);
        return null;
    }
}

