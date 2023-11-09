import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image, ImageSourcePropType } from 'react-native';

interface UserEditButtonProps {
  imageUrl: ImageSourcePropType;
  text: string;
  onNavigate: () => void;
}

const UserEditButton: React.FC<UserEditButtonProps> = ({ imageUrl, text, onNavigate }) => {
  return (
    <TouchableOpacity style={styles.container} onPress={onNavigate}>
      <Image source={imageUrl} style={styles.image} />
      <Text>{text}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 10,
    backgroundColor: '#fff'
  },

  image: {
    width: 20,
    height: 20,
    marginRight: 10,
  },
});

export default UserEditButton;