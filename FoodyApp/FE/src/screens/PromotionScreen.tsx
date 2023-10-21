import { Text, StyleSheet, ScrollView, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { baseURL_img } from "../utils/baseUrl";
import ProductDiscountComponent from "../components/ProductDiscountComponent";
import ScreenNames from "../utils/ScreenNames";
import { useCallback, useState } from "react";
import { useFocusEffect } from "@react-navigation/native";
import { getProductDiscount } from "../services/productService";

export default function PromotionScreen({ navigation }: any) {
  const [product, setProduct] = useState([]);

  useFocusEffect(
    useCallback(() => {
      const getData = async () => {
        //lấy danh sách các sản phẩm giảm giá
        const productDiscountResponse = await getProductDiscount();
        setProduct(productDiscountResponse?.data.item);
      };

      getData();
    }, [])
  );

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.headerDetail}>Chương trình khuyến mại</Text>
      </View>

      <ScrollView style={styles.scroll}>
        {
          product.map((value) => (
            <ProductDiscountComponent
              key={value['id']}
              id={value['id']}
              imageUrl={`${baseURL_img}${value['productImageUrl']}`}
              name={value['name']}
              actualPrice={value['actualPrice']}
              price={value['price']}
              discount={value['promotion']['discountPercent']}
              onNavigation={() => navigation.navigate(ScreenNames.PRODUCT, { productId: value['id'] })}
            />
          ))
        }
      </ScrollView>

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
    height: 40,
    alignItems: 'center',
    justifyContent: 'center'
  },

  headerDetail: {
    color: '#EE4D2D',
    fontSize: 18,
    fontWeight: '500'
  },

  scroll: {
    width: '100%'
  }
});
