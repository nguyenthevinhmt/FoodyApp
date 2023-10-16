import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, ImageSourcePropType, View } from 'react-native';

interface OrderProductComponentProps {
    imageUrl: string;
    name: string;
    actualPrice: number;
    price: number,
    quantity: number,
    totalPrice: number,
}

const OrderProductComponent: React.FC<OrderProductComponentProps> = ({ imageUrl, name, actualPrice, price, quantity, totalPrice }) => {
    return (
        <View style={styles.container}>
            <View style={styles.product}>
                <Image source={{uri: imageUrl}} style={styles.image} />
                <View style={styles.productDetail}>
                    <Text style={styles.name}>{name}</Text>
                    <Text style={styles.actualPrice}>đ{price}</Text>
                    <Text style={styles.price}>đ{actualPrice}</Text>
                </View>
            </View>

            <View style={styles.totalPrice}>
                <Text style={{color: '#B4B4B3'}}>{quantity} sản phẩm</Text>
                <Text>Thành tiền:   <Text style={{color: '#EE4D2D', fontWeight: '600'}}>đ{totalPrice}</Text></Text>
            </View>

            <View style={styles.buttonArea}>
                <TouchableOpacity style={styles.button}>
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
        borderBottomColor: '#BCA37F'
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
    totalPrice: {
        width: '100%',
        height: 40,
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingHorizontal: 10,
        alignItems: 'center',
        borderBottomWidth: 1,
        borderBottomColor: '#BCA37F'
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

export default OrderProductComponent;