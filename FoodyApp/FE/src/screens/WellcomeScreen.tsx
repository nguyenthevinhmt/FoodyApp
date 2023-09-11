import { useEffect } from "react";
import { StyleSheet, View, Image, Text, Button } from "react-native";

export default function WellcomeScreen({ navigation }: any) {
  const logoImage = require("../assets/images/Logo_app.png");
  useEffect(() => {
    const timer = setTimeout(() => {
      navigation.navigate("LoginScreen");
    }, 2000);
    return () => {
      clearTimeout(timer);
    };
  }, []);
  return (
    <View style={styles.container}>
      <Image style={styles.logo} source={logoImage} />
      <Text style={styles.textLogo}>Foody</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#EE4D2D",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
  },
  logo: {
    width: 150,
    height: 215,
  },
  textLogo: {
    color: "#FFF",
    textAlign: "center",
    // fontFamily: "InknutAntqua-Regular",
    fontSize: 20,
  },
});
