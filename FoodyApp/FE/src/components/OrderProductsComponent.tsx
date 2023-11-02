import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, View } from 'react-native';
import { baseURL_img } from '../utils/baseUrl';

interface OrderProductsComponentProps {
    products: any[],
    totalPrice: number,
    onNavigation: () => void
}

const OrderProductsComponent: React.FC<OrderProductsComponentProps> = ({ products, totalPrice, onNavigation }) => {
    return (
        <View style={styles.container}>
            {
                products ? products.map((value) => (
                    <View style={styles.product} key={value['id']}>
                        <Image source={{ uri: `${baseURL_img}${value['productImageUrl']}` }} style={styles.image} />

                        <View style={styles.productDetail}>
                            <Text style={styles.name}>{value['name']}</Text>
                            <Text style={styles.actualPrice}>{(value['price'] | 0).toLocaleString()}đ</Text>
                            <Text style={styles.price}>{(value['actualPrice'] | 0).toLocaleString()}đ</Text>
                            <Text style={styles.quantity}>x{value['quantity']}</Text>
                        </View>
                    </View>
                )) : ''
            }

            <View style={styles.totalPrice}>
                <Text>Thành tiền:   <Text style={{ color: '#EE4D2D', fontWeight: '600' }}>{totalPrice.toLocaleString()}đ</Text></Text>
            </View>

            <View style={styles.buttonArea}>
                <TouchableOpacity style={styles.button} onPress={onNavigation}>
                    <Text style={{
                        alignSelf: 'center',
                        color: '#fff'
                    }}>Chi tiết đơn hàng</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        width: '100%',
        flexDirection: 'column',
        marginVertical: 5,
        backgroundColor: '#fff'
    },

    product: {
        width: "100%",
        height: 100,
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        padding: 10,
        borderBottomWidth: 1,
        borderBottomColor: '#ddd'
    },

    image: {
        width: 80,
        height: 80,
        marginRight: 10,
        marginLeft: 5,
    },

    productDetail: {
        flexDirection: 'column'
    },

    name: {
        fontSize: 20,
    },

    actualPrice: {
        alignSelf: 'flex-end',
        fontSize: 13,
        color: '#B4B4B3',
        textDecorationLine: 'line-through'
    },

    price: {
        alignSelf: 'flex-end',
        fontSize: 13,
        color: '#EE4D2D',
        fontWeight: '600'
    },

    quantity: {
        alignSelf: 'flex-end',
        color: '#B4B4B3',
        alignItems: 'flex-end'
    },

    totalPrice: {
        width: '100%',
        height: 40,
        flexDirection: 'row',
        justifyContent: 'flex-end',
        paddingHorizontal: 10,
        alignItems: 'center',
        borderBottomWidth: 1,
        borderBottomColor: '#ddd'
    },

    buttonArea: {
        width: '100%',
        height: 60,
        flexDirection: 'row',
        justifyContent: 'flex-end',
        alignItems: 'center'
    },

    button: {
        width: 140,
        height: 40,
        backgroundColor: '#EE4D2D',
        justifyContent: 'center',
        alignItem: 'center',
        marginRight: 10,
        borderRadius: 8
    }
});

export default OrderProductsComponent;