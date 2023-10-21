// Alert.js
import React from "react";
import { View, Text, Modal, Button } from "react-native";

const Alert = ({ visible, message, onClose }: any) => {
  return (
    <Modal
      animationType="slide"
      transparent={true}
      visible={visible}
      onRequestClose={onClose}
    >
      <View
        style={{
          flex: 1,
          justifyContent: "center",
          alignItems: "center",
          height: 50,
        }}
      >
        <View
          style={{
            backgroundColor: "#fff",
            // opacity: 0.5,
            padding: 20,
            borderRadius: 10,
            borderWidth: 1,
            borderColor: "#ccc",
            height: "30%",
            width: "75%",
          }}
        >
          <Text style={{ marginVertical: 30, color: "#111" }}>{message}</Text>
          <Button color="#007BFF" onPress={onClose} title="OK"></Button>
        </View>
      </View>
    </Modal>
  );
};

export default Alert;
