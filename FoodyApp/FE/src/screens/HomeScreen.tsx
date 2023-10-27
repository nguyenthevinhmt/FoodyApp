import { View, Text, StyleSheet, Image, TouchableOpacity, FlatList, TextInput, ScrollView } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useEffect, useState } from "react";
import Swiper from "react-native-swiper";
import { getAllCategory } from "../services/categoryService";
import { getListProduct } from "../services/productService";
import { baseURL_img } from "../utils/baseUrl";
import ScreenNames from "../utils/ScreenNames";

export default function HomeScreen({ navigation }: any) {
  const images = [
    require('../assets/images/food1.png'),
    require('../assets/images/food2.png'),
    require('../assets/images/food3.png'),
  ];

  //danh sách danh mục và sản phẩm
  const [listCategory, setListCategory] = useState([]);
  const [listProduct, setListProduct] = useState([]);

  //kết quả tìm kiếm
  const [searchText, setSearchText] = useState('');

  useEffect(() => {
    const getData = async () => {
      //danh sách các danh mục
      const categories = await getAllCategory(null, 20, 1);
      setListCategory(categories?.data.item);

      //danh sách món ăn 
      const products = await getListProduct(null, null, 5, 1);
      setListProduct(products?.data.item);
    }

    getData();
  }, []);

  const handleSearch = () => {
    // Điều hướng sang màn hình khác và truyền dữ liệu
    navigation.navigate(ScreenNames.PRODUCT_SEARCH, { productName: searchText });
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.headerArea}>
        <View style={styles.logoApp}>
          <Image
            source={require("../assets/images/Logo_app.png")}
            style={{
              width: 40,
              height: 50,
            }}
          />

          <Text style={{ color: '#EE4D2D', fontWeight: '500' }}>Foody - Ứng dụng đặt đồ ăn số một Việt Nam</Text>
        </View>

        <View style={styles.Search}>
          <Image
            source={require("../assets/Icons/SearchIcon.png")}
            style={styles.searchIcon}
          />

          <TextInput
            style={styles.input}
            placeholder=" Tìm kiếm loại món ăn, combo,..."
            onChangeText={value => setSearchText(value)}
            onSubmitEditing={handleSearch}
          >
          </TextInput>
        </View>
      </View>

      <View style={styles.View1}>
        <Swiper autoplay autoplayTimeout={5}>
          {images.map((image, index) => (
            <View key={index}>
              <Image source={image} style={styles.Img} />
            </View>
          ))}
        </Swiper>
      </View>

      <View style={styles.container1}>
        <View style={styles.view1}>
          <Text style={styles.text1}>Danh mục món ăn </Text>
        </View>

        <View style={{ marginVertical: 5, height: 70 }}>
          <FlatList
            horizontal
            data={listCategory}
            keyExtractor={(item: any) => item.id}
            renderItem={({ item }) => (
              <View style={{ justifyContent: 'center', height: 70 }}>
                <TouchableOpacity
                  style={styles.view2}
                  onPress={() =>
                    navigation.navigate(ScreenNames.PRODUCT_BY_CATEGORY, {
                      categoryId: item.id,
                      categoryName: item.name,
                      categoryDescription: item.description
                    })}>
                  <Text style={styles.text2}>{item.name}</Text>
                </TouchableOpacity>
              </View>
            )}
          />
        </View>

        <View style={styles.view3}>
          <View style={styles.view3_1}>
            <Text style={styles.text1}>Món ăn đề xuất</Text>
            <TouchableOpacity style={{ flex: 1 }} onPress={() => navigation.navigate(ScreenNames.ALL_PRODUCT)}>
              <Text style={styles.text3}>Xem tất cả </Text>
            </TouchableOpacity>
          </View>

          <ScrollView>
            {
              listProduct.map((item) => (
                <TouchableOpacity key={item['id']} style={styles.view4} onPress={() => navigation.navigate(ScreenNames.PRODUCT, { productId: item['id'] })}>
                  <Image
                    source={{ uri: `${baseURL_img}${item['productImageUrl']}` }}
                    style={styles.img2}
                  />

                  <View style={{ flexDirection: "row", paddingTop: 5, paddingLeft: 5 }}>
                    <Text style={styles.text4}>{item['name']}</Text>

                    <Text style={styles.text4_1}>  -   {item['description']}</Text>
                  </View>
                </TouchableOpacity>
              ))
            }
          </ScrollView>
        </View>
      </View>
    </SafeAreaView>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'space-around',
    paddingBottom: 70
  },

  headerArea: {
    width: '100%',
    padding: 7,
  },

  logoApp: {
    width: '100%',
    height: 60,
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 4
  },

  Location: {
    flexDirection: 'row',
  },

  Search: {
    height: 35,
    marginTop: 5,
    borderWidth: 1,
    borderRadius: 3,
    borderColor: '#EE4D2D',
    flexDirection: 'row',
  },

  locationText: {
    textAlign: 'left',
    fontSize: 13
  },

  locationChoose: {
    textAlign: 'left',
    fontSize: 13,
    fontWeight: '500',
    paddingTop: 5
  },

  input: {
    width: 330,
    height: 35,
  },

  locationIcon: {
    width: 13,
    height: 20,
    marginRight: 5
  },

  searchIcon: {
    margin: 5,
    width: 20,
    height: 23,
  },

  View1: {
    height: 200,
  },

  Img: {
    width: 400,
    height: 200,
  },

  container1: {
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
  },

  view3_1: {
    flexDirection: 'row',
    height: 50,
  },

  view4: {
    flexDirection: 'row',
    margin: 5,
  },

  text1: {
    padding: 5,
    fontSize: 16,
    color: 'black',
    fontWeight: '500'
  },

  text2: {
    width: 90,
    fontSize: 13,
    color: '#EE4D2D',
    textAlign: 'center',
  },

  text3: {
    padding: 7,
    flex: 1,
    fontSize: 13,
    color: '#362FD9',
    textAlign: 'right',

  },

  text4: {
    fontSize: 16,
    fontWeight: '500'
  },

  text4_1: {
    fontSize: 15,
    color: 'black',
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