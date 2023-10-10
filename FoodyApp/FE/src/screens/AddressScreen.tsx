import { Text, View, StyleSheet, TextInput, TouchableOpacity } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import ScreenNames from "../utils/ScreenNames";
import { getAccessToken } from "../services/authService";
import { useEffect, useState } from "react";
import { getById, update } from "../services/userService";
import { ValidationEmail } from "../utils/Validation";

export default function AddressScreen({ navigation }: any) {

    return (
        <View style={styles.container}>
            <View>
                
            </View>

            <View style={styles.buttonArea}>
                <TouchableOpacity style={styles.returnButt} onPress={() => navigation.goBack()}>
                    <Text style={{ color: "#EE4D2D" }}>Trở lại</Text>
                </TouchableOpacity>

                <TouchableOpacity
                    style={styles.confirmButt}
                    >
                    <Text style={{ color: "#fff" }}>Lưu thay đổi</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: 'column',
        justifyContent: "space-between",
        alignItems: "center",
        backgroundColor: "#F2E1E1",
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
});
