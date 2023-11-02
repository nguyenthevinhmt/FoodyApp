import { SafeAreaView } from "react-native-safe-area-context";
import { createMaterialTopTabNavigator } from '@react-navigation/material-top-tabs';
import PendingOrderScreen from "./PendingOrderScreen";
import ShippingOrderScreen from "./ShippingOrderScreen";
import SuccessOrderScreen from "./SuccessOrderScreen";

const Tab = createMaterialTopTabNavigator();

export default function OrderScreen() {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <Tab.Navigator
        screenOptions={{
          tabBarActiveTintColor: '#EE4D2D',
          tabBarInactiveTintColor: 'gray',
          tabBarLabelStyle: { fontSize: 10, textTransform: 'none'},
        }}>
        <Tab.Screen
          name="Chờ xử lý"
          component={PendingOrderScreen}
        />

        <Tab.Screen
          name="Đang vận chuyển"
          component={ShippingOrderScreen}
        />

        <Tab.Screen
          name="Đã vận chuyển"
          component={SuccessOrderScreen}
        />
      </Tab.Navigator>
    </SafeAreaView>
  );
}
