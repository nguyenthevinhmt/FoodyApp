import { Text, View, StyleSheet, TouchableOpacity, ScrollView, Image } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React from 'react';
import PagerView from 'react-native-pager-view';
import { getProductById } from "../services/productService";
import { useEffect, useState } from "react";

interface ImageItem {
  id: string;
  uri: number;
}

const images: ImageItem[] = [
  { id: '2', uri: require('../assets/images/food1.jpg') },
  { id: '3', uri: require('../assets/images/food-demo.jpg') },
  { id: '4', uri: require('../assets/images/Sausage-Kid-Mania-1.jpg') },
];

const ProductScreen: React.FC = ({ navigation, route }: any) => {

  //id sản phẩm sẽ được truyền theo tham số từ các trang khác
  const [productId, setProductId] = useState(6)

  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);
  const [actualPrice, setActualPrice] = useState(0);
  const [description, setDescription] = useState('');
  const [imgUrl, setImgUrl] = useState('');

  useEffect(() => {
    const getData = async () => {
      const result = await getProductById(6);

      setName(result?.data['name']);
      setPrice(result?.data['price']);
      setActualPrice(result?.data['actualPrice']);
      setDescription(result?.data['description']);
      setImgUrl('http://192.168.1.10:5010' + result?.data['productImageUrl']);
      console.log(imgUrl);
    };

    getData();
  }, []);

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.header}>
        <PagerView style={styles.viewPager} initialPage={0}>

          <Image source={{ uri: imgUrl }} style={styles.image} />

          {images.map((image) => (
            <View key={image.id} style={styles.page}>
              <Image source={image.uri} style={styles.image} />
            </View>
          ))}
        </PagerView>
      </View>

      <View style={styles.title}>
        <View style={styles.name}>
          <Text style={{ fontSize: 16, fontWeight: '600' }}>{name}</Text>
          <Image source={require('../assets/Icons/discount.png')} style={styles.discountIcon} />
        </View>
        <View style={styles.price}>
          <Text style={{ color: '#EE4D2D', fontWeight: '600' }}>đ{price}</Text>
        </View>
        <View style={styles.actualPrice}>
          <Text style={{
            fontSize: 12,
            color: '#B4B4B3',
            textDecorationLine: 'line-through'
          }}>đ{actualPrice}</Text>
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
        <TouchableOpacity style={styles.buttLeft}>
          <Image source={require('../assets/Icons/add-cart.png')} style={styles.addCartIcon} />
          <Text style={{ color: '#EE4D2D' }}>Thêm vào giỏ hàng</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.buttRight}>
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
  }
});

export default ProductScreen;