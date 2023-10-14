import { Text, View, StyleSheet, Button, Image } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { Logout } from "../services/authService";
import ScreenNames from "../utils/ScreenNames";
import UserEditButton from "../components/UserEditButton";
import { getUserById } from "../services/userService";
import { useCallback, useState } from "react";
import { getAccessToken } from "../services/authService";
import { useFocusEffect } from "@react-navigation/native";

export default function UserScreen({ navigation }: any) {
  const [lastName, setLastName] = useState<string>("");
  const [firstName, setFirstName] = useState<string>("");

  useFocusEffect(
    useCallback(() => {
      const getData = async () => {
        const jwt = require("jwt-decode");
        const token = await getAccessToken();
        const decode = jwt(token);

        const userId = decode.sub;
        console.log("User ID:", userId);

        const result = await getUserById(userId);
        setFirstName(result?.data["firstName"]);
        setLastName(result?.data["lastName"]);
      };
      getData();
    }, [])
  );

  //kiểm tra tên đã có chưa
  const check = () => {
    if (firstName == null && lastName == null) {
      return false;
    } else {
      return true;
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.profile}>
        <Image
          style={{
            height: 100,
            width: 100,
          }}
          source={require("../assets/Icons/icons8-male-user-96.png")}
        />
        <Text style={{ fontSize: 20, color: "#fff" }}>
          {check() ? `${lastName} ${firstName}` : "Không tên"}
        </Text>
      </View>

      <View style={styles.listButton}>
        <View style={styles.Button}>
          <UserEditButton
            imageUrl={require("../assets/Icons/icons8-user-shield-75.png")}
            text="Tài khoản và bảo mật"
            onNavigate={() => navigation.navigate(ScreenNames.ACCOUNT)}
          />
        </View>
        <View style={styles.Button}>
          <UserEditButton
            imageUrl={require("../assets/Icons/icons8-map-marker-100.png")}
            text="Địa chỉ"
            onNavigate={() => navigation.navigate(ScreenNames.ADDRESS)}
          />
        </View>
        <View style={styles.Button}>
          <UserEditButton
            imageUrl={require("../assets/Icons/icons8-loyalty-card-80.png")}
            text="Ví Voucher"
            onNavigate={() => navigation.navigate(ScreenNames.LOGIN)}
          />
        </View>
        <View style={styles.Button}>
          <UserEditButton
            imageUrl={require("../assets/Icons/icons8-bill-100.png")}
            text="Lịch sử"
            onNavigate={() => navigation.navigate(ScreenNames.LOGIN)}
          />
        </View>
        <View style={styles.Button}>
          <UserEditButton
            imageUrl={require("../assets/Icons/icons8-logout-rounded-100.png")}
            text="Đăng xuất"
            onNavigate={async () => {
              await Logout();
              navigation.navigate(ScreenNames.LOGIN);
            }}
          />
        </View>
      </View>
    </SafeAreaView>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#F1EFEF"
  },
  profile: {
    width: "100%",
    height: "30%",
    backgroundColor: "#EE4D2D",
    justifyContent: "flex-start",
    flexDirection: "row",
    alignItems: "center",
  },
  listButton: {
    //backgroundColor: '#ffc',
    width: "100%",
    height: "70%",
  },
  Button: {
    width: "100%",
    height: 60,
    justifyContent: "center",
    borderBottomWidth: 1,
  },
});
