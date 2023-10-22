import { Text, StyleSheet, View, ScrollView, TouchableOpacity } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import ProductCartComponent from "../components/ProductCartComponent";
import ScreenNames from "../utils/ScreenNames";
import { baseURL_img } from "../utils/baseUrl";
import { useCallback, useState } from "react";
import { useFocusEffect } from "@react-navigation/native";
import { getCartByUser } from "../services/cartService";
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
  const [products, setProducts] = useState([]);
  const [totalPrice, setTotalPrice] = useState(0);
  const [cartId, setCartId] = useState(0);

  useFocusEffect(
    useCallback(() => {
      const getData = async () => {
        const result = await getCartByUser();
        setCartId(result?.data.cartId);
        setTotalPrice(result?.data.totalPrice);
        setProducts(result?.data.products);
      };

      getData();
    }, [products])
  );

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView style={styles.cartDetail}>
        {
          products ? products.map((value) => (
            <ProductCartComponent
              key={value['id']}
              productId={value['id']}
              imageUrl={`${baseURL_img}${value['productImageUrl']}`}
              name={value['name']}
              actualPrice={value['actualPrice']}
              price={value['price']}
              Quantity={value['quantity']}
              onNavigation={() => navigation.navigate(ScreenNames.PRODUCT, { productId: value['id'] })}
            />
          )) : 
          <View style={{marginTop: 200}}>{emtyOrder()}</View>
        }
      </ScrollView>

      <View style={styles.header}>
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
    justifyContent: "center",
    alignItems: "center",
  },

  header: {
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
