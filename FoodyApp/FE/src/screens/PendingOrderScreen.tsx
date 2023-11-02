import { Text, StyleSheet, View, ScrollView } from "react-native";
import ProductComponent from "../components/ProductComponent";
import EmptyOrderComponent from "../components/EmptyOrderComponent";
import ScreenNames from "../utils/ScreenNames";
import { useState, useEffect, useCallback } from "react";
import OrderProductsComponent from "../components/OrderProductsComponent";
import { getAllOrderPending } from "../services/orderService";
import { getProductDiscount } from "../services/productService";
import { useFocusEffect, useIsFocused } from "@react-navigation/native";
import { baseURL_img } from "../utils/baseUrl";

function emtyOrder() {
    return (
        <EmptyOrderComponent
            imageUrl={require('../assets/Icons/add_shopping_cart.png')}
            title="Quên chưa đặt món rồi nè bạn ơi?"
            detail="Bạn sẽ nhìn thấy các món đang được chuẩn bị tại đây để kiểm tra đơn hàng nhanh hơn!"
        />
    );
}

const PendingOrderScreen = ({ navigation }: any) => {
    //kiểm tra nếu tồn tại order sẽ xóa màn emptyOrder
    const [shown, setShown] = useState(true);
    const [order, setOrder] = useState([]);
    const [product, setProduct] = useState([]);
    const isFocused = useIsFocused();
    useFocusEffect(
        useCallback(() => {
            const getData = async () => {
                const orderResponse = await getAllOrderPending();
                setOrder(orderResponse.data);
                if (order && order.length === 0) {
                    setShown(true);
                } else {
                    setShown(false);
                }
                console.log('đây là màn order');

                const productDiscountResponse = await getProductDiscount();
                setProduct(productDiscountResponse?.data.item);
            };
            getData();
             if (Array.isArray(order) && order.length === 0) {
            setShown(true);
        } else {
            setShown(false);
        }
        }, [order,isFocused])
    );

    // useEffect(() => {
    //     if (Array.isArray(order) && order.length === 0) {
    //         setShown(true);
    //     } else {
    //         setShown(false);
    //     }
    // }, [order]);

    return (
        <ScrollView style={styles.container}>
            {shown ? emtyOrder() : ''}

            {order.map((value) => (
                <OrderProductsComponent
                    key={value['id']}
                    products={value['products']}
                    totalPrice={value['totalAmount']}
                    onNavigation={() => { navigation.navigate(ScreenNames.DETAIL_ORDER_PENDING, { orderId: value['id'] }) }}
                />
            ))}
            
            <View style={styles.boundary}>
                <View style={styles.divider} />
                <Text style={{
                    color: '#B4B4B3',
                }}>Có thể bạn cũng thích</Text>
                <View style={styles.divider} />
            </View>

            <View style={styles.suggestion}>
                {
                    product.map((value) => (
                        <View style={{ width: '47%', marginHorizontal: 5 }} key={value['id']}>
                            <ProductComponent
                                imageUrl={`${baseURL_img}${value['productImageUrl']}`}
                                name={value['name']}
                                actualPrice={value['actualPrice']}
                                price={value['price']}
                                onNavigation={() => navigation.navigate(ScreenNames.PRODUCT, { productId: value['id'] })}
                            />
                        </View>
                    ))
                }
            </View>
        </ScrollView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: "column",
        backgroundColor: "#F1EFEF"
    },

    boundary: {
        width: '100%',
        height: 40,
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center'
    },

    divider: {
        flex: 1,
        height: 1,
        marginHorizontal: 5,
        backgroundColor: '#B4B4B3',
    },

    suggestion: {
        width: '100%',
        maxHeight: 10000,
        paddingHorizontal: 5,
        flexDirection: 'row',
        justifyContent: 'space-between',
        flexWrap: 'wrap',
        alignContent: 'space-around',
    }
});

export default PendingOrderScreen;