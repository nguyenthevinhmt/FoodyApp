import React, { Component, useEffect, useRef } from 'react';
import { SafeAreaView } from 'react-native-safe-area-context';
import { StyleSheet, Text, View, BackHandler, Alert } from 'react-native';
import { WebView } from 'react-native-webview';
import queryString from 'query-string';
import { parseUrl } from 'query-string/base';
import ScreenNames from '../utils/ScreenNames';

const WebVnPay = ({ navigation, route }: any) => {
    const url = route.params.url;

    const webViewRef = useRef(null);

    const handleMessage = (event: any) => {
        const data = JSON.parse(event.nativeEvent.data);
        console.log('data:', data);
        // Xử lý object trả về ở đây
        // ...
        if (data.url) {
            const parsedUrl = queryString.parseUrl(data.url);
            const vnp_ResponseCode = parsedUrl.query.vnp_ResponseCode;
            console.log('parsedUrl: ', parsedUrl);
            console.log('vnp_ResponseCode: ', vnp_ResponseCode);
        }
        };

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
            navigation.navigate(ScreenNames.MAIN); // Thay 'MainScreen' bằng tên màn hình chính của bạn
            Alert.alert('Payment Successful', 'Your payment has been processed successfully.');
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
        <SafeAreaView style={{ flex: 1 }}>
            <WebView 
                ref={webViewRef} 
                source={{ uri: url }} 
                onMessage={handleMessage} 
                onError={handleWebViewError} // Bắt lỗi và lấy thông tin chi tiết
                style={{ flex: 1 }} 
            />
        </SafeAreaView>
    );
}

export default WebVnPay;