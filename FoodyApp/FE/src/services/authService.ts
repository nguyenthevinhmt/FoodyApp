import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import { TokenResponse } from "../models/AuthModel";
import { baseURL } from "../utils/baseUrl";

export const login = async (email: string, password: string) => {
  try {
    const response = await axios.post(`${baseURL}/Auth/login`, {
      email,
      password,
    });

    if (response.status === 200) {
      const accessToken: string = response.data.accessToken;
      const refreshToken: string = response.data.refreshToken;
      await saveToken({ accessToken, refreshToken });
      await intercepterToken();
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};

export const saveToken = async ({ accessToken, refreshToken }: any) => {
  try {
    await AsyncStorage.setItem("accessToken", accessToken ? accessToken : "");
    await AsyncStorage.setItem(
      "refreshToken",
      refreshToken ? refreshToken : ""
    );
  } catch (error: any) {
    console.log(error.response);
    console.error("Lỗi khi lưu trữ token:", error);
  }
};

export const getAccessToken = async () => {
  try {
    const accessToken = await AsyncStorage.getItem("accessToken");
    return accessToken;
  } catch (error) {
    console.error("Lỗi khi truy xuất accessToken:", error);
    return null;
  }
};

export const getRefreshToken = async () => {
  try {
    const refreshToken = await AsyncStorage.getItem("refreshToken");
    return refreshToken;
  } catch (error) {
    console.error("Lỗi khi truy xuất refreshToken:", error);
    return null;
  }
};

export const Logout = async () => {
  try {
    await AsyncStorage.removeItem("accessToken");
    await AsyncStorage.removeItem("refreshToken");
  } catch (error) {
    console.error("Lỗi khi xóa token:", error);
  }
};

export const intercepterToken = async () =>
  axios.interceptors.response.use(
    function (config) {
      const token = AsyncStorage.getItem("accessToken");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },

    function (error) {
      return Promise.reject(error);
    }
  );

export const refreshAccessToken = async ({
  accessToken,
  refreshToken,
}: TokenResponse) => {
  try {
    const newToken = await axios.post(`${baseURL}/refresh`, {
      accessToken,
      refreshToken,
    });

    if (newToken) {
      const accessToken: string = newToken.data.accessToken;
      const refreshToken: string = newToken.data.refreshToken;

      await saveToken({ accessToken, refreshToken });
      console.log("Đã refresh");

      return { accessToken, refreshToken };
    } else {
      console.log("Token đã hết hạn");
      return null;
    }
  } catch (error) {
    return null;
    console.log("Lỗi ở service");
    console.log(error);
  }
};

export const register = async (email: string, password: string) => {
  try {
    const userType = 2;
    const response = await axios.post(`${baseURL}/Auth/register`, {
      email,
      password,
      userType
    });

    if (response.status === 200) {
      console.log('Đăng ký thành công');
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};