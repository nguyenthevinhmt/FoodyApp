import { Text, StyleSheet, View, ScrollView, TouchableOpacity, Image } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import ProductCartComponent from "../components/ProductCartComponent";
import ScreenNames from "../utils/ScreenNames";
import { baseURL_img } from "../utils/baseUrl";
import { useCallback, useEffect, useState } from "react";
import { useFocusEffect } from "@react-navigation/native";
import { deleteProductFromCart, getCartByUser } from "../services/cartService";
import EmptyOrderComponent from "../components/EmptyOrderComponent";

function emtyOrder() {
  return (
    <EmptyOrderComponent
      imageUrl={require('../assets/Icons/cart-xmark-svgrepo-com.png')}
      title="Quên chưa chọn món rồi nè bạn ơi?"
      detail="Thông tin giỏ hàng của bạn sẽ hiển thị ở đây!"
    />
  );
}

export default function CartScreen({ navigation }: any) {
  const [products, setProducts] = useState<any[]>([]);
  const [totalPrice, setTotalPrice] = useState(0);
  const [cartId, setCartId] = useState(0);

  //id sản phẩn được lựa chọn
  const [selectedProducts, setSelectedProducts] = useState<number[]>([]);

  //hiển thị lựa chọn xóa sản phẩm
  const [remove, setRemove] = useState(false);

  useFocusEffect(
    useCallback(() => {
      const getData = async () => {
        const result = await getCartByUser();
        setCartId(result?.data.cartId);
        setProducts(result?.data.products);
      };

      getData();
    }, [products])
  );

  //cập nhật tổng số tiền các sản phẩm được chọn
  useEffect(() => {
    setTotalPrice(() => {
      let sum = 0.0;
      selectedProducts.forEach(productId => {
        const product = products.find((value) => value['id'] === productId);
        if (product) {
          sum += product.actualPrice;
        }
      });
      return sum;
    });
  }, [selectedProducts]);

  //xử lý chọn từng sản phẩm
  const handleProductSelection = (productId: number) => {
    if (selectedProducts.includes(productId)) {
      setSelectedProducts(selectedProducts.filter(id => id !== productId));
    } else {
      setSelectedProducts([...selectedProducts, productId]);
    }
  };

  //xử lý chọn tất cả sản phẩm
  const handleSelectAll = () => {
    if (selectedProducts.length === products.length) {
      setSelectedProducts([]);
    } else {
      const allProductIds = products.map(product => product['id']);
      setSelectedProducts(allProductIds);
    }
  };

  //xử lý việc xóa sản phẩm
  const handleDelete = async (productId: number) => {
    setSelectedProducts(selectedProducts.filter(id => id !== productId));
    const result = await deleteProductFromCart(productId);
    console.log(result);
  }


  return (
    <SafeAreaView style={styles.container}>
      <View style={{ width: '100%', flex: 1 }}>
        {
          products ?
            <View style={styles.header}>
              <View style={styles.selectAllContainer}>
                <TouchableOpacity onPress={handleSelectAll}>
                  <Text style={styles.selectAllText}>
                    {selectedProducts.length === products.length ? 'Bỏ chọn tất cả' : 'Chọn tất cả'
                    }
                  </Text>
                </TouchableOpacity>
              </View>

              <View style={styles.deleteOption}>
                <TouchableOpacity onPress={() => setRemove(!remove)}>
                  <Text>Sửa</Text>
                </TouchableOpacity>
              </View>
            </View> 
            : ''
        }

        <ScrollView style={styles.cartDetail}>
          {
            products ? products.map((value) => (
              <View key={value['id']} style={{ flexDirection: 'row' }}>
                <View style={styles.productChoice}>
                  <TouchableOpacity onPress={() => handleProductSelection(value['id'])} style={styles.choiceButton}>
                    <View style={styles.choiceButtonText}>
                      {selectedProducts.includes(value['id']) && <Text style={styles.selectedIndicator}>✓</Text>}
                    </View>
                  </TouchableOpacity>
                </View>

                <View style={{ width: '75%' }}>
                  <ProductCartComponent
                    productId={value['id']}
                    imageUrl={`${baseURL_img}${value['productImageUrl']}`}
                    name={value['name']}
                    actualPrice={value['actualPrice']}
                    price={value['price']}
                    Quantity={value['quantity']}
                    onNavigation={() => navigation.navigate(ScreenNames.PRODUCT, { productId: value['id'] })}
                  />
                </View>

                {
                  remove ?
                    <TouchableOpacity style={styles.deleteProduct} onPress={() => handleDelete(value['id'])}>
                      <Image source={require('../assets/Icons/delete.png')} style={{ width: 30, height: 30 }} />
                    </TouchableOpacity>
                    :
                    <View style={{ width: '15%', marginVertical: 10, backgroundColor: '#fff' }}></View>
                }
              </View>
            )) 
            :
            <View style={{ marginTop: 200 }}>{emtyOrder()}</View>
          }
        </ScrollView>
      </View>

      <View style={styles.footter}>
        <View style={styles.price}>
          <Text style={{ fontSize: 12 }}>Tổng thanh toán: </Text>
          <Text style={{ fontSize: 16, color: '#EE4D2D' }}>{(totalPrice | 0).toLocaleString()}đ</Text>
        </View>

        <TouchableOpacity
          style={styles.orderButton}
          onPress={() => {
            if (products) {
              navigation.navigate(ScreenNames.CREATE_CART_ORDER,
                {
                  'cartId': cartId,
                  'products': products,
                  'totalPrice': totalPrice,
                })
            }
          }}>
          <Text style={{ color: '#fff' }}>Đặt hàng</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "space-between",
    alignItems: "center",
  },

  header: {
    height: 40,
    width: '100%',
    paddingHorizontal: 10,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },

  selectAllContainer: {

  },

  selectAllText: {

  },

  deleteOption: {

  },

  productChoice: {
    width: '10%',
    marginVertical: 10,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#fff'
  },

  choiceButton: {
    justifyContent: 'center',
    alignItems: 'center'
  },

  choiceButtonText: {
    width: 25,
    height: 25,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 0.7,

  },

  selectedIndicator: {
    //color: '#fff'
  },

  deleteProduct: {
    width: '15%',
    marginVertical: 10,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#EE4D2D',
  },

  footter: {
    width: '100%',
    height: 50,
    paddingLeft: 10,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    borderBottomWidth: 0.7,
    borderColor: '#B4B4B3',
    backgroundColor: '#fff'

  },

  price: {
    flexDirection: 'row',
    alignItems: 'center'
  },

  orderButton: {
    backgroundColor: '#EE4D2D',
    height: 50,
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 20,
  },

  cartDetail: {
    width: '100%',
    height: '50%'
  }
});
