import { Text, View, StyleSheet, TouchableOpacity, ScrollView, Image, Alert } from "react-native";
import React, { useEffect } from 'react';
import { useState } from "react";
import Modal from 'react-native-modal';
import AddressComponent from "../components/AddressComponent";
import { getAccessToken } from "../services/authService";
import { getAllAddress, getUserById } from "../services/userService";
import { createOrder } from "../services/orderService";
import { createPayment } from "../services/vnpayService";
import ScreenNames from "../utils/ScreenNames";

function showAlert(navigation: () => void) {
    return (
        Alert.alert(
            'Thông báo',
            'Vui lòng thêm địa chỉ nhận hàng trước khi đặt hàng.',
            [{ text: 'Ok', onPress: () => { navigation } }],
            { cancelable: false }
        )
    )
};

const CreateOrderScreen = ({ navigation, route }: any) => {
    const productId = route.params['id'];

    //thông tin địa chỉ
    const [addressList, setAddressList] = useState([]);
    const [name, setName] = useState('');
    const [phone, setPhone] = useState('');

    //vị trí địa chỉ trong addressList
    const [addressIndex, setAddressIndex] = useState(0);

    //phương thức thanh toán
    const [paymentMethod, setPaymentMethod] = useState(1);

    //Chỉnh hiệu ứng nút thanh toán khi ấn
    const [button1Pressed, setButton1Pressed] = useState(false);
    const [button2Pressed, setButton2Pressed] = useState(false);

    //vòng tròn loading
    const [loadingOn, setLoadingOn] = useState(false);

    useEffect(() => {
        const getData = async () => {
            //gọi api lấy địa chỉ và thông tin người dùng
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
    }, []);

    const handleOrder = async () => {
        if (Array.isArray(addressList) && addressList.length === 0) {
            showAlert(navigation.goBack());
        }
        else {
            setLoadingOn(true);

            const result = await createOrder(productId, paymentMethod, route.params['quantity'], addressList[addressIndex]['addressType']);
            console.log(result);
            if (paymentMethod == 1) {
                navigation.navigate(ScreenNames.MAIN, { screen: 'Order' });
            }
            else if (paymentMethod == 2) {
                //gọi api thanh toán điện tử
                const payment = await createPayment(result?.data);
                console.log(payment?.data);

                setTimeout(() => {
                    navigation.navigate(ScreenNames.VNPAY, { 
                        url: payment?.data,
                        orderId: result?.data, //id đơn hàng
                    });
                }, 0); // Đợi 3 giây trước khi thực hiện navigation
            }
        }
    }

    const [isModalVisible, setModalVisible] = useState(false);

    const toggleModal = () => {
        setModalVisible(!isModalVisible);
    };

    return (
        <View style={styles.container}>
            <View style={styles.scroll}>
                <ScrollView>
                    <View style={styles.address}>
                        <Text style={{
                            marginTop: 10,
                            marginLeft: 5,
                            fontSize: 16,
                            fontWeight: '600'
                        }}>
                            Địa chỉ nhận hàng
                        </Text>
                        {addressList.length > 0 && (
                            <AddressComponent
                                addressType={addressList[addressIndex]['addressType']}
                                province={addressList[addressIndex]['province']}
                                district={addressList[addressIndex]['district']}
                                ward={addressList[addressIndex]['ward']}
                                street={addressList[addressIndex]['streetAddress']}
                                detail={addressList[addressIndex]['detailAddress']}
                                name={name}
                                phoneNumber={phone}
                                onNavigate={() => { toggleModal() }}
                            />
                        )}
                    </View>

                    <Modal
                        isVisible={isModalVisible}
                        style={styles.bottomSheet}
                        onBackdropPress={toggleModal} // Đóng modal khi chạm vào ngoài vùng hiển thị
                        onSwipeComplete={toggleModal} // Đóng modal khi vuốt xuống
                        swipeDirection="down" // Cho phép vuốt xuống để đóng modal
                    >
                        <View style={styles.bottomSheetContainer}>
                            <View style={styles.headerBottomSheet}>
                                <Text style={{ fontSize: 18, fontWeight: '600' }}>Lựa chọn địa chỉ nhận hàng</Text>

                                <TouchableOpacity style={{ justifyContent: 'flex-start' }} onPress={() => { toggleModal() }}>
                                    <Text style={{
                                        fontSize: 20
                                    }}>X</Text>
                                </TouchableOpacity>
                            </View>

                            {/* {danh sách các địa chỉ để lựa chọn} */}
                            <View style={styles.bottomSheetContent}>
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
                                            onNavigate={() => { toggleModal(); setAddressIndex(index) }}
                                        />
                                    ))
                                }
                            </View>
                        </View>
                    </Modal>

                    <View style={styles.listProduct}>
                        <Text style={{
                            marginTop: 10,
                            marginLeft: 5,
                            fontSize: 16,
                            fontWeight: '600'
                        }}>
                            Thông tin sản phẩm
                        </Text>

                        <View style={styles.productCart}>
                            <View style={{ width: '30%' }}>
                                <Image source={{ uri: route.params['imgUrl'] }} style={styles.bottomSheetImage} />
                            </View>

                            <View style={styles.productDetail}>
                                <Text style={styles.productCartName}>{route.params['productName']}</Text>
                                <Text style={styles.productCartActualPrice}>đ{route.params['price'].toLocaleString()}</Text>
                                <Text style={styles.productCartPrice}>đ{route.params['actualPrice'].toLocaleString()}</Text>
                                <Text>X{route.params['quantity']}</Text>
                            </View>
                        </View>
                    </View>

                    <View style={{ marginTop: 5, backgroundColor: '#fff' }}>
                        <Text style={{
                            marginTop: 10,
                            marginLeft: 5,
                            fontSize: 16,
                            fontWeight: '600'
                        }}>
                            Phương thức thanh toán
                        </Text>

                        <View style={styles.paymentMethods}>
                            <TouchableOpacity
                                onPress={() => {
                                    setButton1Pressed(true);
                                    setButton2Pressed(false);
                                    setPaymentMethod(1)
                                }}

                                style={[
                                    styles.paymentButton,
                                    button1Pressed && { borderColor: '#EE4D2D', opacity: 1 }
                                ]}>
                                <Text
                                    style={[{ fontWeight: '500' },
                                    button1Pressed && { color: '#EE4D2D' }
                                    ]}
                                >Cash</Text>
                            </TouchableOpacity>

                            <TouchableOpacity
                                onPress={() => {
                                    setButton1Pressed(false);
                                    setButton2Pressed(true);
                                    setPaymentMethod(2)
                                }}
                                style={[
                                    styles.paymentButton,
                                    button2Pressed && { borderColor: '#EE4D2D', opacity: 1 }
                                ]}
                            >
                                <Text
                                    style={[{ fontWeight: '500' },
                                    button2Pressed && { color: '#EE4D2D' }
                                    ]}
                                >VnPay</Text>
                            </TouchableOpacity>
                        </View>
                    </View>

                    <View style={styles.paymentDetails}>
                        <Text style={{
                            marginTop: 10,
                            fontSize: 16,
                            fontWeight: '600'
                        }}>
                            Chi tiết thanh toán
                        </Text>

                        <View style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                            alignItems: 'center',
                            marginVertical: 3
                        }}>
                            <Text>Tổng tiền hàng</Text>
                            <Text>{route.params['actualPrice'].toLocaleString()}đ</Text>
                        </View>

                        <View style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                            alignItems: 'center',
                            marginVertical: 3
                        }}>
                            <Text style={{
                                fontSize: 18,
                                fontWeight: '400',
                            }}>Tổng thanh toán</Text>

                            <Text style={{
                                color: '#EE4D2D',
                                fontSize: 18
                            }}>{route.params['actualPrice'].toLocaleString()}đ</Text>
                        </View>
                    </View>
                </ScrollView>
            </View>

            <View style={styles.footer}>
                <View style={{
                    width: '65%',
                    justifyContent: 'center',
                    alignItems: 'flex-end',
                    paddingRight: 10,
                }}>
                    <Text style={{ color: '#EE4D2D', fontWeight: '600', fontSize: 20 }}>đ{route.params['actualPrice'].toLocaleString()}</Text>
                </View>

                <TouchableOpacity
                    style={styles.orderButton}
                    onPress={() => {
                        handleOrder();
                        //navigation.goBack();
                    }}>
                    <Text style={{ color: '#fff' }}>Đặt hàng</Text>
                </TouchableOpacity>
            </View>

            <Modal
                isVisible={loadingOn}
                animationIn="fadeIn"
                animationOut="fadeOut"
                backdropOpacity={0.5}
                style={{ justifyContent: 'center', alignItems: 'center' }}
            >
                <View style={{ justifyContent: 'center', alignItems: 'center' }}>
                    <Image source={require('../assets/Icons/loading.gif')} style={{ width: 50, height: 50 }} />
                </View>
            </Modal>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: 'column',
        backgroundColor: "#F1EFEF"
    },

    scroll: {
        width: '100%',
        height: '93%',
    },

    address: {
        width: '100%',
        height: 180,
        backgroundColor: '#fff'
    },

    bottomSheet: {
        justifyContent: 'flex-end',
        margin: 0,
    },

    bottomSheetContainer: {
        backgroundColor: 'white',
        height: '70%',
        flexDirection: 'column',
        justifyContent: 'flex-start',
        borderTopLeftRadius: 10,
        borderTopRightRadius: 10
    },

    headerBottomSheet: {
        height: 30,
        width: '100%',
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginVertical: 15,
        paddingHorizontal: 20,
    },

    bottomSheetContent: {
        width: '100%',
        height: '100%',
        backgroundColor: '#F1EFEF'
    },

    listProduct: {
        width: '100%',
        marginTop: 5,
        backgroundColor: '#fff'
    },

    productCart: {
        width: '100%',
        flexDirection: 'row',
        paddingVertical: 10,
        paddingHorizontal: 20,
        marginVertical: 10,
        backgroundColor: '#fff'
    },

    bottomSheetImage: {
        width: 100,
        height: 100,
    },

    productDetail: {
        width: '70%',
        flexDirection: 'column',
        alignItems: 'flex-end',
    },

    productCartName: {
        paddingBottom: 3,
        fontSize: 16,
    },

    productCartActualPrice: {
        paddingBottom: 3,
        color: '#B4B4B3',
        textDecorationLine: 'line-through'
    },

    productCartPrice: {
        paddingBottom: 3,
        color: '#EE4D2D',
        fontWeight: '600'
    },


    paymentMethods: {
        width: '100%',
        flexDirection: 'row',
        padding: 5,
    },

    paymentButton: {
        width: '50%',
        height: 50,
        justifyContent: 'center',
        alignItems: 'center',
        borderWidth: 1,
        borderColor: '#B4B4B3',
        opacity: 0.5
    },

    paymentDetails: {
        height: 300,
        paddingHorizontal: 5,
        marginTop: 5,
        backgroundColor: '#fff'
    },

    footer: {
        width: '100%',
        height: '7%',
        flexDirection: 'row',
        marginTop: 5,
        backgroundColor: '#fff'
    },

    orderButton: {
        width: '35%',
        alignItems: 'center',
        backgroundColor: '#EE4D2D',
        justifyContent: 'center',
        margin: 0,
    }
});

export default CreateOrderScreen;