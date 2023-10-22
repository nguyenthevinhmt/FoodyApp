import axios from "axios";

axios.interceptors.request.use(
  function (config) {
    config.headers["Authorization"] += `Bearer ${localStorage.getItem(
      "access_token"
    )}`;
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (
      error.response.status === 401 &&
      error.response.data.message === "Token expired"
    ) {
      // Token hết hạn
      const accessToken = localStorage.getItem("accessToken");
      const refreshToken = localStorage.getItem("refreshToken");

      if (refreshToken) {
        const newAccessToken = await handleRefreshToken(
          accessToken,
          refreshToken
        );
        // Cố gắng gửi lại request với accessToken mới
        const originalRequest = error.config;
        originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
        return axios(originalRequest);
      } else {
        // Không có refreshToken, đăng xuất người dùng hoặc thực hiện xử lý khác
        console.log("No refreshToken available.");
      }
    }

    return Promise.reject(error);
  }
);

export default axios;
