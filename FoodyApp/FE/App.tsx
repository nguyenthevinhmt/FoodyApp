import { LoginScreen } from "./src/screens/LoginScreen";
import { NavigationContainer, useNavigation } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { RegisterScreen } from "./src/screens/RegisterScreen";
import { useEffect, useState } from "react";
import MainScreen from "./src/screens/MainScreen";
import SplashScreen from "./src/screens/SplashScreen";
import ScreenNames from "./src/utils/ScreenNames";
import AccountScreen from "./src/screens/AccountScreen";
import UserScreen from "./src/screens/UserScreen";

const Stack = createNativeStackNavigator();
export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator
        screenOptions={{ headerShown: false }}
        initialRouteName="SplashScreen"
      >
        <Stack.Screen name={ScreenNames.SPLASH} component={SplashScreen} />
        <Stack.Screen name={ScreenNames.MAIN} component={MainScreen} />
        <Stack.Screen name={ScreenNames.LOGIN} component={LoginScreen} />
        <Stack.Screen name={ScreenNames.REGISTER} component={RegisterScreen} />
        <Stack.Screen name={ScreenNames.USER} component={UserScreen} />
        <Stack.Screen name={ScreenNames.ACCOUNT} component={AccountScreen} options={{headerShown: true, headerStyle: {backgroundColor: '#EE4D2D'}}}/>
      </Stack.Navigator>
    </NavigationContainer>
  );
}
