import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "../common/interceptorConfig";
import { baseURL } from "../utils/baseUrl";
import { useNavigation } from "@react-navigation/native";
import ScreenNames from "../utils/ScreenNames";
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

export const refreshAccessToken = async (
  accessToken: string,
  refreshToken: string
) => {
  const navigation = useNavigation<any>();
  const navigate = (name: string, params?: any) => {
    navigation.navigate(name, params);
  };
  try {
    const newToken = await axios.post(`${baseURL}/Auth/refresh`, {
      accessToken: accessToken,
      refreshToken: refreshToken,
    });
    if (newToken.status === 200) {
      const newAccessToken: string = newToken.data.accessToken;
      const newRefreshToken: string = newToken.data.refreshToken;

      await saveToken({ newAccessToken, newRefreshToken });
      return { newAccessToken, newRefreshToken };
    } else {
      console.log("Token đã hết hạn");
      navigate(ScreenNames.LOGIN);
      return null;
    }
  } catch (error) {
    navigate(ScreenNames.LOGIN);
  }
};

export const register = async (email: string, password: string) => {
  try {
    const userType = 2;
    const response = await axios.post(`${baseURL}/Auth/register`, {
      email,
      password,
      userType,
    });

    if (response.status === 200) {
      console.log("Đăng ký thành công");
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};
