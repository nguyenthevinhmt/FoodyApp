import { Text, StyleSheet, View, ScrollView, TouchableOpacity, Image, ToastAndroid } from "react-native";
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
  //kiểm tra khi component con gọi api
  const [componentAction, setComponentAction] = useState(true);

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
        setSelectedProducts([]);

        if (!products) {
          setTotalPrice(0);
        }
      };

      getData();
    }, [componentAction])
  );

  //cập nhật tổng số tiền các sản phẩm được chọn
  useEffect(() => {
    setTotalPrice(() => {
      console.log(selectedProducts);
      let sum = 0.0;
      if (selectedProducts && products) {
        selectedProducts.forEach(productId => {
          const product = products.find((value) => value['productCartId'] === productId);
          if (product) {
            sum += product.actualPrice*product.quantity;
          }
        });
      }
      return sum;
    });
  }, [selectedProducts, products]);

  //xử lý chọn từng sản phẩm
  const handleProductSelection = (productCartId: number) => {
    if (selectedProducts.includes(productCartId)) {
      setSelectedProducts(selectedProducts.filter(id => id !== productCartId));
    } else {
      setSelectedProducts([...selectedProducts, productCartId]);
    }
  };

  //xử lý chọn tất cả sản phẩm
  const handleSelectAll = () => {
    if (selectedProducts.length === products.length) {
      setSelectedProducts([]);
    } else {
      const allProductIds = products.map(product => product['productCartId']);
      setSelectedProducts(allProductIds);
    }
  };

  //xử lý việc xóa sản phẩm
  const handleDelete = async (productId: number, productCartId: number) => {
    setSelectedProducts(selectedProducts.filter(id => id !== productCartId));
    const result = await deleteProductFromCart(productId);
    console.log(result);
    setComponentAction(!componentAction);
    ToastAndroid.show("Đã xóa sản phẩm khỏi giỏ hàng", ToastAndroid.SHORT);
  }

  //theo dõi hoạt động trong component
  const handleComponent = () => {
    // Thực hiện các tác vụ khi component con gọi API
    console.log("Component con đã gọi API");
    setComponentAction(!componentAction);
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={{ width: '100%', flex: 1 }}>
        {
          products ?
            <View style={styles.header}>
              <TouchableOpacity onPress={handleSelectAll} style={styles.selectAllContainer}>
                <View style={[styles.choiceButtonText, {borderColor: '#fff'}]}>
                  {selectedProducts.length === products.length ? <Text style={[styles.selectedIndicator, {color: '#fff', fontSize: 8}]}>✓</Text> : ''}
                </View>

                <View style={{marginLeft: 5}}>
                  <Text style={{color: '#fff'}}>
                    {selectedProducts.length === products.length ? 'Bỏ chọn tất cả' : 'Chọn tất cả'}
                  </Text>
                </View>
              </TouchableOpacity>

              <View style = {{marginRight: 10}}>
                <TouchableOpacity onPress={() => setRemove(!remove)}>
                  <Text style={{color: '#fff'}}>Sửa</Text>
                </TouchableOpacity>
              </View>
            </View> 
            : ''
        }

        <ScrollView style={styles.cartDetail}>
          {
            products ? products.map((value) => (
              <View key={value['productCartId']} style={{ flexDirection: 'row' }}>
                <View style={styles.productChoice}>
                  <TouchableOpacity onPress={() => handleProductSelection(value['productCartId'])} style={styles.choiceButton}>
                    <View style={styles.choiceButtonText}>
                      {selectedProducts.includes(value['productCartId']) && <Text style={styles.selectedIndicator}>✓</Text>}
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
                    onAction={() => (handleComponent())}
                  />
                </View>

                {
                  remove ?
                    <TouchableOpacity style={styles.deleteProduct} onPress={() => handleDelete(value['id'], value['productCartId'])}>
                      <Image source={require('../assets/Icons/delete.png')} style={{ width: 20, height: 20 }} />
                    </TouchableOpacity>
                    :
                    <View style={{ width: '15%', marginBottom: 10, backgroundColor: '#fff' }}></View>
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
            if (selectedProducts.length > 0) {
              navigation.navigate(ScreenNames.CREATE_CART_ORDER,
                {
                  'cartId': cartId,
                  'selectedProducts': selectedProducts, //id sản phẩm trong giỏ hàng
                  'products': products.filter(product => selectedProducts.includes(product['productCartId'])),
                  'totalPrice': totalPrice,
                })
            };
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
    height: 60,
    width: '100%',
    paddingHorizontal: 8,
    paddingBottom: 10,
    flexDirection: 'row',
    alignItems: 'flex-end',
    justifyContent: 'space-between',
    backgroundColor: '#EE4D2D',
  },

  selectAllContainer: {
    flexDirection: 'row',
    alignItems: 'center'
  },

  productChoice: {
    width: '10%',
    marginBottom: 10,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#fff'
  },

  choiceButton: {
    justifyContent: 'center',
    alignItems: 'center',
  },

  choiceButtonText: {
    width: 15,
    height: 15,
    justifyContent: 'center',
    alignItems: 'center',
    borderWidth: 0.5,
    borderRadius: 3

  },

  selectedIndicator: {
    //color: '#fff'
    fontSize: 10,
  },

  deleteProduct: {
    width: '15%',
    marginBottom: 10,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#EE4D2D',
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

  }
});
