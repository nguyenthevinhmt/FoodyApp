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
    require('../assets/images/banner1.png'),
    require('../assets/images/banner2.png'),
    require('../assets/images/banner3.png'),
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
      <ScrollView showsVerticalScrollIndicator = {false}>
        <View style={styles.banner}>
          <Swiper
            autoplay
            autoplayTimeout={5}
            showsPagination = {false}
          >
            {images.map((image, index) => (
              <View key={index}>
                <Image source={image} style={styles.bannerImg} />
              </View>
            ))}
          </Swiper>
        </View>

        <View style={styles.areaCategories}>
          <View>
            <Text style={styles.title}>Danh mục món ăn </Text>
          </View>

          <View style={styles.listCategories}>
            <FlatList
              horizontal
              data={listCategory}
              showsHorizontalScrollIndicator={false}
              keyExtractor={(item: any) => item.id}
              renderItem={({ item }) => (
                <View style={{ justifyContent: 'center' }}>
                  <TouchableOpacity
                    style={styles.category}
                    onPress={() =>
                      navigation.navigate(ScreenNames.PRODUCT_BY_CATEGORY, {
                        categoryId: item.id,
                        categoryName: item.name,
                        categoryDescription: item.description
                      })}>
                    <Text style={styles.categoryName}>{item.name}</Text>
                  </TouchableOpacity>
                </View>
              )}
            />
          </View>
        </View>

        <View style={styles.listProduct}>
          <View style={styles.listProductHeader}>
            <Text style={styles.title}>Món ăn đề xuất</Text>
            <TouchableOpacity style={{ flex: 1 }} onPress={() => navigation.navigate(ScreenNames.ALL_PRODUCT)}>
              <Text style={styles.allProduct}>Xem tất cả</Text>
            </TouchableOpacity>
          </View>

          <View>
            {
              listProduct.map((item) => (
                <TouchableOpacity key={item['id']} style={styles.product} onPress={() => navigation.navigate(ScreenNames.PRODUCT, { productId: item['id'] })}>
                  <Image
                    source={{ uri: `${baseURL_img}${item['productImageUrl']}` }}
                    style={styles.imgProduct}
                  />

                  <View style={{ flexDirection: "column", paddingTop: 5, paddingLeft: 5 }}>
                    <Text style={styles.name}>{item['name']}</Text>

                    <View style={{ flexDirection: 'column', alignItems: 'flex-start' }}>
                      <Text style={styles.price}>{item['price'].toLocaleString()}đ</Text>
                      <Text style={styles.actualPrice}>{item['actualPrice'].toLocaleString()}đ</Text>
                    </View>
                  </View>
                </TouchableOpacity>
              ))
            }
          </View>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'column',
    justifyContent: 'flex-start',
    backgroundColor: '#fefefe'
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

  Search: {
    height: 35,
    marginTop: 5,
    borderWidth: 0.2,
    borderRadius: 3,
    borderColor: '#EE4D2D',
    flexDirection: 'row',
  },

  input: {
    width: 330,
    height: 35,
  },

  searchIcon: {
    margin: 5,
    width: 20,
    height: 23,
  },

  banner: {
    height: 120,
  },

  bannerImg: {
    width: '100%',
    height: 120,
  },

  areaCategories: {
    paddingVertical: 20,
    //flex: 1,
    justifyContent: 'space-between',
  },

  title: {
    marginLeft: 10,
    fontSize: 16,
    color: 'black',
    fontWeight: '500'
  },

  listCategories: {
    paddingVertical: 10,
    paddingHorizontal: 7,
  },

  category: {
    flexDirection: 'column',
    paddingVertical: 10,
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 5,
    marginRight: 10,
    borderWidth: 0.2,
    borderRadius: 3,
    borderColor: '#EE4D2D'
  },

  categoryName: {
    width: 90,
    fontSize: 13,
    color: '#EE4D2D',
    textAlign: 'center',
  },

  listProduct: {
    flexDirection: 'column',
    width: '100%'
  },

  listProductHeader: {
    flexDirection: 'row',
    paddingVertical: 5
  },

  allProduct: {
    padding: 7,
    flex: 1,
    fontSize: 13,
    color: '#362FD9',
    textAlign: 'right',

  },

  product: {
    flexDirection: 'row',
    paddingHorizontal: 5,
    marginVertical: 5
  },

  imgProduct: {
    width: 100,
    height: 80
  },

  name: {
    fontSize: 16,
    fontWeight: '500',
    marginBottom: 2
  },

  price: {
    fontSize: 11,
    color: '#B4B4B3',
    textDecorationLine: 'line-through',
    marginRight: 5
  },

  actualPrice: {
    fontSize: 14,
    color: '#EE4D2D',
    fontWeight: '700'
  },

});