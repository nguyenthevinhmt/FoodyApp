import { Text, View, StyleSheet, TouchableOpacity, ScrollView, Image, Alert, ToastAndroid } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React from 'react';
import Modal from 'react-native-modal';
import PagerView from 'react-native-pager-view';
import { getProductById } from "../services/productService";
import { useEffect, useState } from "react";
import ScreenNames from "../utils/ScreenNames";
import { baseURL_img } from "../utils/baseUrl";
import { addProductToCart, getCartByUser } from "../services/cartService";
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

  const [products, setProducts] = useState<any[]>([]);

  //kiểm tra khi component con gọi api
  const [componentAction, setComponentAction] = useState(true);
  
  const [isModalVisible, setModalVisible] = useState(false);

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

  useEffect(() => {
    const getData = async () => {
      //lấy thông tin sản phẩm trong giỏ hàng
      const result = await getCartByUser();
      setProducts(result?.data.products);
    }

    getData();
  }, [componentAction]);

  
  const handleAddCart = async () => {
    const result = await addProductToCart(Id);
    const cartResult = await getCartByUser();
    setProducts(cartResult?.data.products);
  }
  
  const showAlert = () => {
    handleAddCart();
    ToastAndroid.show('Thêm sản phẩm thành công !', ToastAndroid.SHORT);
  };

  const toggleModal = () => {
    setModalVisible(!isModalVisible);
  };

  //theo dõi hoạt động trong component
  const handleComponent = () => {
    // Thực hiện các tác vụ khi component con gọi API
    console.log("Component con đã gọi API");
    setComponentAction(!componentAction);
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
        <TouchableOpacity style={styles.buttLeft} onPress={() => {
          toggleModal();
          showAlert()}
        }>
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
          <View style={styles.headerBottomSheet}>
            <Text style={{ fontSize: 18, fontWeight: '600' }}>Giỏ hàng</Text>

            <TouchableOpacity style={{ justifyContent: 'flex-start' }} onPress={() => { toggleModal() }}>
              <Text style={{
                fontSize: 20
              }}>X</Text>
            </TouchableOpacity>
          </View>

          {/* {danh sách các sản phẩm trong giỏ hàng} */}
          <ScrollView style={styles.bottomSheetContent}>
            {
              products.map((value) => (
                <View key={value['id']}>
                  <ProductCartComponent
                    productId={value['id']}
                    imageUrl={`${baseURL_img}${value['productImageUrl']}`}
                    name={value['name']}
                    actualPrice={value['actualPrice']}
                    price={value['price']}
                    Quantity={value['quantity']}
                    onNavigation={() => navigation.navigate(ScreenNames.PRODUCT, { productId: value['id'] })}
                    onAction={() => {handleComponent()}}
                  />
                </View>
              ))
            }
          </ScrollView>
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
    height: '70%',
    flexDirection: 'column',
    justifyContent: 'flex-start',
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
});

export default ProductScreen;