import { Text, View, StyleSheet, TextInput, TouchableOpacity } from "react-native";
import { getAccessToken } from "../services/authService";
import { useEffect, useState } from "react";
import { updateAddress } from "../services/userService";

export default function UpdateAddressScreen({ navigation, route }: any) {
    const addressType = route.params['addressType'];
    const Id = route.params['id'];

    const [userId, setUserId] = useState(0);
    const [Province, setProvince] = useState('');
    const [District, setDistrict] = useState('');
    const [Ward, setWard] = useState('');
    const [StreetAddress, setStreetAddress] = useState('');
    const [DetailAddress, setDetailAddress] = useState('');
    const [Notes, setNotes] = useState('');

    useEffect(() => {
        const getData = async () => {
            const jwt = require("jwt-decode");
            const token = await getAccessToken();
            const decode = jwt(token);

            const userId = decode.sub;
            setUserId(userId);
            setProvince(route.params['province']);
            setDistrict(route.params['district']);
            setWard(route.params['ward']);
            setStreetAddress(route.params['street']);
            setDetailAddress(route.params['detail']);
            setNotes(route.params['notes']);
        };

        getData();
    }, [])

    const handleUpdate = async () => {
        const update = await updateAddress(Id, userId, Province, District, Ward, StreetAddress, DetailAddress, Notes, addressType);
        navigation.goBack();
    }

    return (
        <View style={styles.container}>
            <View>
                <View style={styles.listDetail}>
                    <Text style={styles.title}>Tỉnh/ Thành phố</Text>

                    <TextInput
                        style={styles.input}
                        placeholder="chưa có"
                        onChangeText={(value) => {
                            setProvince(value);
                        }}>
                        <Text>{Province}</Text>
                    </TextInput>
                </View>

                <View style={styles.listDetail}>
                    <Text style={styles.title}>Quận/ Huyện</Text>

                    <TextInput
                        style={styles.input}
                        placeholder="chưa có"
                        onChangeText={(value) => {
                            setDistrict(value);
                        }}>
                        <Text>{District}</Text>
                    </TextInput>
                </View>

                <View style={styles.listDetail}>
                    <Text style={styles.title}>Phường/ Xã</Text>

                    <TextInput
                        style={styles.input}
                        placeholder="chưa có"
                        onChangeText={(value) => {
                            setWard(value);
                        }}>
                        <Text>{Ward}</Text>
                    </TextInput>
                </View>

                <View style={styles.listDetail}>
                    <Text style={styles.title}>Đường</Text>

                    <TextInput
                        style={styles.input}
                        placeholder="chưa có"
                        onChangeText={(value) => {
                            setStreetAddress(value);
                        }}>
                        <Text>{StreetAddress}</Text>
                    </TextInput>
                </View>

                <View style={styles.listDetail}>
                    <Text style={styles.title}>Chi tiết</Text>

                    <TextInput
                        style={styles.input}
                        placeholder="chưa có"
                        onChangeText={(value) => {
                            setDetailAddress(value);
                        }}>
                        <Text>{DetailAddress}</Text>
                    </TextInput>
                </View>

                <View style={styles.listDetail}>
                    <Text style={styles.title}>Mô tả thêm</Text>

                    <TextInput
                        style={styles.input}
                        placeholder="chưa có"
                        onChangeText={(value) => {
                            setNotes(value);
                        }}>
                        <Text>{Notes}</Text>
                    </TextInput>
                </View>
            </View>

            <View style={styles.buttonArea}>
                <TouchableOpacity style={styles.returnButt} onPress={() => navigation.goBack()}>
                    <Text style={{ color: "#EE4D2D" }}>Trở lại</Text>
                </TouchableOpacity>

                <TouchableOpacity
                    style={styles.confirmButt}
                    onPress={() => {
                        handleUpdate();
                    }}>
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
        backgroundColor: "#F1EFEF",
    },

    listDetail: {
        backgroundColor: "#fff",
        width: "100%",
        height: 70,
        paddingHorizontal: 20,
        flexDirection: "row",
        alignItems: "center",
        marginBottom: 3
    },

    title: {
        width: "30%",

    },

    input: {
        width: "70%",
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
});
