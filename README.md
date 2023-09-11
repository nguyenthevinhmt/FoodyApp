# Đồ án FoodyMobileApp
### BackendAPI sử dụng ASP.NET core web api 7.0
### Frontend sử dụng react native expo
##Hướng dẫn tải về: 
- Sử dụng câu lệnh git clone để lưu được cả lịch sử commit
- Sau khi clone dự án về, tạo nhánh riêng ở local. Ví dụ Nguyễn Thế Vinh thì tạo tên nhánh là vinhnt/dev
- Nếu phát triển các cách chức năng thì pull code từ nhánh dev về
- Sau khi hoàn thành chức năng không tự động merge mà phải tạo merge request để review code, tránh conflict sau này
  
## Cách cấu hình backend api để frontend có thể gọi được api
- Vào terminal hoặc cmd, gõ lệnh ipconfig
- Lấy ra địa chỉ IPV4
- Vào Window Defender Firewall with Advanced Security, chọn Inbound Rules
![Window Defender Firewall with Advanced Security](https://github.com/nguyenthevinhmt/FoodyMobileApp/assets/81474434/324d93f9-fc24-42f8-a6ea-e809328d5966)
-Chọn New Rule ở thanh Actions bên phải, cửa sổ hiển thị ra chọn Port và next
![port](https://github.com/nguyenthevinhmt/FoodyMobileApp/assets/81474434/96f26a39-4528-4867-a27a-0bd48f9e4d08)
- Gõ port 5010, ấn next
![portselect](https://github.com/nguyenthevinhmt/FoodyMobileApp/assets/81474434/d65d616a-f437-43a8-afe9-28e97d3d6fca)
- Next liên tục rồi điền tên là Port 5010
- Bật api net core lên, chạy react native test api xem đã gọi được api chưa
- Lưu ý: Lúc này trong react native, địa chỉ api sẽ có định dạng sau: http://{địa chỉ IPV4}:5010/api/{tên controller}
- Ví dụ: 'http://192.168.1.171:5010/api/Pong'
