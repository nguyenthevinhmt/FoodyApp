import React, { useState } from "react";
import {
  StyleSheet,
  SafeAreaView,
  View,
  Image,
  Text,
  TextInput,
  TouchableOpacity,
} from "react-native";
import { ValidationEmail, ValidationPassword } from "../utils/Validation";
import {
  getAccessToken,
  getRefreshToken,
  login,
} from "../services/authService";
import Alert from "../components/CustomAlert";
import ScreenNames from "../utils/ScreenNames";

export const LoginScreen = ({ navigation }: any) => {
  const [email, setEmail] = useState<string>("Customer@gmail.com");
  const [password, setPassword] = useState<string>("Customer@12345");
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");
  const [isValidEmail, setIsValidEmail] = useState<boolean>(false);
  const [isValidPassword, setIsValidPassword] = useState<boolean>(false);
  const [showAlert, setShowAlert] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");

  const handleLogin = async () => {
    const checkEmail = ValidationEmail(email);
    const checkPassword = ValidationPassword(password);

    if (checkEmail !== null) {
      setEmailError(checkEmail);
      setIsValidEmail(true);
    }
    if (checkPassword !== null) {
      setPasswordError(checkPassword);
      setIsValidPassword(true);
    } else {
      setEmailError("");
      setIsValidEmail(false);
      setPasswordError("");
      setIsValidPassword(false);
    }

    const result = await login(email, password);
    //console.log(result);

    if (result) {
      const accessToken = await getAccessToken();
      const refreshToken = await getRefreshToken();
      console.log("accessToken", accessToken);
      console.log("refreshToken", refreshToken);
      navigation.replace(ScreenNames.MAIN);
    } else {
      setAlertMessage("Email hoặc mật khẩu không đúng, mời đăng nhập lại");
      setShowAlert(true);
      console.log("Đăng nhập lỗi");
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

          <View style={[styles.inputField, isValidPassword && styles.fieldError]}>
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
          <Text
            style={{
              textAlign: "right",
              fontSize: 12,
              color: "#007BFF",
              marginTop: 5,
            }}
          >
            Quên mật khẩu?
          </Text>

          <TouchableOpacity
            style={[styles.button, styles.loginButton]}
            activeOpacity={0.7}
            onPress={() => {
              handleLogin();
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
              Đăng nhập
            </Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.button, styles.registerButton]}
            activeOpacity={0.7}
            onPress={() => {
              navigation.navigate(ScreenNames.REGISTER);
              setEmailError("");
              setIsValidEmail(false);
              setPasswordError("");
              setIsValidPassword(false);
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
              Đăng kí
            </Text>
          </TouchableOpacity>
        </View>
      </View>

      <Alert
        visible={showAlert}
        message={alertMessage}
        onClose={() => setShowAlert(false)}
      />
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
    marginTop: 20,
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
