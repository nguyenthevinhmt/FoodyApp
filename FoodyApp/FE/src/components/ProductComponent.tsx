import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, View, Dimensions } from 'react-native';

const windowWidth = Dimensions.get('window').width;

interface ProductComponentProps {
    imageUrl: string;
    name: string;
    actualPrice: number;
    price: number;
    onNavigation: () => void
}

const ProductComponent: React.FC<ProductComponentProps> = ({ imageUrl, name, actualPrice, price, onNavigation }) => {
    return (
        <TouchableOpacity style={styles.container} onPress={onNavigation}>
            <Image source={{ uri: imageUrl }} style={styles.image} />

            <View style={styles.productDetail}>
                <Text style={styles.name}>{name}</Text>
                <Text style={styles.actualPrice}>{price.toLocaleString()}đ</Text>
                <Text style={styles.price}>{actualPrice.toLocaleString()}đ</Text>
            </View>

        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    container: {
        width: "100%",
        marginVertical: 10,
        paddingBottom: 20,
        flexDirection: 'column',
        justifyContent: 'flex-start',
        alignItems: 'center',
        backgroundColor: '#fffe'
    },
    image: {
        width: '100%',
        height: windowWidth * 0.45,
    },
    productDetail: {
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'flex-start',
        width: '100%',
        paddingLeft: 15
    },
    name: {
        paddingTop: 7,
        fontSize: 16,
        fontWeight: '700',

    },
    actualPrice: {
        fontSize: 10,
        color: '#B4B4B3',
        textDecorationLine: 'line-through'
    },
    price: {
        fontSize: 16,
        color: '#EE4D2D',
        fontWeight: '700'
    }
});

export default ProductComponent;