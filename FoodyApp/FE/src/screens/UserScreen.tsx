import { Text, StyleSheet, Button } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { Logout } from "../services/authService";

export default function UserScreen({ navigation }: any) {
  return (
    <SafeAreaView style={styles.container}>
      <Text style={{ marginBottom: 20 }}>User Screen work!</Text>
      <Button
        title="Logout"
        onPress={async () => {
          await Logout();
          navigation.navigate("LoginScreen");
        }}
      />
    </SafeAreaView>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
});
