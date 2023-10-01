export interface TokenResponse {
  accessToken: string | undefined;
  refreshToken: string | undefined;
}
export interface UserLogin {
  email: string | null;
  password: string | null;
}
