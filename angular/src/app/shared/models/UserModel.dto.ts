import { LoginResponseDto } from "./login-response.dto";

export interface UserModel extends LoginResponseDto {
  account: string;
  address: string;
  avatarResizeUrl: string;
  avatarUrl: string;
  birthday: Date;
  department: string;
  email: string;
  fullName: string;
  isActive: true;
  phone: boolean;
  position: string;
  roles: number[];
    userId: number;
    
}