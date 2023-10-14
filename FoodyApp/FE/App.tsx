import { LoginScreen } from "./src/screens/LoginScreen";
import { NavigationContainer, useNavigation } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { RegisterScreen } from "./src/screens/RegisterScreen";
import { useEffect, useState } from "react";
import MainScreen from "./src/screens/MainScreen";
import SplashScreen from "./src/screens/SplashScreen";
import ScreenNames from "./src/utils/ScreenNames";
import UserScreen from "./src/screens/UserScreen";
import AccountScreen from "./src/screens/AccountScreen";
import AddressScreen from "./src/screens/AddressScreen";
import CreateAddressScreen from "./src/screens/CreateAddressScreen";
import UpdateAddressScreen from "./src/screens/UpdateAddressScreen";
import ProductScreen from "./src/screens/ProductScreen";

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
        <Stack.Screen name={ScreenNames.ADDRESS} component={AddressScreen} options={{headerShown: true, headerStyle: {backgroundColor: '#EE4D2D'}}}/>
        <Stack.Screen name={ScreenNames.CREATEADDRESS} component={CreateAddressScreen} options={{headerShown: true, headerStyle: {backgroundColor: '#EE4D2D'}}}/>
        <Stack.Screen name={ScreenNames.UPDATEADDRESS} component={UpdateAddressScreen} options={{headerShown: true, headerStyle: {backgroundColor: '#EE4D2D'}}}/>
        <Stack.Screen name={ScreenNames.PRODUCT} component={ProductScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
