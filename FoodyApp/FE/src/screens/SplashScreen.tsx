import { useEffect } from "react";
import { StyleSheet, View, Image, Text } from "react-native";
import {
  Logout,
  getAccessToken,
  getRefreshToken,
  refreshAccessToken,
} from "../services/authService";
import ScreenNames from "../utils/ScreenNames";
import { baseURL } from "../utils/baseUrl";

export default function SplashScreen({ navigation }: any) {
  const logoImage = require("../assets/images/Logo_app.png");
  const jwt_decode = require("jwt-decode");

  const setRefreshToken = async (accessToken: string, refreshToken: string) => {
    try {
      const response = await refreshAccessToken(accessToken, refreshToken);
      // const newAccessToken = response?.newAccessToken;
      // const newRefreshToken = response?.newRefreshToken;
      console.log("fresh here", response);
      navigation.navigate(ScreenNames.MAIN);
    } catch (error) {
      console.log("Có lỗi khi refresh token", `${baseURL}/Auth/refresh`);
      navigation.navigate(ScreenNames.LOGIN);
      await Logout();
    }
  };
  useEffect(() => {
    const timer = setTimeout(() => {}, 2000);

    return () => {
      clearTimeout(timer);
    };
  }, []);
  useEffect(() => {
    const checkTokenValidity = async () => {
      const accessToken = await getAccessToken();
      const refreshToken = await getRefreshToken();
      console.log("accessToken: ", accessToken, "refreshToken: ", refreshToken);

      if (accessToken !== null) {
        const decodedToken = jwt_decode(accessToken);
        const currentTime = Math.floor(Date.now() / 1000);
        console.log("chạy 1");

        if (decodedToken.exp < currentTime) {
          await setRefreshToken(accessToken, refreshToken as string);
          console.log("Token hết hạn, đang thử lấy lại");
        } else {
          console.log("Token còn hạn");
          navigation.replace(ScreenNames.MAIN);
        }
      } else {
        console.log("Token hết hạn, mời đăng nhập lại");
        navigation.replace(ScreenNames.LOGIN);
      }
    };
    checkTokenValidity();
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
