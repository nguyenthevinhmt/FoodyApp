import { Text, StyleSheet, View, ScrollView, Image } from "react-native";
import ProductComponent from "../components/ProductComponent";
import EmptyOrderComponent from "../components/EmptyOrderComponent";
import { useState } from "react";

function emtyOrder() {
    return (
        <EmptyOrderComponent
            imageUrl={require('../assets/Icons/cart-xmark-svgrepo-com.png')}
            title="Quên chưa đặt món rồi nè bạn ơi?"
            detail="Bạn sẽ nhìn thấy các món đang được chuẩn bị tại đây để kiểm tra đơn hàng nhanh hơn!"
        />
    );
}

const PendingOrderScreen = () => {
    //kiểm tra nếu tồn tại order sẽ xóa màn emptyOrder
    const [shown, setShown] = useState(true);

    return (
        <ScrollView style={styles.container}>
            
            {shown ? emtyOrder() : ''}

            <View style={styles.boundary}>
                <View style={styles.divider} />
                <Text style={{
                    color: '#B4B4B3',
                }}>Có thể bạn cũng thích</Text>
                <View style={styles.divider} />
            </View>

            <View style={styles.suggestion}>
                <ProductComponent
                    imageUrl={require('../assets/images/food-demo.jpg')}
                    name="Cơm rang dưa bò"
                    actualPrice={45000}
                    price={35000}
                />

                <ProductComponent
                    imageUrl={require('../assets/images/food-demo.jpg')}
                    name="Cơm rang dưa bò"
                    actualPrice={45000}
                    price={35000}
                />

                <ProductComponent
                    imageUrl={require('../assets/images/food-demo.jpg')}
                    name="Cơm rang dưa bò"
                    actualPrice={45000}
                    price={35000}
                />

                <ProductComponent
                    imageUrl={require('../assets/images/food-demo.jpg')}
                    name="Cơm rang dưa bò"
                    actualPrice={45000}
                    price={35000}
                />

                <ProductComponent
                    imageUrl={require('../assets/images/food-demo.jpg')}
                    name="Cơm rang dưa bò"
                    actualPrice={45000}
                    price={35000}
                />

                <ProductComponent
                    imageUrl={require('../assets/images/food-demo.jpg')}
                    name="Cơm rang dưa bò"
                    actualPrice={45000}
                    price={35000}
                />
            </View>
        </ScrollView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: "column",
        backgroundColor: "#F1EFEF"
    },
    boundary: {
        width: '100%',
        height: 40,
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center'
    },
    divider: {
        flex: 1,
        height: 1, // Độ dày của đường kẻ
        marginHorizontal: 5,
        backgroundColor: '#B4B4B3', // Màu của đường kẻ
    },
    suggestion: {
        width: '100%',
        maxHeight: 10000,
        //backgroundColor: "#fca",
        flexDirection: 'row',
        justifyContent: 'space-around',
        flexWrap: 'wrap',
        alignContent: 'space-around',
    }
});
export default PendingOrderScreen;