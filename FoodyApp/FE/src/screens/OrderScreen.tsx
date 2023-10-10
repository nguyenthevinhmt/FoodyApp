import { Text, StyleSheet, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { createMaterialTopTabNavigator } from '@react-navigation/material-top-tabs';
import PendingOrderScreen from "./PendingOrderScreen";
import ShippingOrderScreen from "./ShippingOrderScreen";
import SuccessOrderScreen from "./SuccessOrderScreen";
import CancelOrderScreen from "./CancelOrderScreen";

const Tab = createMaterialTopTabNavigator();

export default function OrderScreen() {
  return (
    <SafeAreaView style={{flex: 1}}>
    <Tab.Navigator>
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
      
      <Tab.Screen 
        name="Đã hủy" 
        component={CancelOrderScreen} 
      />
    </Tab.Navigator>    
    </SafeAreaView>
  );
}
