import { LoginScreen } from "./src/screens/LoginScreen";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { RegisterScreen } from "./src/screens/RegisterScreen";
import MainScreen from "./src/screens/MainScreen";
import SplashScreen from "./src/screens/SplashScreen";
import ScreenNames from "./src/utils/ScreenNames";
import UserScreen from "./src/screens/UserScreen";
import AccountScreen from "./src/screens/AccountScreen";
import AddressScreen from "./src/screens/AddressScreen";
import CreateAddressScreen from "./src/screens/CreateAddressScreen";
import UpdateAddressScreen from "./src/screens/UpdateAddressScreen";
import ProductScreen from "./src/screens/ProductScreen";
import CreateOrderScreen from "./src/screens/CreateOrderScreen";
import AllProductScreen from "./src/screens/AllProductScreen";
import ProductByCategoryScreen from "./src/screens/ProductByCategoryScreen";
import ProductSearchScreen from "./src/screens/ProductSearchScreen";
import CreateCartOrderScreen from "./src/screens/CreateCartOrderScreen";
import OrderScreen from "./src/screens/OrderScreen";
import DetailOrderScreen from "./src/screens/DetailOrderScreen";
import DetailOrderPendingScreen from "./src/screens/DetailOrderPendingScreen";
import DetailOrderShippingScreen from "./src/screens/DetailOrderShippingCreen";
import WebVnPay from "./src/screens/WebViewVnPayScreen";
import WebVnPayCart from "./src/screens/WebViewVnPayCartScreen";

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
        <Stack.Screen name={ScreenNames.ACCOUNT} component={AccountScreen} options={{ headerShown: true, headerStyle: { backgroundColor: '#EE4D2D' }, headerTintColor: '#FFFFFF', headerTitleStyle: { fontSize: 14 } }} />
        <Stack.Screen name={ScreenNames.ADDRESS} component={AddressScreen} options={{ headerShown: true, headerStyle: { backgroundColor: '#EE4D2D' }, headerTintColor: '#FFFFFF', headerTitleStyle: { fontSize: 14 } }} />
        <Stack.Screen name={ScreenNames.CREATEADDRESS} component={CreateAddressScreen} options={{ headerShown: true, headerStyle: { backgroundColor: '#EE4D2D' }, headerTintColor: '#FFFFFF', headerTitleStyle: { fontSize: 14 } }} />
        <Stack.Screen name={ScreenNames.UPDATEADDRESS} component={UpdateAddressScreen} options={{ headerShown: true, headerStyle: { backgroundColor: '#EE4D2D' } }} />
        <Stack.Screen name={ScreenNames.PRODUCT} component={ProductScreen} />
        <Stack.Screen name={ScreenNames.CREATE_ORDER} component={CreateOrderScreen} options={{ headerShown: true, headerStyle: { backgroundColor: '#EE4D2D' }, headerTintColor: '#FFFFFF', headerTitleStyle: { fontSize: 14 } }} />
        <Stack.Screen name={ScreenNames.ALL_PRODUCT} component={AllProductScreen} />
        <Stack.Screen name={ScreenNames.PRODUCT_BY_CATEGORY} component={ProductByCategoryScreen} />
        <Stack.Screen name={ScreenNames.PRODUCT_SEARCH} component={ProductSearchScreen} />
        <Stack.Screen name={ScreenNames.CREATE_CART_ORDER} component={CreateCartOrderScreen} options={{ headerShown: true, headerStyle: { backgroundColor: '#EE4D2D' }, headerTintColor: '#FFFFFF', headerTitleStyle: { fontSize: 14 } }} />
        <Stack.Screen name={ScreenNames.ORDER} component={OrderScreen} />
        <Stack.Screen name={ScreenNames.DETAIL_ORDER} component={DetailOrderScreen} />
        <Stack.Screen name={ScreenNames.DETAIL_ORDER_PENDING} component={DetailOrderPendingScreen} />
        <Stack.Screen name={ScreenNames.DETAIL_ORDER_SHIPPING} component={DetailOrderShippingScreen} />
        <Stack.Screen name={ScreenNames.VNPAY} component={WebVnPay}/>
        <Stack.Screen name={ScreenNames.VNPAY_CART} component={WebVnPayCart}/>
      </Stack.Navigator>
    </NavigationContainer>
  );
}
