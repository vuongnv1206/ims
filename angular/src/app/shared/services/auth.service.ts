import { Injectable } from "@angular/core";
import { LoginRequestDto } from "../models/login-request.dto";
import { LoginResponseDto } from "../models/login-response.dto";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { TokenService } from "./token.service";
import { Auth, getAuth, User } from "@angular/fire/auth";
import { FirebaseApp } from "@angular/fire/app";

@Injectable({
  providedIn: 'root',
})


export class AuthService{
  auth: Auth;
  constructor(
    private httpClient: HttpClient,
    private tokenService: TokenService,
    private afApp: FirebaseApp)
   {
    this.auth = getAuth(this.afApp);
   }

  public isAuthenticated(): boolean {
    return this.tokenService.getToken() != null;
  }

  public logout(){
    this.tokenService.signOut();
  }

  public navigateToLogin(): void{

  };

}
