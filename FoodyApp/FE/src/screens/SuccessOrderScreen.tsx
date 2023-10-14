import { Text, StyleSheet, View, ScrollView, Image } from "react-native";
import ProductComponent from "../components/ProductComponent";
import EmptyOrderComponent from "../components/EmptyOrderComponent";
import OrderProductComponent from "../components/OrderProductComponent";
import { useState, useEffect } from "react";
import ScreenNames from "../utils/ScreenNames";
import { getAllOrderSuccess } from "../services/orderService";
import { getProductDiscount } from "../services/productService";

function emtyOrder() {
    return (
        <EmptyOrderComponent
            imageUrl={require('../assets/Icons/cart-xmark-svgrepo-com.png')}
            title="Quên chưa đặt món rồi nè bạn ơi?"
            detail="Thông tin đơn hàng đã được vận chuyển của bạn sẽ được hiển thị tại đây!"
        />
    );
}

const SuccessOrderScreen = ({navigation}: any) => {
    //kiểm tra nếu tồn tại order sẽ xóa màn emptyOrder
    const [shown, setShown] = useState(true);
    const [order, setOrder] = useState([]);
    const [product, setProduct] = useState([]);

    useEffect(() => {
        const getData = async () => {
            const orderResponse = await getAllOrderSuccess();
            setOrder(orderResponse?.data);

            const productDiscountResponse = await getProductDiscount();
            setProduct(productDiscountResponse?.data.item);
        };

        getData();

        if (order == null) {
            setShown(true);
        }
        else {
            setShown(false);
        }
    }, []);

    return (
        <ScrollView style={styles.container}>
            
            {shown ? emtyOrder() : ''}

            {//áp dụng với mỗi order chỉ có 1 sản phẩm
            order.map((value) => (
                <OrderProductComponent
                    key={value['id']}
                    imageUrl={`http://192.168.1.10:5010${value['products'][0]['productImageUrl']}`}
                    name={value['products'][0]['name']}
                    actualPrice={value['products'][0]['actualPrice']}
                    price={value['products'][0]['price']}
                    quantity={value['products'][0]['quantity']}
                    totalPrice={value['totalAmount']}
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
                        <ProductComponent
                            key={value['id']}
                            imageUrl={`http://192.168.1.10:5010${value['productImageUrl']}`}
                            name={value['name']}
                            actualPrice={value['actualPrice']}
                            price={value['price']}
                            onNavigation={() => navigation.navigate(ScreenNames.PRODUCT, {productId: value['id']})}
                        />
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
        height: 1, // Độ dày của đường kẻ
        marginHorizontal: 5,
        backgroundColor: '#B4B4B3', // Màu của đường kẻ
    },
    suggestion: {
        width: '100%',
        maxHeight: 10000,
        //backgroundColor: "#fca",
        flexDirection: 'row',
        justifyContent: 'space-around',
        flexWrap: 'wrap',
        alignContent: 'space-around',
    }
});
export default SuccessOrderScreen;