import { Text, View, StyleSheet, TouchableOpacity, ScrollView, Image, Button, Dimensions } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React, { useRef } from 'react';
import PagerView from 'react-native-pager-view';
import { getProductById } from "../services/productService";
import { useEffect, useState } from "react";
import Modal from 'react-native-modal';
import { getCartByUser } from "../services/cartService";
import ScreenNames from "../utils/ScreenNames";

const ProductScreen = ({ navigation, route }: any) => {
  const Id = route.params['productId'];

  //kiểm tra sản phẩm có được giảm giá ko thì sẽ hiển thị icon lên
  const [discount, checkDiscount] = useState(true);

  const [quantity, setQuantity] = useState(1);
  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);
  const [actualPrice, setActualPrice] = useState(0);
  const [description, setDescription] = useState('');
  const [imgUrl, setImgUrl] = useState('http://192.168.1.10:5010');

  const [cartProducts, setCartProducts] = useState([]);
  const [cart, setCart] = useState([]);

  useEffect(() => {
    const getData = async () => {
      //lấy thông tin sản phẩm theo id
      const product = await getProductById(Id);

      setName(product?.data['name']);
      setPrice(product?.data['price']);
      setActualPrice(product?.data['actualPrice']);
      setDescription(product?.data['description']);
      setImgUrl('http://192.168.1.10:5010' + product?.data['productImageUrl']);

      if (product?.data['promotion'] != null) {
        checkDiscount(true);
      }
      else {
        checkDiscount(false);
      }
    };

    getData();
  }, []);

  useEffect(() => {
    const getCart = async () => {
      //lấy thông tin sản phẩm trong giỏ hàng
      const cartValue = await getCartByUser();
      setCartProducts(cartValue?.data['products']);
      setCart(cartValue?.data);
    }

    getCart();
  }, []);

  const [isModalVisible, setModalVisible] = useState(false);

  const toggleModal = () => {
    setModalVisible(!isModalVisible);
  };


  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.header}>
        <PagerView style={styles.viewPager} initialPage={0}>

          <Image source={{ uri: imgUrl }} style={styles.image} />


        </PagerView>
      </View>

      <View style={styles.title}>
        <View style={styles.name}>
          <Text style={{ fontSize: 16, fontWeight: '600' }}>{name}</Text>
          {discount ?
            <Image source={require('../assets/Icons/discount.png')} style={styles.discountIcon} />
            : ''
          }
        </View>
        <View style={styles.price}>
          <Text style={{ color: '#EE4D2D', fontWeight: '600' }}>đ{actualPrice}</Text>
        </View>
        <View style={styles.actualPrice}>
          <Text style={{
            fontSize: 12,
            color: '#B4B4B3',
            textDecorationLine: 'line-through'
          }}>đ{price}</Text>
        </View>
      </View>

      <View style={styles.detail}>
        <Text style={{
          width: '100%',
          paddingLeft: 10,
          paddingVertical: 5,
          fontWeight: '600'
        }}>Mô tả món ăn</Text>
        <ScrollView>
          <Text style={{
            width: '100%',
            paddingHorizontal: 10,
          }}>
            {description}
          </Text>
        </ScrollView>
      </View>

      <View style={styles.buttArea}>
        <TouchableOpacity style={styles.buttLeft} onPress={toggleModal} >
          <Image source={require('../assets/Icons/add-cart.png')} style={styles.addCartIcon} />
          <Text style={{ color: '#EE4D2D' }}>Thêm vào giỏ hàng</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.buttRight}
          onPress={() => navigation.navigate(ScreenNames.CREATE_ORDER, {
            id: Id,
            productName: name,
            price: price,
            actualPrice: actualPrice,
            imgUrl: imgUrl,
            quantity: quantity
          })}>
          <Text style={{ color: '#fff' }}>Mua ngay</Text>
        </TouchableOpacity>
      </View>

      <Modal
        isVisible={isModalVisible}
        style={styles.bottomSheet}
        onBackdropPress={toggleModal} // Đóng modal khi chạm vào ngoài vùng hiển thị
        onSwipeComplete={toggleModal} // Đóng modal khi vuốt xuống
        swipeDirection="down" // Cho phép vuốt xuống để đóng modal
      >
        <View style={styles.bottomSheetContainer}>
          <View style={styles.headerBottomSheet}>
            <TouchableOpacity>
              <Text style={{
                color: '#EE4D2D'
              }}>Xóa tất cả</Text>
            </TouchableOpacity>

            <Text style={{ fontSize: 18, fontWeight: '600' }}>Giỏ hàng</Text>

            <TouchableOpacity style={{ justifyContent: 'flex-start' }} onPress={() => { toggleModal() }}>
              <Text style={{
                fontSize: 20
              }}>X</Text>
            </TouchableOpacity>
          </View>

          <View style={styles.bottomSheetContent}>

            {//lỗi chưa xử lý được quantity của riêng từng sản phẩm
              //   cartProducts.map((value) => (
              //     <View style={styles.productCart} key={value['id']}>
              //       <View style={{ width: '30%' }}>
              //         <Image source={{ uri: `http://192.168.1.10:5010${value['productImageUrl']}` }} style={styles.bottomSheetImage} />
              //       </View>

              //       <View style={styles.productDetail}>
              //         <Text style={styles.productCartName}>{value['name']}</Text>
              //         <Text style={styles.productCartActualPrice}>đ{value['actualPrice']}</Text>
              //         <Text style={styles.productCartPrice}>đ{value['price']}</Text>
              //         <View style={styles.quantity}>
              //           <TouchableOpacity style={styles.minus} onPress={() => {
              //             setQuantity(() => {
              //               if (quantity == 1) {
              //                 return 0
              //               }
              //               else {
              //                 return quantity - 1
              //               }
              //             })
              //           }}>
              //             <Text style={{ fontSize: 10, fontWeight: '700', color: '#EE4D2D' }}>-</Text>
              //           </TouchableOpacity>
              //           <Text style={{ marginHorizontal: 10, fontWeight: '700' }}>{quantity}</Text>
              //           <TouchableOpacity style={styles.plus} onPress={() => {
              //             setQuantity(quantity + 1)
              //           }}>
              //             <Text style={{ fontSize: 10, fontWeight: '700', color: '#fff' }}>+</Text>
              //           </TouchableOpacity>
              //         </View>
              //       </View>
              //     </View>
              //   ))
              // 
            }

          </View>

          <View style={styles.footer}>
            <View style={{
              width: '65%',
              justifyContent: 'center',
              alignItems: 'flex-end',
              paddingRight: 10,
            }}>
              <Text style={{ color: '#EE4D2D', fontWeight: '600' }}>đ{ }</Text>
            </View>
            <TouchableOpacity style={styles.orderButton}>
              <Text style={{ color: '#fff' }}>Giao hàng</Text>
            </TouchableOpacity>
          </View>

        </View>
      </Modal>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: "#F1EFEF"
  },

  header: {
    width: '100%',
    height: '30%'
  },

  viewPager: {
    flex: 1,
  },
  page: {
    justifyContent: 'center',
    alignItems: 'center',
  },
  image: {
    width: '100%',
    height: '100%',
  },

  title: {
    width: '100%',
    height: '15%',
    backgroundColor: '#fff'
  },

  name: {
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 5,
  },

  discountIcon: {
    width: 30,
    height: 30
  },

  price: {
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 5,
  },

  actualPrice: {
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'center',
    paddingLeft: 10,
    paddingVertical: 5,
  },

  detail: {
    width: '100%',
    height: '40%',
    backgroundColor: '#fff'
  },

  buttArea: {
    width: '100%',
    height: '8%',
    flexDirection: 'row'
  },

  buttLeft: {
    width: '50%',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#fff'
  },

  addCartIcon: {
    width: 40,
    height: 40
  },

  buttRight: {
    width: '50%',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#EE4D2D'
  },

  bottomSheet: {
    justifyContent: 'flex-end',
    margin: 0,
  },

  bottomSheetContainer: {
    backgroundColor: 'white',
    height: '70%',
    flexDirection: 'column',
    justifyContent: 'space-between',
    borderTopLeftRadius: 10,
    borderTopRightRadius: 10
  },

  headerBottomSheet: {
    height: 30,
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginVertical: 15,
    paddingHorizontal: 20,
  },

  bottomSheetContent: {
    width: '100%',
    height: '80%',
    backgroundColor: '#F1EFEF'
  },

  productCart: {
    width: '100%',
    flexDirection: 'row',
    paddingVertical: 10,
    paddingHorizontal: 20,
    marginVertical: 10,
    backgroundColor: '#fff'
  },

  bottomSheetImage: {
    width: 100,
    height: 100,
  },

  productDetail: {
    width: '70%',
    flexDirection: 'column',
    alignItems: 'flex-end',
  },

  productCartName: {
    paddingBottom: 3,
    fontSize: 16,
  },

  productCartActualPrice: {
    paddingBottom: 3,
    color: '#B4B4B3',
    textDecorationLine: 'line-through'
  },

  productCartPrice: {
    paddingBottom: 3,
    color: '#EE4D2D',
    fontWeight: '600'
  },

  quantity: {
    flexDirection: 'row'
  },

  minus: {
    width: 20,
    height: 20,
    borderWidth: 1,
    borderColor: '#EE4D2D',
    alignItems: 'center',
    backgroundColor: '#fff'
  },

  plus: {
    width: 20,
    height: 20,
    borderWidth: 1,
    borderColor: '#EE4D2D',
    alignItems: 'center',
    backgroundColor: '#EE4D2D'
  },

  footer: {
    width: '100%',
    height: 40,
    flexDirection: 'row',
  },

  orderButton: {
    width: '35%',
    alignItems: 'center',
    backgroundColor: '#EE4D2D',
    justifyContent: 'center',
    margin: 0,
  }

});

export default ProductScreen;