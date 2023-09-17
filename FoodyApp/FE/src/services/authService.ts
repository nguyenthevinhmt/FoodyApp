import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import { TokenResponse, UserLogin } from "../models/AuthModel";

const baseURL = "http://192.168.1.7:5010/api/Auth";

export const login = async ({ email, password }: UserLogin) => {
  try {
    const response = await axios.post(`${baseURL}/login`, {
      email,
      password,
    });
    if (response) {
      const accessToken: string = response.data.accessToken;
      const refreshToken: string = response.data.refreshToken;
      await saveToken({ accessToken, refreshToken });
      await intercepterToken();
      return response;
    } else {
      return null;
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
    }
  } catch (error) {
    console.log("Lỗi ở service");

    console.log(error);
  }
};
