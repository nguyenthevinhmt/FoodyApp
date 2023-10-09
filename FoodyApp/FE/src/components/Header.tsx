import { View, Text, StyleSheet, Image, TouchableOpacity, TextInput } from "react-native";

export default function Header() {
    function location() { }

    return (
        <View style={styles.container}>
            <Text style={styles.text1}>Giao đến:</Text>
            <View style={styles.view1}>
                <Image
                    source={require("../assets/Icons/LocationIcon.png")}
                    style={styles.img1}
                />
                <TouchableOpacity onPress={location}>
                    <Text style={styles.text2} >Nhập vị trí của bạn</Text>
                </TouchableOpacity>
            </View>
            <View style={styles.view2}>
                <Image
                    source={require("../assets/Icons/SearchIcon.png")}
                    style={styles.img2}
                />
                <TextInput style={styles.input} placeholder=" Tìm kiếm loại món ăn, combo,..." >
                </TextInput>  

            </View>


        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        padding: 7,
    },
    view1: {
        flexDirection: 'row',
    },
    view2: {
        height:35,
        marginTop:5,
        borderWidth:1,
        borderRadius:3,
        flexDirection: 'row',
    },
    text1: {
        textAlign: 'left',
        fontSize: 13
    },
    text2: {
        textAlign: 'left',
        fontSize: 13,
        fontWeight: '500',
        paddingTop: 5
    },
    input: {
        width: 330,
        height: 35,
    },
    img1: {
        width: 13,
        height: 20,
        marginRight: 5
    },
    img2: {
        margin:5,
        width: 20,
        height: 23,
        
    }
});