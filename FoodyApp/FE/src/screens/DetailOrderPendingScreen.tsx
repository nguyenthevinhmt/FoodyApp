import { Text, View, StyleSheet, TouchableOpacity, ScrollView, Image, Button, Alert } from "react-native";
import Modal from 'react-native-modal';
import { SafeAreaView } from "react-native-safe-area-context";
import React, { useCallback } from 'react';
import { useState } from "react";
import AddressComponent from "../components/AddressComponent";
import { useFocusEffect } from "@react-navigation/native";
import { getAccessToken } from "../services/authService";
import { getUserById } from "../services/userService";
import { baseURL_img } from "../utils/baseUrl";
import { getOrderById, updateOrderStatus } from "../services/orderService";
import ScreenNames from "../utils/ScreenNames";
import { createPayment } from "../services/vnpayService";

const DetailOrderPendingScreen: React.FC = ({ navigation, route }: any) => {
    const orderId = route.params['orderId'];

    //thông tin đơn hàng
    const [order, setOrder] = useState([]);

    //danh sách sản phẩm
    const [products, setProducts] = useState([]);

    //thông tin địa chỉ
    const [address, setAddress] = useState([]);
    const [name, setName] = useState('');
    const [phone, setPhone] = useState('');

    //phương thức thanh toán
    const [paymentMethod, setPaymentMethod] = useState(1);

    //đã thanh toán
    const [paid, setPaid] = useState(false);

    //vòng tròn loading
    const [loadingOn, setLoadingOn] = useState(false);

    //Thanh toán trực tuyến
    //const [vnPay, setVnPay] = useState(false);

    useFocusEffect(
        useCallback(() => {
            const getData = async () => {
                const jwt = require("jwt-decode");
                const token = await getAccessToken();
                const decode = jwt(token);

                const userId = decode.sub;

                //gọi api lấy địa chỉ và thông tin người dùng
                const responseUser = await getUserById(userId);
                setName(responseUser?.data['firstName'] + ' ' + responseUser?.data['lastName']);
                setPhone(responseUser?.data['phoneNumber']);

                //gọi api lấy thông tin đơn hàng
                const responseOrder = await getOrderById(orderId);
                setOrder(responseOrder?.data)
                setProducts(responseOrder?.data['products']);
                setAddress(responseOrder?.data['userAddress']);
                setPaymentMethod(responseOrder?.data['paymentMethod']);
                setPaid(responseOrder?.data['isPaid']);
            };

            getData();
        }, [])
    );

    const PaymentDetail = () => {
        if (paymentMethod == 1 && paid == false) {
            return <Text>Đơn hàng chưa được thanh toán</Text>
        }
        else if(paymentMethod == 1 && paid == true) {
            return <Text>Đơn hàng đã được thanh toán trực tiếp</Text>
        }
        else if (paymentMethod == 2 && paid == false) {
            //setVnPay(true);
            return <Text>Đơn hàng chưa được thanh toán trực tuyến</Text>
        } else {
            return <Text>Đơn hàng đã được thanh toán trực tuyến</Text>
        }
    }

    const handlePayment = async () => {
        setLoadingOn(true);
        //gọi api thanh toán điện tử
        const payment = await createPayment(order.id);

        setTimeout(() => {
            navigation.navigate(ScreenNames.VNPAY, { 
                url: payment?.data,
                orderId: order.id, //id đơn hàng
            });
        }, 0); // Đợi 3 giây trước khi thực hiện navigation
    }

    const handleCancel = async () => {
        const result = await updateOrderStatus(orderId, 5); //đơn hàng đã hủy
        return result?.data;
    }

    const showAlert = () => {
        Alert.alert(
            'Thông báo',
            'Bạn chắc chắn muốn hủy đơn hàng',
            [
                { text: 'Trở lại', style: 'cancel' },
                {
                    text: 'Xóa', onPress: async () => {
                        setLoadingOn(true);
                        const result = await handleCancel();
                        setTimeout(() => {
                            if (result === 'Success') {  
                                navigation.navigate(ScreenNames.MAIN, {screen: 'Order'});
                            } else {
                                console.log('lỗi khi xóa');
                            }
                        }, 2000)
                        
                    }
                }
            ],
            { cancelable: false }
        );
    };

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.header}>
                <Text style={styles.headerDetail}>Chi tiết đơn hàng</Text>
            </View>

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

                        <AddressComponent
                            addressType={(address as any)?.addressType}
                            province={(address as any)?.province}
                            district={(address as any)?.district}
                            ward={(address as any)?.ward}
                            street={(address as any)?.streetAddress}
                            detail={(address as any)?.detailAddress}
                            name={name}
                            phoneNumber={phone}
                            onNavigate={() => { }}
                        />
                    </View>

                    <View style={styles.listProduct}>
                        <Text style={{
                            marginTop: 10,
                            marginLeft: 5,
                            fontSize: 16,
                            fontWeight: '600'
                        }}>
                            Thông tin sản phẩm
                        </Text>

                        {products.map((value: any) => (
                            <View style={styles.productCart} key={value.id}>
                                <View style={{ width: '30%' }}>
                                    <Image source={{ uri: `${baseURL_img}${value['productImageUrl']}` }} style={styles.bottomSheetImage} />
                                </View>

                                <View style={styles.productDetail}>
                                    <Text style={styles.productCartName}>{value['name']}</Text>
                                    <Text style={styles.productCartActualPrice}>{value['price'].toLocaleString()}đ</Text>
                                    <Text style={styles.productCartPrice}>{value['actualPrice'].toLocaleString()}đ</Text>
                                    <Text>X{value['quantity']}</Text>
                                </View>
                            </View>
                        ))}
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
                            {
                                PaymentDetail()
                            }
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
                            <Text>{(order.totalAmount | 0).toLocaleString()}đ</Text>
                        </View>

                        <View style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                            alignItems: 'center',
                            marginVertical: 3
                        }}>
                            <Text>Tổng tiền phí vận chuyển</Text>
                            <Text>0đ</Text>
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
                            }}>{(order.totalAmount | 0).toLocaleString()}đ</Text>
                        </View>
                    </View>
                </ScrollView>
            </View>

            <View style={styles.footer}>
                <TouchableOpacity
                    style={[styles.orderButton, (paymentMethod == 2 && paid == false) && styles.orderButtonNew]}
                    onPress={() => {
                        navigation.goBack();
                    }}>
                    <Text style={[{ color: '#fff' }, (paymentMethod == 2 && paid == false) && { color: '#aaa' }]}>Trở lại</Text>
                </TouchableOpacity>

                {paymentMethod == 2 && paid == false ?  
                    (<TouchableOpacity style={styles.VNPAYpayment} onPress={() => {handlePayment()}}>
                        <Text style={{color: '#fff'}}>Thanh toán</Text>
                    </TouchableOpacity>)
                : ('')
                }
                
                <TouchableOpacity
                    style={[styles.cancelButton, (paymentMethod == 2 && paid == false) && styles.cancelButtonNew]}
                    onPress={() => {
                        showAlert();
                    }}>
                    <Text style={{ color: '#EE4D2D' }}>Hủy đặt hàng</Text>
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
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: 'column',
        backgroundColor: "#F1EFEF"
    },

    header: {
        width: '100%',
        height: '7%',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#fff',
        borderBottomWidth: 1
    },

    headerDetail: {
        fontSize: 17,
        fontWeight: '500',
        color: '#EE4D2D'
    },

    scroll: {
        width: '100%',
        height: '86%',
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
        backgroundColor: '#fff',
        borderBottomWidth: 0.7,
        borderColor: '#B4B4B3'
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
        width: '50%',
        alignItems: 'center',
        backgroundColor: '#EE4D2D',
        justifyContent: 'center',
        margin: 0,
    },

    cancelButton: {
        width: '50%',
        alignItems: 'center',
        backgroundColor: '#fff',
        justifyContent: 'center',
        margin: 0,
    },

    orderButtonNew: {
        width: '33%',
        alignItems: 'center',
        backgroundColor: '#fff',
        justifyContent: 'center',
        margin: 0,
    },

    cancelButtonNew: {
        width: '33%',
        alignItems: 'center',
        backgroundColor: '#fff',
        justifyContent: 'center',
        margin: 0,
    },

    VNPAYpayment: {
        width: '33%',
        alignItems: 'center',
        backgroundColor: '#EE4D2D',
        justifyContent: 'center',
        margin: 0,
    }
});

export default DetailOrderPendingScreen;