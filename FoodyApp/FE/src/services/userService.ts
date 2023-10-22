import axios from "axios";
import { baseURL } from "../utils/baseUrl";

//lấy thông tin người dùng theo id
export const getUserById = async (id: number) => {
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
export const updateUser = async (id: number, firstName: string, lastName: string, phoneNumber: string) => {
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

//Lấy thông tin đia chỉ người dùng
export const getAllAddress = async (UserId: number) => {
    const PageSize = 100;
    const PageIndex = 1;
    try {
        const response = await axios.get(`${baseURL}/User/get-user-address-paging`, {
            params: {
                UserId: UserId,
                PageSize: PageSize,
                PageIndex: PageIndex
            }
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

//cập nhật thông tin địa chỉ người dùng
export const updateAddress = async (id: number, UserId: number, Province: string, District: string, Ward: string, StreetAddress: string, DetailAddress: string, Notes: string, AddressType: string) => {
    try {
        const response = await axios.put(`${baseURL}/User/update-address`, {
            id: id,
            userId: UserId,
            province: Province,
            district: District,
            ward: Ward,
            streetAddress: StreetAddress,
            detailAddress: DetailAddress,
            notes: Notes,
            addressType: AddressType
        });
        if (response.status == 200) {
            return response;
        }
    }
    catch (error) {
        console.log(error);
        return null
    }
};

//thêm địa chỉ mới
export const createAddress = async (UserId: number, Province: string, District: string, Ward: string, StreetAddress: string, DetailAddress: string, Notes: string, AddressType: string) => {
    try {
        const response = await axios.post(`${baseURL}/User/create-new-address`, {
            userId: UserId,
            province: Province,
            district: District,
            ward: Ward,
            streetAddress: StreetAddress,
            detailAddress: DetailAddress,
            notes: Notes,
            addressType: AddressType
        });
        if (response.status == 200) {
            return response;
        }
    }
    catch (error) {
        console.log(error);
        return null
    }
};