import { Text, StyleSheet, View, ScrollView, Image } from "react-native";
import ProductComponent from "../components/ProductComponent";
import EmptyOrderComponent from "../components/EmptyOrderComponent";
import { useState, useEffect, useCallback } from "react";
import OrderProductComponent from "../components/OrderProductComponent";
import ScreenNames from "../utils/ScreenNames";
import { getAllOrderCancel } from "../services/orderService";
import { getProductDiscount } from "../services/productService";
import { useFocusEffect } from "@react-navigation/native";
import { baseURL_img } from "../utils/baseUrl";

function emtyOrder() {
    return (
        <EmptyOrderComponent
            imageUrl={require('../assets/Icons/cart-xmark-svgrepo-com.png')}
            title="Quên chưa đặt món rồi nè bạn ơi?"
            detail="Thông tin đơn hàng đã hủy của bạn sẽ hiển thị ở đây!"
        />
    );
}

const CancelOrderScreen = ({navigation}: any) => {
    //kiểm tra nếu tồn tại order sẽ xóa màn emptyOrder
    const [shown, setShown] = useState(true);
    const [order, setOrder] = useState([]);
    const [product, setProduct] = useState([]);

    useFocusEffect(
        useCallback(() => {
          const getData = async () => {
            

            const orderResponse = await getAllOrderCancel();
            setOrder(orderResponse?.data);

            const productDiscountResponse = await getProductDiscount();
            setProduct(productDiscountResponse?.data.item);
          };

          getData();
        }, [])
    );
    
    useEffect(() => {
        if (Array.isArray(order) && order.length === 0) {
          setShown(true);
        } else {
          setShown(false);
        }
      }, [order]);

    return (
        <ScrollView style={styles.container}>
            
            {shown ? emtyOrder() : ''}

            {//áp dụng với mỗi order chỉ có 1 sản phẩm
            order.map((value) => (
                <OrderProductComponent
                    key={value['id']}
                    imageUrl={`${baseURL_img}${value['products'][0]['productImageUrl']}`}
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
                            imageUrl={`${baseURL_img}${value['productImageUrl']}`}
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
export default CancelOrderScreen;