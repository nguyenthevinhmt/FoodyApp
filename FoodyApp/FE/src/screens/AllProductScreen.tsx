import React, { useEffect, useState } from 'react';
import { SafeAreaView } from "react-native-safe-area-context";
import { View, FlatList, Text, TouchableOpacity, StyleSheet } from 'react-native';
import { getListProduct } from '../services/productService';
import { baseURL_img } from '../utils/baseUrl';
import ScreenNames from '../utils/ScreenNames';
import ProductComponent from '../components/ProductComponent';

const AllProductScreen = ({ navigation }: any) => {
    const [currentPage, setCurrentPage] = useState(1);
    const itemsPerPage = 6; // Số sản phẩm hiển thị trên mỗi trang
    const [listProduct, setListProduct] = useState([]);

    useEffect(() => {
        const getData = async () => {
            //danh sách món ăn 
            const products = await getListProduct(null, null, 200, 1);
            setListProduct(products?.data.item);
        }

        getData();
    }, []);

    const handleNextPage = () => {
        setCurrentPage(currentPage + 1);
    };

    const handlePreviousPage = () => {
        setCurrentPage(currentPage - 1);
    };

    // Tính toán chỉ mục bắt đầu và kết thúc của danh sách sản phẩm trên trang hiện tại
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;

    // Lấy danh sách sản phẩm trên trang hiện tại
    const currentProducts = listProduct.slice(startIndex, endIndex);

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.header}>
                <Text style={styles.header_text}>Tất cả món ngon ở đây</Text>
                <Text style={{ fontSize: 11 }}>Chúng tôi có tất cả những món ăn để làm hài lòng vị giác của những khách hàng khó tính nhất.</Text>
            </View>

            <View style={{ flex: 1 }}>
                <FlatList
                    data={currentProducts}
                    keyExtractor={(item) => item['id']}
                    renderItem={({ item }) => (
                        // Hiển thị thông tin sản phẩm
                        <View style={styles.product}>
                            <ProductComponent
                                key={item['id']}
                                imageUrl={`${baseURL_img}${item['productImageUrl']}`}
                                name={item['name']}
                                actualPrice={item['actualPrice']}
                                price={item['price']}
                                onNavigation={() => navigation.navigate(ScreenNames.PRODUCT, { productId: item['id'] })}
                            />
                        </View>
                    )}
                    numColumns={2}
                    contentContainerStyle={styles.productList}
                />

                <View style={styles.navigation}>
                    <TouchableOpacity onPress={handlePreviousPage} disabled={currentPage === 1}>
                        <Text style={{ marginHorizontal: 5, color: '#EE4D2D' }}>{"<<<"}</Text>
                    </TouchableOpacity>

                    <Text style={{ marginHorizontal: 10, borderWidth: 0.5, borderColor: '#EE4D2D', width: 20, textAlign: 'center', color: '#EE4D2D' }}>{currentPage}</Text>

                    <TouchableOpacity onPress={handleNextPage} disabled={endIndex >= listProduct.length}>
                        <Text style={{ marginHorizontal: 5, color: '#EE4D2D' }}>{">>>"}</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </SafeAreaView>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#D2E3C8'
    },

    header: {
        justifyContent: 'center',
        alignItems: 'flex-start',
        height: 100,
        paddingHorizontal: 10,
        borderBottomWidth: 1,
        borderColor: '#EE4D2D',
    },

    header_text: {
        fontSize: 20,
        color: '#EE4D2D',
        marginVertical: 10
    },

    productList: {
        MarginHorizontal: 10,
        justifyContent: 'space-between',
        marginBottom: 10,
        backgroundColor: "#EBF3E8",
    },

    product: {
        flex: 1 / 2,
        maxWidth: '45%',
        marginHorizontal: 10,
        marginBottom: 5,
    },

    navigation: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'center',
        paddingVertical: 10,
        borderTopWidth: 1,
        borderColor: '#EE4D2D'
    }
});
export default AllProductScreen;