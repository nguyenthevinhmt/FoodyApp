import React from 'react';
import { Text, StyleSheet, Image, ImageSourcePropType, View } from 'react-native';

interface EmptyOrderComponentProps {
    imageUrl: ImageSourcePropType;
    title: string;
    detail: string
}

const EmptyOrderComponent: React.FC<EmptyOrderComponentProps> = ({ imageUrl, title, detail }) => {
    return (
        <View style={styles.main}>
            <Image
                source={imageUrl}
                style={{
                    width: 150,
                    height: 150,
                    marginBottom: 20
                }}
            />

            <Text style={{
                width: '70%',
                textAlign: 'center',
                fontWeight: '600',
                fontSize: 15,
                marginBottom: 20
            }}>{title}</Text>

            <Text style={{
                width: '80%',
                textAlign: 'center',
                fontSize: 10,
            }}>{detail}</Text>
        </View>
    );
};

const styles = StyleSheet.create({
    main: {
        width: '100%',
        alignItems: "center",
        justifyContent: "center",
        paddingVertical: 40
    }
});

export default EmptyOrderComponent;