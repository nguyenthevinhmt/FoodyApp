// export const ValidationForm = (email: string, password: string) => {
//   const regex = /^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$/;
//   if (email.trim() === "" && password.trim() === "") {
//     return "Email không được bỏ trống!";
//   } else if (!regex.test(email)) {
//     return "Email sai định dạng";
//   } else if (password.length < 6) {
//     return "Mật khẩu phải có từ 6 kí tự trở lên";
//   } else if (!/[A-Z]/.test(password)) {
//     return "Chữ cái đầu phải viết hoa!";
//   } else if (!/[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(password)) {
//     return "Mật khẩu phải chứa ít nhất 1 kí tự đặc biệt";
//   } else {
//     return "";
//   }
// };
export const ValidationEmail = (email: string) => {
  const regex = /^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$/;
  if (email.trim() === "") {
    return "Email không được bỏ trống!";
  } else if (!regex.test(email)) {
    return "Email sai định dạng";
  }
  return null;
};

export const ValidationPassword = (password: string) => {
  if (password.trim() === "") {
    return "Mật khẩu không được bỏ trống!";
  } else if (password.length < 6) {
    return "Mật khẩu phải có từ 6 kí tự trở lên";
  } else if (!/[A-Z]/.test(password)) {
    return "Chữ cái đầu phải viết hoa!";
  } else if (!/[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(password)) {
    return "Mật khẩu phải chứa ít nhất 1 kí tự đặc biệt";
  } else {
    return null;
  }
};

export const ValidationRePassword = (password: string, rePassword: string) => {
  if (password.trim() !== rePassword.trim()) {
    return "Mật khẩu không khớp";
  } else {
    return null;
  }
};
