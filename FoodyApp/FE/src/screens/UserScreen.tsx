import { Text, View, StyleSheet, Image, Button, TouchableOpacity } from "react-native";
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
      <View style={{ width: '100%', flex: 13 }}>
        <View style={styles.profile}>
          <Image
            style={{
              height: 80,
              width: 80,
            }}
            source={require("../assets/Icons/userIcon.png")}
          />

          <Text style={{ fontSize: 20, color: "#fff" }}>
            {check() ? `${lastName} ${firstName}` : "Không tên"}
          </Text>
        </View>

        <View style={styles.listButton}>
          <View style={styles.Button}>
            <UserEditButton
              imageUrl={require("../assets/Icons/account.png")}
              text="Tài khoản và bảo mật"
              onNavigate={() => navigation.navigate(ScreenNames.ACCOUNT)}
            />
          </View>

          <View style={styles.Button}>
            <UserEditButton
              imageUrl={require("../assets/Icons/address.png")}
              text="Địa chỉ"
              onNavigate={() => navigation.navigate(ScreenNames.ADDRESS)}
            />
          </View>

          <View style={styles.Button}>
            <UserEditButton
              imageUrl={require("../assets/Icons/history.png")}
              text="Lịch sử"
              onNavigate={() => navigation.navigate(ScreenNames.MAIN, { screen: 'Order', params: { screen: 'Đã vận chuyển' } })}
            />
          </View>
        </View>
      </View>

      <View style={{ width: '100%', flex: 1, alignItems: 'center' }}>
        <TouchableOpacity style={styles.exit} onPress={async () => {
          await Logout();
          navigation.navigate(ScreenNames.LOGIN);
        }}>
          <Text style={{ color: '#FFF' }}>Đăng xuất</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "space-between",
    alignItems: "center",
    backgroundColor: "#fafafa"
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
    width: "100%",
    height: "70%",
  },

  Button: {
    width: "100%",
    height: 60,
    justifyContent: "center",
    borderBottomWidth: 1,
    borderColor: "#ccc"
  },

  exit: {
    width: '90%',
    alignItems: 'center',
    backgroundColor: '#EE4D2D',
    paddingVertical: 10,
    borderRadius: 5
  }
});
