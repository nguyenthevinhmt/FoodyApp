import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, ImageSourcePropType, View } from 'react-native';

interface AddressComponentProps {
    addressType: number;
    province: string;
    district: string;
    ward: string;
    street: string;
    detail: string;
    name: string;
    phoneNumber: string,
    onNavigate: () => void;
}

const imageUrls: { [key: number]: ImageSourcePropType } = {
    1: require('../assets/Icons/home-icon.png'),
    2: require('../assets/Icons/department-icon.png'),
    3: require('../assets/Icons/save-icon.png'),
  };

const titles: { [key: number]: string } = {
    1: 'Nhà',
    2: 'Công ty',
    3: 'Khác',
  };

const AddressComponent: React.FC<AddressComponentProps> = ({addressType, province, district, ward, street, detail, name, phoneNumber, onNavigate }) => {
    const img = imageUrls[addressType];
    const title = titles[addressType];
    
    return (
        <TouchableOpacity style={styles.container} onPress={onNavigate}>
            <View style={styles.left}>
                <Image source={img} style={styles.image} />
            </View>

            <View style={styles.right}>
                <Text style={styles.title}>{title}</Text>
                <Text style={styles.address}>[{detail}], {street}, {ward}, {district}, {province}</Text>
                <Text style={styles.info}>{name} | {phoneNumber}</Text>
            </View>
        </TouchableOpacity>
    );
};

const styles = StyleSheet.create({
    container: {
        width: "100%",
        flexDirection: 'row',
        alignItems: 'flex-start',
        marginVertical: 2,
        paddingHorizontal: 4,
        paddingVertical: 10,
        backgroundColor: '#fff',
    },

    left: {
        width: '10%',
        alignItems: 'center',
        paddingTop: 5,
    },

    image: {
        width: 20,
        height: 20,
    },

    right: {
        width: '90%',
    },

    title: {
        fontSize: 15,
        marginBottom: 5,
    },

    address: {
        fontSize: 13,
        marginBottom: 5,
    },
    
    info: {
        fontSize: 13,
        marginBottom: 5,
    }
});

export default AddressComponent;