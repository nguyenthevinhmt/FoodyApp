import { useEffect } from "react";
import { StyleSheet, View, Image, Text } from "react-native";
import {
  Logout,
  getAccessToken,
  getRefreshToken,
  refreshAccessToken,
  saveToken,
} from "../services/authService";
import ScreenNames from "../utils/ScreenNames";

export default function SplashScreen({ navigation }: any) {
  const logoImage = require("../assets/images/Logo_app.png");
  const jwt_decode = require("jwt-decode");

  const setRefreshToken = async (objRefreshToken: any) => {
    try {
      const response = await refreshAccessToken(objRefreshToken);
      const newAccessToken = response?.accessToken;
      const newRefreshToken = response?.refreshToken;
      
      await saveToken({ newAccessToken, newRefreshToken });
      navigation.navigate(ScreenNames.MAIN);
    } catch (error) {
      console.log("Có lỗi khi refresh token");
      await Logout();
      navigation.navigate(ScreenNames.LOGIN);
    }
  };

  useEffect(() => {
    const checkTokenValidity = async () => {
      const accessToken = await getAccessToken();
      const refreshToken = await getRefreshToken();
      const objRefreshToken: any = { accessToken, refreshToken };
      console.log("accessToken: ", accessToken, "refreshToken: ", refreshToken);

      if (accessToken !== null) {
        const decodedToken = jwt_decode(accessToken);
        const currentTime = Math.floor(Date.now() / 1000);

        if (decodedToken.exp < currentTime) {
          await setRefreshToken(objRefreshToken);
        } else {
          navigation.replace(ScreenNames.MAIN);
        }
      } else {
        navigation.replace(ScreenNames.LOGIN);
      }
    };

    const timer = setTimeout(() => {
      checkTokenValidity();
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
    fontSize: 20,
  },
});