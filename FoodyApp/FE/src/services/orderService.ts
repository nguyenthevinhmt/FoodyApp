import { getAccessToken } from "./authService";
import axios from "axios";
import { baseURL } from "../utils/baseUrl";

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
  productId: [],
  paymentMethods: number,
  addressType: number
) => {
  try {
    const token = await getAccessToken();

    const params = {
      cartId: cartId,
      productId: productId,
      paymentMethods: paymentMethods,
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
}

//thay đổi trạng thái đơn hàng
export const updateOrderStatus = async (orderId: number, newStatus: number) => {
    try {
        const token = await getAccessToken();
        const params = {
            orderId: orderId,
            newStatus: newStatus
        };

        const response = await axios.put(`${baseURL}/Order/update-order-status`, params, {
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

//xóa đơn hàng được tạo trực tiếp từ sản phẩm
export const deleteOrder = async (orderId: number) => {
  try {
    const response = await axios.delete(`${baseURL}/Order/delete-order?orderId=${orderId}`);

    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
}

//Xử lý đơn hàng khi thanh toán thành công
export const orderPaidSuccess = async (orderId: number) => {
  try {
    const response = await axios.put(`${baseURL}/Order/order-paid-success?orderId=${orderId}`);

    if (response.status == 200) {
      return response;
    }
  } catch (error) {
    console.log(error);
    return null;
  }
}

//Xử lý đơn hàng khi thanh toán từ giỏ hàng thất bại
export const orderFromCartFail = async (orderId:number, productCartId: []) => {
  const params = {
    orderId: orderId,
    productCartId: productCartId
  }
  try {
    const token = await getAccessToken();

    const response = await axios.put(`${baseURL}/Order/order-from-cart-fail`, params, {
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
