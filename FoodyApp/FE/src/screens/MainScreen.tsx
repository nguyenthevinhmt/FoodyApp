import { Image, View, Text } from "react-native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import OrderScreen from "./OrderScreen";
import HomeScreen from "./HomeScreen";
import PromotionScreen from "./PromotionScreen";
import CartScreen from "./CartScreen";
import UserScreen from "./UserScreen";

const Tab = createBottomTabNavigator();

export default function MainScreen() {
  return (
    <Tab.Navigator
      screenOptions={{
        tabBarHideOnKeyboard: true,
      }}
    >
      <Tab.Screen
        options={{
          headerShown: false,
          tabBarLabel: ({ focused }) => (
            <View style={{ alignItems: "center" }}>
              <Image
                source={require("../assets/IconNavigation/HomeIcon.png")}
                style={{
                  width: 20,
                  height: 24,
                  tintColor: focused ? "#EE4D2D" : "gray",
                }}
              />

              <Text
                style={{ color: focused ? "#EE4D2D" : "gray", fontSize: 12 }}
              >
                Home
              </Text>
            </View>
          ),
        }}
        name="Home"
        component={HomeScreen}
      />

      <Tab.Screen
        options={{
          headerShown: false,
          tabBarLabel: ({ focused }) => (
            <View style={{ alignItems: "center" }}>
              <Image
                source={require("../assets/IconNavigation/OrderIcon.png")}
                style={{
                  width: 20,
                  height: 24,
                  tintColor: focused ? "#EE4D2D" : "gray",
                }}
              />

              <Text
                style={{ color: focused ? "#EE4D2D" : "gray", fontSize: 12 }}
              >
                Đơn hàng
              </Text>
            </View>
          ),
        }}
        name="Order"
        component={OrderScreen}
      />

      <Tab.Screen
        options={{
          headerShown: false,
          tabBarLabel: ({ focused }) => (
            <View style={{ alignItems: "center" }}>
              <Image
                source={require("../assets/IconNavigation/PromotionIcon.png")}
                style={{
                  width: 20,
                  height: 24,
                  tintColor: focused ? "#EE4D2D" : "gray",
                }}
              />

              <Text
                style={{ color: focused ? "#EE4D2D" : "gray", fontSize: 12 }}
              >
                Khuyến mại
              </Text>
            </View>
          ),
        }}
        name="Promotion"
        component={PromotionScreen}
      />

      <Tab.Screen
        options={{
          headerShown: false,
          tabBarLabel: ({ focused }) => (
            <View style={{ alignItems: "center" }}>
              <Image
                source={require("../assets/IconNavigation/CartIcon.png")}
                style={{
                  width: 22,
                  height: 26,
                  tintColor: focused ? "#EE4D2D" : "gray",
                }}
              />

              <Text
                style={{ color: focused ? "#EE4D2D" : "gray", fontSize: 12 }}
              >
                Giỏ hàng
              </Text>
            </View>
          ),
        }}
        name="Cart"
        component={CartScreen}
      />

      <Tab.Screen
        options={{
          headerShown: false,
          tabBarLabel: ({ focused }) => (
            <View style={{ alignItems: "center" }}>
              <Image
                source={require("../assets/IconNavigation/UserIcon.png")}
                style={{
                  width: 20,
                  height: 24,
                  tintColor: focused ? "#EE4D2D" : "gray",
                }}
              />

              <Text
                style={{ color: focused ? "#EE4D2D" : "gray", fontSize: 12 }}
              >
                Tôi
              </Text>
            </View>
          ),
        }}
        name="User"
        component={UserScreen}
      />
    </Tab.Navigator>
  );
}
