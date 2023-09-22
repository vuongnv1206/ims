import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PrimeNGConfig } from 'primeng/api';
import { AuthService } from './shared/services/auth.service';
import { LOGIN_URL } from './shared/constants/url.const';

@Component({
  selector: 'app-root',
  template: `
    <router-outlet></router-outlet>
    <p-confirmDialog [style]="{ width: '50vw' }" header="Confirmation" rejectButtonStyleClass="p-button-outlined" acceptButtonStyleClass=""></p-confirmDialog>
    <p-toast position="top-right"></p-toast>

  `,
})
export class AppComponent {
  title = 'angular';
  menuMode = 'static';
  constructor(
    private primeNgConfig : PrimeNGConfig,
    private authService : AuthService,
    private router: Router
    ){

  }
  ngOnInit(): void {
    this.primeNgConfig.ripple = true;
    document.documentElement.style.fontSize = '14px';

    if (this.authService.isAuthenticated() == false) {
      this.router.navigate([LOGIN_URL]);
    }

  };
}
