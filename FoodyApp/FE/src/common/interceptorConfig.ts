import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";

axios.create({
  baseURL: "http://192.168.1.10:5010/api/",
  timeout: 1000,
});

// Hàm để làm mới accessToken bằng refreshToken
const handleRefreshToken = async (
  accessToken: string,
  refreshToken: string
) => {
  try {
    const response = await axios.post(
      "http://192.168.1.10:5010/api/Auth/refresh",
      {
        accessToken,
        refreshToken,
      }
    );
    const newAccessToken = response.data.accessToken;
    const newRefreshToken = response.data.refreshToken;
    // Lưu trữ lại accessToken mới
    AsyncStorage.setItem("accessToken", newAccessToken);
    AsyncStorage.setItem("refreshToken", newRefreshToken);
    return newAccessToken;
  } catch (error) {
    console.error("Error refreshing accessToken:", error);
    throw error;
  }
};

axios.interceptors.request.use(
  function (config: any) {
    const accessToken = AsyncStorage.getItem("accessToken");
    if (accessToken) {
      config.headers = {
        authorization: `Bearer ${accessToken}`,
      };
    }
    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (error.response.status === 401) {
      // Token hết hạn
      const accessToken = await AsyncStorage.getItem("accessToken");
      const refreshToken = await AsyncStorage.getItem("refreshToken");
      const newAccessToken = await handleRefreshToken(
        accessToken as string,
        refreshToken as string
      );
      const originalRequest = error.config;
      originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
      return axios(originalRequest);
    }
    return Promise.reject(error);
  }
);
export default axios;
