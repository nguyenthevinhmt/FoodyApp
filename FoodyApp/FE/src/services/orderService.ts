import { getAccessToken } from "./authService";
import axios from "axios";
import { baseURL } from "../utils/baseUrl";

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

axios.interceptors.response.use(
  function (response) {
    return response;
  },
  function (error) {
    console.error(error);
    return Promise.reject(error);
  }
);

//lấy thông tin tất cả order pending
export const getAllOrderPending = async () => {
  try {
    const token = await getAccessToken();
    const response = await axios.get(`${baseURL}/Order/get-all-pending-order`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};

//lấy thông tin tất cả order shipping
export const getAllOrderShipping = async () => {
  try {
    const token = await getAccessToken();
    const response = await axios.get(
      `${baseURL}/Order/get-all-shipping-order`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};

//lấy thông tin tất cả order success
export const getAllOrderSuccess = async () => {
  try {
    const token = await getAccessToken();
    const response = await axios.get(`${baseURL}/Order/get-all-success-order`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};

//lấy thông tin tất cả order cancel
export const getAllOrderCancel = async () => {
  try {
    const token = await getAccessToken();
    const response = await axios.get(`${baseURL}/Order/get-all-cancel-order`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};

//lấy thông tin order theo id
export const getOrderById = async (id: number) => {
  try {
    const token = await getAccessToken();
    const response = await axios.get(`${baseURL}/Order/get-by-id/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};

//tạo order mới trực tiếp từ sản phẩm
export const createOrder = async (
  productId: number,
  paymentMethod: number,
  quantity: number,
  addressType: number
) => {
  try {
    const token = await getAccessToken();
    const response = await axios.post(
      `${baseURL}/Order/create-order`,
      {
        productId: productId,
        paymentMethod: paymentMethod,
        quantity: quantity,
        addressType: addressType,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );
    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};

//tạo order mới từ cart
export const createCartOrder = async (
  cartId: number,
  paymentMethod: number,
  addressType: number
) => {
  try {
    const token = await getAccessToken();

    const params = {
      cartId: cartId,
      paymentMethod: paymentMethod,
      addressType: addressType,
    };

    const response = await axios.post(
      `${baseURL}/Order/create-order-from-cart`,
      params,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
};
