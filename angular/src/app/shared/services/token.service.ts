import { Injectable } from '@angular/core';

const TOKEN_KEY = 'access-token';




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
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }





}
