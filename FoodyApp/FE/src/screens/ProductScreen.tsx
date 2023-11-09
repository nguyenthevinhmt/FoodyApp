import { Text, View, StyleSheet, TouchableOpacity, ScrollView, Image, Alert, ToastAndroid } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React from 'react';
import Modal from 'react-native-modal';
import PagerView from 'react-native-pager-view';
import { getProductById } from "../services/productService";
import { useEffect, useState } from "react";
import ScreenNames from "../utils/ScreenNames";
import { baseURL_img } from "../utils/baseUrl";
import { addProductToCart, getCartByUser, updateProductQuantity } from "../services/cartService";
import ProductCartComponent from "../components/ProductCartComponent";


const ProductScreen = ({ navigation, route }: any) => {
  const Id = route.params['productId'];

  //kiểm tra sản phẩm có được giảm giá ko thì sẽ hiển thị icon lên
  const [discount, checkDiscount] = useState(true);

  //thông tin sản phẩm
  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);
  const [actualPrice, setActualPrice] = useState(0);
  const [description, setDescription] = useState('');
  const [imgUrl, setImgUrl] = useState(baseURL_img);
  
  const [isModalVisible, setModalVisible] = useState(false);

  //Thêm số lượng
  const [quantity, setQuantity] = useState(1);

  useEffect(() => {
    const getData = async () => {
      //lấy thông tin sản phẩm theo id
      const product = await getProductById(Id);

      setName(product?.data['name']);
      setPrice(product?.data['price']);
      setActualPrice(product?.data['actualPrice']);
      setDescription(product?.data['description']);
      setImgUrl(baseURL_img + product?.data['productImageUrl']);

      if (product?.data['promotion'] != null) {
        checkDiscount(true);
      }
      else {
        checkDiscount(false);
      }
    };

    getData();
  }, []);
  
  const handleAddCart = async () => {
    const cartResult = await getCartByUser();
    const productCart = cartResult?.data['products'];
    const filteredProductCart = productCart.filter((obj: any) => obj.id === Id);
    if (filteredProductCart.length > 0) {
      await updateProductQuantity(Id, quantity);
    }
    else {
      const create = await addProductToCart(Id);
      if (create?.status === 200)
        await updateProductQuantity(Id, quantity-1);
    }
  }
  
  const showAlert = () => {
    handleAddCart();
    ToastAndroid.show('Thêm sản phẩm thành công !', ToastAndroid.SHORT);
    toggleModal();
  };

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
          <Text style={{ color: '#EE4D2D', fontWeight: '600' }}>{actualPrice.toLocaleString()}đ</Text>
        </View>

        <View style={styles.actualPrice}>
          <Text style={{
            fontSize: 12,
            color: '#B4B4B3',
            textDecorationLine: 'line-through'
          }}>{price.toLocaleString()}đ</Text>
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
        <TouchableOpacity style={styles.buttLeft} onPress={() => {toggleModal()}}>
          <Image source={require('../assets/Icons/add-cart.png')} style={styles.addCartIcon} />
          <Text style={{ color: '#EE4D2D', fontSize: 12 }}>Thêm vào giỏ hàng</Text>
          
        </TouchableOpacity>

        <TouchableOpacity style={styles.buttRight}
          onPress={() => navigation.navigate(ScreenNames.CREATE_ORDER, {
            id: Id,
            productName: name,
            price: price,
            actualPrice: actualPrice,
            imgUrl: imgUrl,
            quantity: 1
          })}>
          <Text style={{ color: '#fff', fontSize: 14 }}>Mua ngay</Text>
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
          <View>
          <View style={styles.headerBottomSheet}>
            <Text style={{ fontSize: 18, fontWeight: '600' }}>Thêm sản phẩm vào giỏ</Text>

            <TouchableOpacity style={{ justifyContent: 'flex-start' }} onPress={() => { toggleModal() }}>
              <Text style={{
                fontSize: 20
              }}>X</Text>
            </TouchableOpacity>
          </View>

          <View style={styles.containerModal}>
            <Image source={{ uri: imgUrl }} style={styles.imageModal} />

            <View style={styles.productDetailModal}>
              <View>
                <Text style={styles.nameModal}>{name}</Text>
                <View style={{ width: '100%', flexDirection: 'row', alignItems: 'center', marginTop: 10 }}>
                  <Text style={styles.priceModal}>{price.toLocaleString()}đ</Text>
                  <Text style={styles.actualPriceModal}>{actualPrice.toLocaleString()}đ</Text>
                </View>
              </View>

              <View style={styles.updateQuantityModal}>
                <TouchableOpacity 
                  style={styles.subtractionModal} 
                  onPress={() => {
                    if (quantity - 1 >= 1)
                      setQuantity(quantity => quantity - 1)
                  }}>
                  <Image source={require('../assets/Icons/subtraction-logo.png')} style={styles.quantity_logoModal} />
                </TouchableOpacity>

                <Text style={styles.quantityModal}>{quantity}</Text>

                <TouchableOpacity style={styles.additionModal} onPress={() => setQuantity(quantity => quantity + 1)}>
                  <Image source={require('../assets/Icons/addition-logo.png')} style={styles.quantity_logoModal} />
                </TouchableOpacity>
              </View>
            </View>
          </View>
          </View>

          <TouchableOpacity style={styles.addCartButton} onPress={() => showAlert()}>
            <Text style={{color: '#fff'}}>Thêm</Text>
          </TouchableOpacity>
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
    width: 25,
    height: 25
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
    height: '45%',
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
    height: '100%',
    backgroundColor: '#F1EFEF'
},

containerModal: {
  width: '100%',
  marginBottom: 10,
  paddingVertical: 15,
  flexDirection: 'row',
  justifyContent: 'flex-start',
  alignItems: 'center',
  backgroundColor: '#fff'
},

imageModal: {
  width: 90,
  height: 90,
  marginHorizontal: 10,
},

productDetailModal: {
  height: 110,
  justifyContent: 'space-around',
  flexDirection: 'column',
  alignItems: 'flex-start'
},

nameModal: {
  fontSize: 16,
  fontWeight: '400'
},

priceModal: {
  fontSize: 12,
  color: '#B4B4B3',
  marginRight: 6,
  textDecorationLine: 'line-through'
},

actualPriceModal: {
  fontSize: 12,
  color: '#EE4D2D',
  fontWeight: '700'
},

updateQuantityModal: {
  flexDirection: 'row',
},

subtractionModal: {
  width: 25,
  height: 25,
  alignItems: 'center',
  justifyContent: 'center',
  borderWidth: 0.7,
  borderColor: '#EE4D2D'
},

quantityModal: {
  marginHorizontal: 15
},

additionModal: {
  width: 25,
  height: 25,
  alignItems: 'center',
  justifyContent: 'center',
  borderWidth: 0.7,
  borderColor: '#EE4D2D',
  backgroundColor: '#EE4D2D'
},

quantity_logoModal: {
  width: 20,
  height: 20
},

addCartButton: {
  justifyContent: 'center',
  alignItems: 'center',
  height: 40,
  backgroundColor: '#EE4D2D',
}
});

export default ProductScreen;