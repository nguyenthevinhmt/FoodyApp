import React from 'react';
import { TouchableOpacity, Image } from 'react-native';
import Toast from 'react-native-toast-message';

interface ToastMessageProps {
  title: string;
  onPress?: () => void;
}

const ToastMessage: React.FC<ToastMessageProps> = ({ title, onPress }) => {
  const showToast = () => {
    Toast.show({
      type: 'success',
      text1: 'Thông báo',
      text2: title,
      onPress: onPress,
    });
  };

  return (
    <TouchableOpacity onPress={showToast}>
      {/* Add your custom icon or image here */}
      <Image source={require('../path/to/your/icon.png')} />
    </TouchableOpacity>
  );
};

export default ToastMessage;