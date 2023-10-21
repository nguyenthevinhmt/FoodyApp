import React, { useState } from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, View } from 'react-native';
import { addProductToCart } from '../services/cartService';
import Alert from './CustomAlert';


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
    const [showAlert, setShowAlert] = useState(false);
    const [message, setMessage] = useState('');

    const handleAddCart = async () => {
        const result = await addProductToCart(id);
        if (result != null) {
            console.log(result);
            setMessage('Thêm sản phẩm vào giỏ hàng thành công.');
            setShowAlert(true);
        }
        else {
            setMessage('Không thể thêm sản phẩm vào giỏ hàng.');
            setShowAlert(true);
        }
    }

    return (
        <TouchableOpacity style={styles.container} onPress={onNavigation}>
            <View style={styles.product}>
                <Image source={{ uri: imageUrl }} style={styles.image} />

                <View style={styles.productDetail}>
                    <Text style={styles.name}>{name}</Text>
                    <Text style={styles.price}>{price.toLocaleString()}đ</Text>
                    <Text style={styles.actualPrice}>{actualPrice.toLocaleString()}đ</Text>
                </View>
            </View>

            <View style={styles.discountArea}>
                <View style={styles.discount}>
                    <Text style={{ color: '#EE4D2D' }}>Giảm </Text>
                    <Text style={{ color: '#EE4D2D' }}>{discount}%</Text>
                </View>

                <View style={styles.buttonArea}>
                    <TouchableOpacity style={styles.button} onPress={() => handleAddCart()}>
                        <Text style={{ color: '#fff' }}>Thêm vào giỏ</Text>
                    </TouchableOpacity>
                </View>
            </View>

            <Alert
                visible={showAlert}
                message={message}
                onClose={() => setShowAlert(false)}
            />

        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    container: {
        width: "100%",
        marginVertical: 10,
        paddingVertical: 15,
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
        fontWeight: '500',
        marginBottom: 5
    },
    price: {
        fontSize: 12,
        color: '#B4B4B3',
        textDecorationLine: 'line-through'
    },
    actualPrice: {
        fontSize: 12,
        color: '#EE4D2D',
        fontWeight: '600'
    },

    discountArea: {
        height: 110,
        justifyContent: 'space-between'
    },

    discount: {
        marginRight: 5,
        flexDirection: 'row',
        height: '20%',
        alignSelf: 'flex-end'
    },

    buttonArea: {
        height: '60%',
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