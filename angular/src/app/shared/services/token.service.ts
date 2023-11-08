import { Injectable } from '@angular/core';
import { UserModel } from '../models/user.model';


const TOKEN_KEY = 'access-token';


const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenService{
  constructor() {}

  signOut(): void {
    window.sessionStorage.clear();
  }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);

    const user = this.getUser();
    if (user?.id) {
       this.saveUser({ ...user, accessToken: token });
    }
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }
  public getUser(): UserModel | null {
    // const token = window.sessionStorage.getItem(USER_KEY);
    // if (!token)
    //   return null;
    // const base64Url = token.split('.')[1];
    // const base64 = base64Url.replace('-', '+').replace('_', '/');
    // const user: UserModel = JSON.parse(this.b64DecodeUnicode(base64));
    // return user;
    const userData = window.sessionStorage.getItem(USER_KEY);
  if (!userData)
    return null;
  const user: UserModel = JSON.parse(userData);
  return user;
  }

  public saveUser(user: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }


  b64DecodeUnicode(str) {
    return decodeURIComponent(Array.prototype.map.call(atob(str), function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
  }




}
