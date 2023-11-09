import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, ImageSourcePropType } from 'react-native';

interface AddAddressComponentProps {
    addressType: number;
    onNavigate: () => void;
}

const imageUrls: { [key: number]: ImageSourcePropType } = {
    1: require('../assets/Icons/home-icon.png'),
    2: require('../assets/Icons/department-icon.png'),
    3: require('../assets/Icons/save-icon.png'),
};

const titles: { [key: number]: string } = {
    1: 'nhà',
    2: 'công ty',
    3: 'khác',
};

const AddAddressComponent: React.FC<AddAddressComponentProps> = ({ addressType, onNavigate }) => {
    const img = imageUrls[addressType];
    const title = titles[addressType];

    return (
        <TouchableOpacity style={styles.container} onPress={onNavigate}>
            <Image source={img} style={styles.img} />
            <Text style={styles.title}>Thêm địa chỉ {title}</Text>
        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    container: {
        width: "100%",
        height: 60,
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'flex-start',
        paddingLeft: 20,
        marginVertical: 2,
        backgroundColor: '#fff'
    },

    img: {
        width: 20,
        height: 20,
        marginRight: 10
    },

    title: {
        //fontWeight: '600',
        fontSize: 15
    }
});

export default AddAddressComponent;