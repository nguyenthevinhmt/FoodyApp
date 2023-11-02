import {
  StyleSheet,
  View,
  SafeAreaView,
  Image,
  Text,
  TextInput,
  TouchableOpacity,
  ToastAndroid,
} from "react-native";
import { useState } from "react";
import {
  ValidationEmail,
  ValidationPassword,
  ValidationRePassword,
} from "../utils/Validation";
import { register } from "../services/authService";
import ScreenNames from "../utils/ScreenNames";

export const RegisterScreen = ({ navigation }: any) => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [rePassword, setRePassword] = useState<string>("");
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");
  const [rePasswordError, setRePasswordError] = useState<string>("");
  const [isValidEmail, setIsValidEmail] = useState<boolean>(false);
  const [isValidPassword, setIsValidPassword] = useState<boolean>(false);
  const [isValidRePassword, setIsValidRePassword] = useState<boolean>(false);

  const handleRegister = async () => {
    const checkEmail = ValidationEmail(email);
    const checkPassword = ValidationPassword(password);
    const checkRePassword = ValidationPassword(rePassword);
    const checkRePasswordNotMatch = ValidationRePassword(password, rePassword);

    let hasError = false; // Biến này để kiểm tra xem có lỗi không

    if (checkEmail !== null) {
      setEmailError(checkEmail);
      setIsValidEmail(true);
      hasError = true;
    }
    if (checkPassword !== null) {
      setPasswordError(checkPassword);
      setIsValidPassword(true);
      hasError = true;
    }
    if (checkRePassword !== null) {
      setRePasswordError(checkRePassword);
      setIsValidRePassword(true);
      hasError = true;
    }
    if (checkRePasswordNotMatch !== null) {
      setRePasswordError(checkRePasswordNotMatch);
      setIsValidRePassword(true);
      hasError = true;
    }

    const result = await register(email, password);
    if (result != null) {
      hasError = true;
      console.log(result);
    }
    else {
      hasError = false;
      console.log(result);
    }

    if (hasError == true) {
      // Nếu không có lỗi, thực hiện đăng kí thành công
      setEmailError("");
      setIsValidEmail(false);
      setPasswordError("");
      setIsValidPassword(false);
      setRePasswordError("");
      setIsValidRePassword(false);
      ToastAndroid.show("Đăng ký thành công", ToastAndroid.SHORT);
      navigation.replace(ScreenNames.LOGIN)
    }
    else {
      ToastAndroid.show("Đăng ký không thành công", ToastAndroid.SHORT);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.container}>
        <View style={styles.logoApp}>
          <Image
            source={require("../assets/images/Logo_app.png")}
            style={styles.logoImage}
          />
        </View>

        <View style={styles.formLogin}>
          <View style={[styles.inputField, isValidEmail && styles.fieldError]}>
            <Image
              source={require("../assets/Icons/UsernameIcon.png")}
              width={16}
              height={16}
              style={{ marginRight: 10 }}
            />
            <TextInput
              style={styles.emailField}
              placeholder="Tài khoản email"
              placeholderTextColor="#aaa"
              value={email}
              onChangeText={(value) => {
                setEmail(value);
                if (email !== "") {
                  setEmailError("");
                  setIsValidEmail(false);
                }
              }}
            ></TextInput>
          </View>

          {isValidEmail ? (
            <Text style={styles.textError}>{emailError}</Text>
          ) : null}

          <View
            style={[styles.inputField, isValidPassword && styles.fieldError]}
          >
            <Image
              source={require("../assets/Icons/PasswordIcon.png")}
              width={16}
              height={16}
              style={{ marginRight: 10 }}
            />

            <TextInput
              style={styles.emailField}
              placeholder="Password"
              placeholderTextColor="#aaa"
              secureTextEntry={true}
              value={password}
              onChangeText={(value) => {
                setPassword(value);
                if (password !== "") {
                  setPasswordError("");
                  setIsValidPassword(false);
                }
              }}
            ></TextInput>
          </View>

          {isValidPassword ? (
            <Text style={styles.textError}>{passwordError}</Text>
          ) : null}

          <View
            style={[styles.inputField, isValidPassword && styles.fieldError]}
          >
            <Image
              source={require("../assets/Icons/Frame.png")}
              width={16}
              height={16}
              style={{ marginRight: 10 }}
            />

            <TextInput
              style={styles.emailField}
              placeholder="Nhập lại mật khẩu"
              placeholderTextColor="#aaa"
              secureTextEntry={true}
              value={rePassword}
              onChangeText={(value) => {
                setRePassword(value);
                if (rePassword !== "") {
                  setRePasswordError("");
                  setIsValidRePassword(false);
                }
              }}
            ></TextInput>
          </View>

          {isValidRePassword ? (
            <Text style={styles.textError}>{rePasswordError}</Text>
          ) : null}

          <TouchableOpacity
            style={[styles.button, styles.loginButton]}
            activeOpacity={0.7}
            onPress={() => {
              handleRegister();
            }}
          >
            <Text
              style={{
                textAlign: "center",
                fontSize: 14,
                color: "#fff",
                fontWeight: "600",
              }}
            >
              Đăng kí
            </Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.button, styles.registerButton]}
            activeOpacity={0.7}
            onPress={() => {
              navigation.navigate(ScreenNames.LOGIN);
            }}
          >
            <Text
              style={{
                textAlign: "center",
                fontSize: 14,
                color: "#111",
                fontWeight: "600",
              }}
            >
              Đăng nhập
            </Text>
          </TouchableOpacity>
        </View>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 8,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#fff",
    width: "100%",
  },

  logoApp: {
    width: "100%",
    justifyContent: "center",
    alignItems: "center",
    margin: "auto",
    flex: 1,
  },

  logoImage: {
    width: 150,
    height: 127,
  },

  formLogin: {
    flex: 3,
  },

  inputField: {
    borderWidth: 1,
    borderColor: "#ccc",
    backgroundColor: "#fafafa",
    flexDirection: "row",
    alignItems: "center",
    paddingHorizontal: 15,
    paddingVertical: 5,
    borderRadius: 9,
    marginTop: 15,
  },

  emailField: {
    backgroundColor: "#fafafa",
    padding: 8,
    borderRadius: 9,
    width: "90%",
    alignItems: "center",
    justifyContent: "center",
  },

  password: {
    borderWidth: 1,
    borderColor: "#ccc",
    backgroundColor: "#fafafa",
    flexDirection: "row",
    alignItems: "center",
    paddingHorizontal: 15,
    paddingVertical: 5,
    borderRadius: 9,
    marginTop: 30,
  },

  textError: {
    color: "red",
    fontSize: 12,
    textAlign: "left",
    marginLeft: 6,
  },

  fieldError: {
    borderColor: "red",
  },

  button: {
    justifyContent: "center",
    marginTop: 20,
    paddingVertical: 15,
    borderRadius: 10,
  },

  loginButton: {
    backgroundColor: "#EE4D2D",
  },

  registerButton: {
    backgroundColor: "#FFF",
    borderWidth: 1,
    borderColor: "#EE4D2D",
  },
});
