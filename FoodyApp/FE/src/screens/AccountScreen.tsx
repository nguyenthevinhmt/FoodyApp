import {
  Text,
  View,
  StyleSheet,
  TextInput,
  TouchableOpacity,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import ScreenNames from "../utils/ScreenNames";
import { getAccessToken } from "../services/authService";
import { useEffect, useState } from "react";
import { getById, update } from "../services/userService";
import { ValidationEmail } from "../utils/Validation";

export default function AccountScreen({ navigation }: any) {
    const [id, setId] = useState<number>(1);
    const [lastName, setLastName] = useState<string>("");
    const [firstName, setFirstName] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [phoneNumber, setPhoneNumber] = useState<string>("");
    const [isValidLastName, setIsValidLastName] = useState<boolean>(false);
    const [isValidFirstName, setIsValidFirstName] = useState<boolean>(false);
    const [isValidPhoneNumber, setIsValidPhoneNumber] = useState<boolean>(false);

    useEffect(() => {
        const getData = async () => {
            const jwt = require("jwt-decode");
            const token = await getAccessToken();
            const decode = jwt(token);

            const userId = decode.sub;
            console.log("User ID:", userId);

            const result = await getById(userId);
            setId(userId);
            setFirstName(result?.data["firstName"]);
            setLastName(result?.data["lastName"]);
            setEmail(result?.data["email"]);
            setPhoneNumber(result?.data["phoneNumber"]);
        };

        getData();
    }, [])

  const handleUpdate = async () => {
    const checkEmail = ValidationEmail(email);

    let hasError = false;

        if (lastName === "") {
            setIsValidLastName(true);
            hasError = true;
        }
        if (firstName === "") {
            setIsValidFirstName(true);
            hasError = true;
        }
        if (phoneNumber === "") {
            setIsValidPhoneNumber(true);
            hasError = true;
        }

        if (hasError == false) {
            setIsValidLastName(true);
            setIsValidFirstName(true);
            setIsValidPhoneNumber(true);
            const result = await update(id, firstName, lastName, phoneNumber);
            navigation.goBack();
        }
        else {
            alert("Sửa thông tin không thành công");
        }
    }
    }

  return (
    <View style={styles.container}>
      <View>
        <View style={styles.listDetail}>
          <Text style={styles.title}>Họ</Text>
          <TextInput
            style={styles.input}
            placeholder="chưa có họ"
            onChangeText={(value) => {
              setLastName(value);
            }}
          >
            <Text>{lastName}</Text>
          </TextInput>
        </View>

        <View style={styles.listDetail}>
          <Text style={styles.title}>Tên</Text>
          <TextInput
            style={styles.input}
            placeholder="chưa có tên"
            onChangeText={(value) => {
              setFirstName(value);
            }}
          >
            <Text>{firstName}</Text>
          </TextInput>
        </View>

        <View style={styles.listDetail}>
          <Text style={styles.title}>Điện thoại</Text>
          <TextInput
            style={styles.input}
            placeholder="chưa có số điện thoại"
            onChangeText={(value) => {
              setPhoneNumber(value);
            }}
          >
            <Text>{phoneNumber}</Text>
          </TextInput>
        </View>
                <View style={styles.listDetail}>
                    <Text style={styles.title}>Email</Text>
                    <TextInput
                        style={styles.input}
                        placeholder="chưa có email"
                    >
                        <Text>{email}</Text>
                    </TextInput>
                </View>
            </View>

            <View style={styles.buttonArea}>
                <TouchableOpacity style={styles.returnButt} onPress={() => navigation.goBack()}>
                    <Text style={{ color: "#EE4D2D" }}>Trở lại</Text>
                </TouchableOpacity>
                </TouchableOpacity>

        <TouchableOpacity
          style={styles.confirmButt}
          onPress={() => {
            handleUpdate();
          }}
        >
          <Text style={{ color: "#fff" }}>Lưu thay đổi</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}
    container: {
        flex: 1,
        flexDirection: 'column',
        justifyContent: "space-between",
        alignItems: "center",
        backgroundColor: "#F1EFEF",
    },
    listDetail: {
        backgroundColor: "#fff",
        width: "100%",
        height: 60,
        paddingHorizontal: 20,
        flexDirection: "row",
        alignItems: "center"
    },
    title: {
        width: "20%",

    },
    input: {
        width: "80%",
        textAlign: "right",
    },
    buttonArea: {
        width: "100%",
    },
    returnButt: {
        width: "100%",
        height: 50,
        alignItems: "center",
        justifyContent: "center",
        backgroundColor: "#fff"
    },
    confirmButt: {
        width: "100%",
        height: 50,
        alignItems: "center",
        justifyContent: "center",
        backgroundColor: "#EE4D2D",
    }
    }
});
