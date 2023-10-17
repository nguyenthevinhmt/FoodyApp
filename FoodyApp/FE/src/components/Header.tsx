import { View, Text, StyleSheet, Image, TouchableOpacity, TextInput } from "react-native";
import { useNavigation } from '@react-navigation/native';
import { useState } from "react";
import ScreenNames from "../utils/ScreenNames";



export default function Header() {
    function location() { }
    
    const [searchText, setSearchText] = useState('');
    const navigation = useNavigation();

  const handleSearch = () => {
    // Điều hướng sang màn hình khác và truyền dữ liệu
  };

    return (
        <View style={styles.headerArea}>
            <Text style={styles.locationText}>Giao đến:</Text>
            <View style={styles.Location}>
                <Image
                    source={require("../assets/Icons/LocationIcon.png")}
                    style={styles.locationIcon}
                />
                <TouchableOpacity onPress={location}>
                    <Text style={styles.locationChoose} >Nhập vị trí của bạn</Text>
                </TouchableOpacity>
            </View>
            <View style={styles.Search}>
                <Image
                    source={require("../assets/Icons/SearchIcon.png")}
                    style={styles.searchIcon}
                />
                <TextInput 
                    style={styles.input} 
                    placeholder=" Tìm kiếm loại món ăn, combo,..." 
                    value={searchText}
                    onChangeText={setSearchText}
                    onSubmitEditing={handleSearch}
                >
                </TextInput>  

            </View>


        </View>
    )
}

const styles = StyleSheet.create({
    headerArea: {
        padding: 7,
    },
    Location: {
        flexDirection: 'row',
    },
    Search: {
        height:35,
        marginTop:5,
        borderWidth:1,
        borderRadius:3,
        flexDirection: 'row',
    },
    locationText: {
        textAlign: 'left',
        fontSize: 13
    },
    locationChoose: {
        textAlign: 'left',
        fontSize: 13,
        fontWeight: '500',
        paddingTop: 5
    },
    input: {
        width: 330,
        height: 35,
    },
    locationIcon: {
        width: 13,
        height: 20,
        marginRight: 5
    },
    searchIcon: {
        margin:5,
        width: 20,
        height: 23,
        
    }
});