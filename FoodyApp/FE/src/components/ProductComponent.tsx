import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, ImageSourcePropType, View, Dimensions } from 'react-native';

const windowWidth = Dimensions.get('window').width;

interface ProductComponentProps {
    imageUrl: ImageSourcePropType;
    name: string;
    actualPrice: number;
    price: number
}

const ProductComponent: React.FC<ProductComponentProps> = ({ imageUrl, name, actualPrice, price }) => {
    return (
        <TouchableOpacity style={styles.container}>
            <Image source={imageUrl} style={styles.image} />
            <View style={styles.productDetail}>
                <Text style={styles.name}>{name}</Text>
                <Text style={styles.actualPrice}>đ{actualPrice}</Text>
                <Text style={styles.price}>đ{price}</Text>
            </View>

        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    container: {
        width: "45%",
        marginVertical: 10,
        flexDirection: 'column',
        justifyContent: 'flex-start',
        alignItems: 'center',
        backgroundColor: '#fff'
    },
    image: {
        width: '100%',
        height: windowWidth*0.45,
    },
    productDetail: {
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center'
    },
    name: {
        fontSize: 13,
    },
    actualPrice: {
        fontSize: 12,
        color: '#B4B4B3',
        textDecorationLine: 'line-through'
    },
    price: {
        fontSize: 12,
        color: '#EE4D2D',
        fontWeight: '600'
    }
});

export default ProductComponent;