import { TouchableOpacity, View, Text, StyleSheet, FlatList, TextInput, Image } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import React, { useEffect, useState } from "react";
import {getAllCategory} from "../services/categoryService";
import {getListProduct} from "../services/productService";
import {baseURL_img} from "../utils/baseUrl"; 
import ScreenNames from "../utils/ScreenNames";
// import RNPickerSelect from 'react-native-picker-select';

const ListProduct = ({ navigation, route }: any) => {
  const [listCategory, setListCategory] = useState([]);
  const [listProduct, setListProduct] = useState([]);

  useEffect(() => {
    const getData = async () => {
      //danh sách các danh mục
      const categories = await getAllCategory();
      setListCategory(categories?.data.item);
      
      //danh sách món ăn 
      const products = await getListProduct(1);
      setListProduct(products?.data.item);
    }
    
    getData();
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

      <View style={{marginVertical: 5 ,height: 70}}>
      <FlatList
        horizontal
        data={listCategory}
        keyExtractor={(item: any) => item.id.toString()}
        renderItem={({ item }) => (
          <View style={{ justifyContent: 'center', height: 70 }}>
            <TouchableOpacity style={styles.view2}>
              <Text style={styles.text2}>{item.name}</Text>
            </TouchableOpacity>
          </View>
        )}
      />
      </View>

      <View style={styles.view3}>
        <View style={styles.view3_1}>
          <Text style={styles.text1}>Món ăn đề xuất</Text>
          <TouchableOpacity style={{flex: 1}}>
          <Text style={styles.text3}>Xem tất cả {">"} </Text>
          </TouchableOpacity>
        </View>

        <FlatList
          data={listProduct}
          keyExtractor={(item: any) => item.id}
          renderItem={({ item }) => (
    
            <TouchableOpacity style={styles.view4} onPress={() => navigation.navigate(ScreenNames.PRODUCT, {productId: item.id})}>
              <Image
                source={{ uri: `${baseURL_img}${item.productImageUrl}` }}
                style={styles.img2}
              />
              <View style={{flexDirection: "row", paddingTop: 5, paddingLeft: 5}}>
              <Text style={styles.text4}>{item.name}</Text>
              <Text style={styles.text4_1}>  -   {item.description}</Text>
              </View>
            </TouchableOpacity>
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
    justifyContent: 'space-between',
  },
  view1: {
    flexDirection: 'row'
  },
  view2: {
    flexDirection: 'column',
    height: 50,
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 5,
    marginRight: 10,
    borderWidth: 1,
    borderRadius: 3,
    borderColor: '#EE4D2D'
  },
  view3: {
    flexDirection: 'column',
    //height: 320,
    //backgroundColor: 'pink'
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
    width: 90,
    fontSize: 13,
    color: '#EE4D2D',
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
    fontSize: 16,
    fontWeight: '500'
  },
  text4_1: {
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

export default ListProduct;