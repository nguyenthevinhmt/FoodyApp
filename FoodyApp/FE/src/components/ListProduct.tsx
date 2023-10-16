import { Button, View, Text, StyleSheet, FlatList, TextInput, Image } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React, { useEffect, useState } from "react";
import categoryService from "../services/categoryService";
import productService from "../services/productService";
import {baseURL_img} from "../utils/baseUrl"; 
// import RNPickerSelect from 'react-native-picker-select';

export default function ListProduct() {
  const [listCategory, setListCategory] = useState([]);
  const [listProduct, setListProduct] = useState([]);
  // const [productDetail, setPoductDetail] = useState(Object);
  // const [Name, setName] = useState(String);
  // const [categoryId, setCategoryId] = useState(String);
  // const [endPrice, setEndPrice] = useState(String);
  // const [startPrice, setStartPrice] = useState(String);
  useEffect(() => {
    categoryService.getAllCategory().then((response) => {
      setListCategory(response.data.item);
      console.log(response.data.item);
    }
    ).catch((error) => {
      console.error('Lỗi khi lấy dữ liệu từ API getAllCategory:', error);
    });
    productService.getListProduct(1)
      .then((response) => {
        setListProduct(response.data.item);
        console.log(listProduct);
      })
      .catch((error) => {
        console.error('Lỗi khi lấy dữ liệu từ API getAllProduct:', error);
      });

    // productService.getProductbyId('5')
    //   .then((response) => {
    //     console.log('Detail:', response.data)
    //     setPoductDetail(response.data);
    //   })
    //   .catch((error) => {
    //     console.error('Lỗi khi lấy dữ liệu từ API getProductbyId:', error);
    //   });
  }, []);

  return (
    <View style={styles.container}>
      <View style={styles.view1}>
        <Image
          source={require("../assets/IconNavigation/HomeIcon.png")}
          style={styles.img}
        />
        <Text style={styles.text1}>Danh mục món ăn </Text>
      </View>
      <FlatList
        horizontal
        data={listCategory}
        keyExtractor={(item: any) => item.id.toString()}
        renderItem={({ item }) => (
          <View style={styles.view2}>
            <Text style={styles.text2}>{item.description}</Text>
          </View>
        )}
      />
      <View style={styles.view3}>
        <View style={styles.view3_1}>
          <Text style={styles.text1}>Món ăn đề xuất</Text>
          <Text style={styles.text3}>Xem tất cả {">"} </Text>
        </View>

        <FlatList
          data={listProduct}
          keyExtractor={(item: any) => item.id.toString()}
          renderItem={({ item }) => (
            <View style={styles.view4}>
              {/* <Image
                source={require("../assets/images/food1.png")}
                style={styles.img2}
              /> */}
              <Image
                source={{ uri: `${baseURL_img}${item.productImageUrl}` }}
                style={styles.img2}
              />
              <Text style={styles.text4}>{item.name}</Text>
              <Text style={styles.text4}>-   {item.description}</Text>
            </View>
          )}
        />
      </View>
    </View>
  );
}
const styles = StyleSheet.create({
  container: {
    padding: 7,
    flex: 1,
  },
  view1: {
    flexDirection: 'row'
  },
  view2: {
    height: 40,
    // backgroundColor: 'pink'
  },
  view3: {
    flexDirection: 'column',
    height: 300,
    // backgroundColor: 'pink'
  },
  view3_1: {
    flexDirection: 'row',
    height: 30,
    // backgroundColor: 'yellow'
  },
  view4: {
    flexDirection: 'row',
    margin: 5,
    // backgroundColor: 'pink'
  },
  text1: {
    padding: 5,
    fontSize: 16,
    color: 'orange',
  },
  text2: {
    margin: 5,
    width: 65,
    height: 40,
    fontSize: 13,
    color: 'black',
    textAlign: 'center',
    // backgroundColor: 'red'
  },
  text3: {
    padding: 7,
    flex: 1,
    fontSize: 13,
    color: 'black',
    textAlign: 'right',
    // backgroundColor: 'red'
  },
  text4: {
    padding: 7,
    fontSize: 15,
    color: 'black',
    // backgroundColor: 'red'
  },
  img: {
    width: 21,
    height: 25
  },
  img2: {
    width: 100,
    height: 80
  }
});