import { Text, View, StyleSheet, ScrollView } from "react-native";
import ScreenNames from "../utils/ScreenNames";
import { getAccessToken } from "../services/authService";
import { useCallback, useEffect, useState } from "react";
import AddressComponent from "../components/AddressComponent";
import AddAddressComponent from "../components/AddAddressComponent";
import { useFocusEffect } from "@react-navigation/native";
import { getAllAddress, getUserById } from "../services/userService";


export default function AddressScreen({ navigation }: any) {
    //kiểm tra nếu đã có địa chỉ sẽ ko cho thêm nữa
    const [checkHome, setCheckHome] = useState(true);
    const [checkCompany, setCheckCompany] = useState(true);
    const [addressList, setAddressList] = useState([]);
    const [name, setName] = useState('');
    const [phone, setPhone] = useState('');

    useFocusEffect(
        useCallback(() => {
            const getData = async () => {
                const jwt = require("jwt-decode");
                const token = await getAccessToken();
                const decode = jwt(token);

                const userId = decode.sub;
                console.log("User ID:", userId);

                const responseAddress = await getAllAddress(userId);
                setAddressList(responseAddress?.data.item);

                const responseUser = await getUserById(userId);
                setName(responseUser?.data['firstName'] + ' ' + responseUser?.data['lastName']);
                setPhone(responseUser?.data['phoneNumber']);
            };

            getData();
        }, [])
    );

    useEffect(() => {
        if (addressList.some(value => value['addressType'] == 1)) {
            setCheckHome(false);
        }
        else {
            setCheckHome(true);
        }
        if (addressList.some(value => value['addressType'] == 2)) {
            setCheckCompany(false);
        }
        else {
            setCheckCompany(true);
        }
        console.log('render');
    })


    return (
        <View style={styles.container}>
            <View style={styles.title}>
                <Text style={{ color: '#B4B4B3' }}>Địa chỉ đã lưu</Text>
            </View>

            <View style={styles.addArea}>
                {checkHome ?
                    <AddAddressComponent
                        addressType={1}
                        onNavigate={() => { navigation.navigate(ScreenNames.CREATEADDRESS, { addressType: 1 }) }}
                    />
                    : ''}

                {checkCompany ?
                    <AddAddressComponent
                        addressType={2}
                        onNavigate={() => { navigation.navigate(ScreenNames.CREATEADDRESS, { addressType: 2 }) }}
                    />
                    : ''}

                <AddAddressComponent
                    addressType={3}
                    onNavigate={() => { navigation.navigate(ScreenNames.CREATEADDRESS, { addressType: 3 }) }}
                />
            </View>

            <ScrollView style={styles.detailArea}>
                {
                    addressList.map((value, index) => (
                        <AddressComponent
                            key={index}
                            addressType={value['addressType']}
                            province={value['province']}
                            district={value['district']}
                            ward={value['ward']}
                            street={value['streetAddress']}
                            detail={value['detailAddress']}
                            name={name}
                            phoneNumber={phone}
                            onNavigate={() => {
                                navigation.navigate(ScreenNames.UPDATEADDRESS, {
                                    id: value['id'],
                                    addressType: value['addressType'],
                                    province: value['province'],
                                    district: value['district'],
                                    ward: value['ward'],
                                    street: value['streetAddress'],
                                    detail: value['detailAddress'],
                                    notes: value['notes']
                                })
                            }}
                        />
                    ))
                }
            </ScrollView>
        </View>
    );
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: 'column',
        justifyContent: "flex-start",
        alignItems: "center",
        backgroundColor: "#F1EFEF",
    },

    addArea: {
        width: '100%',
        marginBottom: 20
    },

    title: {
        width: '100%',
        height: 60,
        justifyContent: 'center',
        alignItems: 'flex-start',
        paddingLeft: 10,
    },

    detailArea: {
    },
});
