import { Injectable } from "@angular/core";
import { LoginRequestDto } from "../models/login-request.dto";
import { LoginResponseDto } from "../models/login-response.dto";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { TokenService } from "./token.service";

@Injectable({
  providedIn: 'root',
})


export class AuthService{
  constructor(private httpClient: HttpClient,private tokenService: TokenService)
   {

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
