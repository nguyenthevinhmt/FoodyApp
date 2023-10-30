import { getAccessToken } from './authService';
import axios from "axios";
import { baseURL } from "../utils/baseUrl";

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
export const addProductToCart = async (id: number) => {
    try {
        const token = await getAccessToken();
        const response = await axios.post(`${baseURL}/Cart/add-product-to-cart?productId=${id}`, null
            , {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    Accept: 'application/json'
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

//cập nhật số lượng sản phẩm
export const updateProductQuantity = async (id: number, quantity: number) => {
    try {
        const token = await getAccessToken();
        const params = {
            productId: id,
            quantity: quantity
        }

        const response = await axios.put(`${baseURL}/Cart/update-cart-quantity`, params, {
            headers: {
                'Authorization': `Bearer ${token}`,
                Accept: 'application/json'
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

//xóa sản phẩm khỏi cart
export const deleteProductFromCart = async (productId: number) => {
    try {
        const token = await getAccessToken();
        const response = await axios.delete(`${baseURL}/Cart/delete-product-from-cart?productId=${productId}`, {
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