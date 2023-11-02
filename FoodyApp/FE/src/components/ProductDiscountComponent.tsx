import React, { useState } from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, View, Alert, ToastAndroid } from 'react-native';
import { addProductToCart } from '../services/cartService';

// function showAlert(message: string) {
//     return (
//         Alert.alert(
//             'Thông báo',
//             message,
//             [{ text: 'Ok', onPress: () => { } }],
//             { cancelable: false }
//         )
//     )
// };

interface ProductDiscountComponentProps {
    id: number;
    imageUrl: string;
    name: string;
    actualPrice: number;
    price: number;
    discount: number;
    onNavigation: () => void
}

const ProductDiscountComponent: React.FC<ProductDiscountComponentProps> = ({ id, imageUrl, name, actualPrice, price, discount, onNavigation }) => {
    //hiển thị alert

    const handleAddCart = async () => {
        const result = await addProductToCart(id);
        if (result != null) {
            showAlert("Thêm sản phẩm vào giỏ hàng thành công");
        }
        else {
            showAlert("Có lỗi khi thêm sản phẩm vào giỏ hàng");
        }
    }

    const showAlert = (text: string) => {
        ToastAndroid.show(text, ToastAndroid.SHORT);
    };

    return (
        <TouchableOpacity style={styles.container} onPress={onNavigation}>
            <View style={styles.product}>
                <Image source={{ uri: imageUrl }} style={styles.image} />

                <View style={styles.productDetail}>
                    <Text style={styles.name}>{name}</Text>
                    <Text style={styles.price}>{price.toLocaleString()}đ</Text>
                    <Text style={styles.actualPrice}>{actualPrice.toLocaleString()}đ</Text>
                    <View style={styles.discount}>
                        <Text style={{ color: '#EE4D2D', fontSize: 10 }}>Giảm {discount}%</Text>
                    </View>
                </View>
            </View>

            <View style={styles.discountArea}>
                <View style={styles.buttonArea}>
                    <TouchableOpacity style={styles.button} onPress={() => handleAddCart()}>
                        <Text style={{ color: '#fff' }}>Thêm vào giỏ</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    container: {
        width: "100%",
        marginVertical: 5,
        paddingVertical: 15,
        paddingBottom: 30,
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        backgroundColor: '#fff'
    },

    product: {
        flexDirection: 'row',
    },

    image: {
        width: 110,
        height: 110,
        marginHorizontal: 10
    },
    productDetail: {
        height: 110,
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'flex-start'
    },
    name: {
        fontSize: 16,
        fontWeight: '700',
        marginBottom: 5
    },
    price: {
        fontSize: 9,
        color: '#B4B4B3',
        textDecorationLine: 'line-through'
    },
    actualPrice: {
        fontSize: 16,
        color: '#EE4D2D',
        fontWeight: '700'
    },

    discount: {
        borderWidth: 1,
        borderRadius: 5,
        borderColor: '#EE4D2D',
        paddingVertical: 2,
        paddingHorizontal: 4
    },

    discountArea: {
        height: 110,
        justifyContent: 'center'
    },

    buttonArea: {
        marginRight: 5,
    },

    button: {
        borderRadius: 10,
        backgroundColor: '#EE4D2D',
        padding: 10,
        justifyContent: 'center',
        alignItems: 'center'
    }
});


export default ProductDiscountComponent;