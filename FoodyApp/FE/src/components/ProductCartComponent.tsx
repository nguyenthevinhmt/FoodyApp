import React, { useState } from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, View, ToastAndroid } from 'react-native';
import { updateProductQuantity } from '../services/cartService';

interface ProductCartComponentProps {
    productId: number
    imageUrl: string;
    name: string;
    actualPrice: number;
    price: number;
    Quantity: number;
    onNavigation: () => void,
    onAction: () => void
}  

const ProductCartComponent: React.FC<ProductCartComponentProps> = ({ productId, imageUrl, name, actualPrice, price, Quantity, onNavigation, onAction }) => {
    const handleUpdate = async (update_quantity: number) => {
        const result = await updateProductQuantity(productId, update_quantity);
        const response = result?.data;
        console.log(result);
        if (result != null && response !== 'sản phẩm đã được xóa khỏi giỏ hàng') {
            onAction();
        }
        else if (result == null || response == 'sản phẩm đã được xóa khỏi giỏ hàng') {
            ToastAndroid.show("Đã xóa sản phẩm khỏi giỏ hàng", ToastAndroid.SHORT);
            onAction();
        }
    }

    return (
        <TouchableOpacity style={styles.container} onPress={onNavigation}>
            <Image source={{ uri: imageUrl }} style={styles.image} />

            <View style={styles.productDetail}>
                <View>
                    <Text style={styles.name}>{name}</Text>
                    <View style={{width: '100%', flexDirection: 'row', alignItems: 'center', marginTop: 10}}>
                    <Text style={styles.price}>{price.toLocaleString()}đ</Text>
                    <Text style={styles.actualPrice}>{actualPrice.toLocaleString()}đ</Text>
                    </View>
                </View>

                <View style={styles.updateQuantity}>
                    <TouchableOpacity style={styles.subtraction} onPress={() => handleUpdate(-1)}>
                        <Image source={require('../assets/Icons/subtraction-logo.png')} style={styles.quantity_logo} />
                    </TouchableOpacity>

                    <Text style={styles.quantity}>{Quantity}</Text>

                    <TouchableOpacity style={styles.addition} onPress={() => handleUpdate(1)}>
                        <Image source={require('../assets/Icons/addition-logo.png')} style={styles.quantity_logo} />
                    </TouchableOpacity>
                </View>
            </View>
        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    container: {
        width: '100%',
        marginBottom: 10,
        paddingVertical: 15,
        flexDirection: 'row',
        justifyContent: 'flex-start',
        alignItems: 'center',
        backgroundColor: '#fff'
    },

    image: {
        width: 90,
        height: 90,
        marginHorizontal: 10,
    },

    productDetail: {
        height: 110,
        justifyContent: 'space-around',
        flexDirection: 'column',
        alignItems: 'flex-start'
    },

    name: {
        fontSize: 16,
        fontWeight: '400'
    },

    price: {
        fontSize: 12,
        color: '#B4B4B3',
        marginRight: 6,
        textDecorationLine: 'line-through'
    },

    actualPrice: {
        fontSize: 12,
        color: '#EE4D2D',
        fontWeight: '700'
    },

    updateQuantity: {
        flexDirection: 'row',
    },

    subtraction: {
        width: 25,
        height: 25,
        alignItems: 'center',
        justifyContent: 'center',
        borderWidth: 0.7,
        borderColor: '#EE4D2D'
    },

    quantity: {
        marginHorizontal: 15
    },

    addition: {
        width: 25,
        height: 25,
        alignItems: 'center',
        justifyContent: 'center',
        borderWidth: 0.7,
        borderColor: '#EE4D2D',
        backgroundColor: '#EE4D2D'
    },

    quantity_logo: {
        width: 20,
        height: 20
    }
});

export default ProductCartComponent;