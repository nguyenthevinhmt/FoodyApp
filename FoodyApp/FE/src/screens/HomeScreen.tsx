import { Button, View, Text, StyleSheet, Image } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useEffect, useState } from "react";
// import productService from "../services/productService";
import ListProduct from "../components/ListProduct";
import Header from "../components/Header";
import Swiper from "react-native-swiper";

export default function HomeScreen() {

  const images = [
    require('../assets/images/food1.png'),
    require('../assets/images/food2.png'),
    require('../assets/images/food3.png'),
  ];

  return (
    <SafeAreaView style={styles.container}>
      <Header ></Header>
      <View style={styles.view1}>
        <Swiper
          // style={styles.wrapper}
          autoplay
          autoplayTimeout={5}
        >
          {images.map((image, index) => (
            <View key={index}>
              <Image source={image} style={styles.img} />
            </View>
          ))}
        </Swiper>
      </View>
      <ListProduct></ListProduct>
    </SafeAreaView>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  view1: {
    height: 200,
  },
  img: {
    width: 400,
    height: 200,
  }
});
