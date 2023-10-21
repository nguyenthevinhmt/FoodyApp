import { Text, View, StyleSheet, TouchableOpacity, ScrollView, Image } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React from 'react';
import PagerView from 'react-native-pager-view';
import { getProductById } from "../services/productService";
import { useEffect, useState } from "react";
import ScreenNames from "../utils/ScreenNames";
import { baseURL_img } from "../utils/baseUrl";
import { addProductToCart } from "../services/cartService";
import Alert from "../components/CustomAlert";

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

  //hiển thị alert
  const [showAlert, setShowAlert] = useState(false);
  const [message, setMessage] = useState('');

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
    const result = await addProductToCart(Id);
    if (result != null) {
      console.log(result);
      setMessage('Thêm sản phẩm vào giỏ hàng thành công.');
      setShowAlert(true);
    }
    else {
      setMessage('Không thể thêm sản phẩm vào giỏ hàng.');
      setShowAlert(true);
    }
  }

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
        <TouchableOpacity style={styles.buttLeft} onPress={() => handleAddCart()}>
          <Image source={require('../assets/Icons/add-cart.png')} style={styles.addCartIcon} />
          <Text style={{ color: '#EE4D2D' }}>Thêm vào giỏ hàng</Text>
        </TouchableOpacity>

        <Alert
          visible={showAlert}
          message={message}
          onClose={() => setShowAlert(false)}
        />

        <TouchableOpacity style={styles.buttRight}
          onPress={() => navigation.navigate(ScreenNames.CREATE_ORDER, {
            id: Id,
            productName: name,
            price: price,
            actualPrice: actualPrice,
            imgUrl: imgUrl,
            quantity: 1
          })}>
          <Text style={{ color: '#fff' }}>Mua ngay</Text>
        </TouchableOpacity>
      </View>
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
});

export default ProductScreen;