import { StyleSheet } from "react-native";
import WellcomeScreen from "./src/screens/WellcomeScreen";
import { LoginScreen } from "./src/screens/LoginScreen";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { RegisterScreen } from "./src/screens/RegisterScreen";
import HomeScreen from "./src/screens/MainScreen";
import { useState } from "react";
import MainScreen from "./src/screens/MainScreen";

const Stack = createNativeStackNavigator();
export default function App() {
  const [isLogin, setIsLogin] = useState<boolean>(false);
  return (
    <NavigationContainer>
      {isLogin ? (
        <Stack.Navigator>
          <Stack.Screen
            options={{ headerShown: false }}
            name="WellcomeScreen"
            component={WellcomeScreen}
          />
          <Stack.Screen
            options={{ headerShown: false }}
            name="LoginScreen"
            component={LoginScreen}
          />
          <Stack.Screen
            options={{ headerShown: false }}
            name="RegisterScreen"
            component={RegisterScreen}
          />
          <Stack.Screen
            options={{ headerShown: false }}
            name="MainScreen"
            component={MainScreen}
          />
        </Stack.Navigator>
      ) : (
        <Stack.Navigator>
          <Stack.Screen
            options={{ headerShown: false }}
            name="MainScreen"
            component={MainScreen}
          />
        </Stack.Navigator>
      )}
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
