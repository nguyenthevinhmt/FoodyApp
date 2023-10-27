import axios from "axios";
import { baseURL } from "../utils/baseUrl";
import { getAccessToken } from "./authService";

export const createPayment = async (id: number) => {
    try {
        const token = await getAccessToken();
        const param = {
            orderId: id
        }

        const response = await axios.post(`${baseURL}/Vnpay/payment-vn-pay`, param, {
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