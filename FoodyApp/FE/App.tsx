import { StyleSheet } from "react-native";
import { LoginScreen } from "./src/screens/LoginScreen";
import { NavigationContainer, useNavigation } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { RegisterScreen } from "./src/screens/RegisterScreen";
import { useEffect, useState } from "react";
import MainScreen from "./src/screens/MainScreen";
import SplashScreen from "./src/screens/SplashScreen";
import {
  Logout,
  getAccessToken,
  getRefreshToken,
  refreshAccessToken,
  saveToken,
} from "./src/services/authService";

const Stack = createNativeStackNavigator();
export default function App() {
  const jwt_decode = require("jwt-decode");
  const [isLogin, setIsLogin] = useState<boolean>(false);
  const setRefreshToken = async (objRefreshToken: any) => {
    try {
      const response = await refreshAccessToken(objRefreshToken);
      const newAccessToken = response?.accessToken;
      const newRefreshToken = response?.refreshToken;
      await saveToken({ newAccessToken, newRefreshToken });
      setIsLogin(true);
    } catch (error) {
      console.log("Có lỗi khi refresh token");
      // await Logout();
      setIsLogin(false);
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
          setIsLogin(true);
        } else {
          setIsLogin(false);
        }
      } else {
        setIsLogin(false);
      }
    };
    checkTokenValidity();
  }, [isLogin]);
  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen name="SplashScreen" component={SplashScreen} />
        <Stack.Screen name="MainScreen" component={MainScreen} />
        <Stack.Screen name="LoginScreen" component={LoginScreen} />
        <Stack.Screen name="RegisterScreen" component={RegisterScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
  },
});
