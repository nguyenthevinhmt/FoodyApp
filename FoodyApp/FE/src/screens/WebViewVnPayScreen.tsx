import React, { useEffect, useRef } from 'react';
import { SafeAreaView } from 'react-native-safe-area-context';
import { BackHandler, Alert, View } from 'react-native';
import { WebView } from 'react-native-webview';
import queryString from 'query-string';
import ScreenNames from '../utils/ScreenNames';

const WebVnPay = ({ navigation, route }: any) => {
    const url = route.params.url;

    const webViewRef = useRef(null);

    const handleBackButtonPress = () => {
        const webView = webViewRef.current as WebView | null;
        if (webView !== null) {
            webView.goBack();
            return true; // Ngăn chặn việc thoát ứng dụng khi WebView không thể quay lại nữa
        }
        return false;
    };

    const handleWebViewError = (event: any) => {
        const { canGoBack, canGoForward, code, description, loading, target, title, url } = event.nativeEvent;
        console.log('Encountered an error loading page', { canGoBack, canGoForward, code, description, loading, target, title, url });
        
        const parsedUrl = queryString.parseUrl(url);
        const vnp_ResponseCode = parsedUrl.query.vnp_ResponseCode;
        console.log('vnp_ResponseCode:', vnp_ResponseCode);

        if (vnp_ResponseCode === '00') {
            navigation.navigate(ScreenNames.MAIN, { screen: 'Order' }) 
            Alert.alert('Thông báo', 'Giao dịch thành công.');
        }
        else if (vnp_ResponseCode === '24') {
            navigation.navigate(ScreenNames.MAIN)
            Alert.alert('Thông báo', 'Giao dịch không thành công do: Khách hàng hủy giao dịch.');
        }
        else {
            navigation.navigate(ScreenNames.MAIN)
            Alert.alert('Thông báo', 'Có lỗi trong quá trình thanh toán. Vui lòng thử lại sau.');
        }
    };

    // Đăng ký sự kiện khi người dùng nhấn nút Back
    useEffect(() => {
        BackHandler.addEventListener('hardwareBackPress', handleBackButtonPress);
        return () => {
            BackHandler.removeEventListener('hardwareBackPress', handleBackButtonPress);
        };
    }, []);
    
    return (
        <View style={{ flex: 1 }}>
            <WebView 
                ref={webViewRef} 
                source={{ uri: url }} 
                onError={handleWebViewError} // Bắt lỗi và lấy thông tin chi tiết
                style={{ flex: 1 }} 
            />
        </View>
    );
}

export default WebVnPay;