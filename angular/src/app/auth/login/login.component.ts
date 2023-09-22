import { LoginModel } from './../../api/api-generate';
import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AuthClient, AuthResponse } from 'src/app/api/api-generate';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { HOME_URL } from 'src/app/shared/constants/url.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { TokenService } from 'src/app/shared/services/token.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent implements OnDestroy {

    private ngUnsubscribe = new Subject<void>();
    loginForm: FormGroup;
    public blockedPanel:boolean = false;

    constructor(
      public layoutService: LayoutService,
      private fb:FormBuilder,
      private notificationService : NotificationService,
      private router: Router,
      private tokenService : TokenService,
      private apiClient : AuthClient
      )
      {
      this.loginForm = this.fb.group({
        username: new FormControl('',Validators.required),
        password: new FormControl('',Validators.required),
      });
     }



     login() {
      this.toggleBlockUI(true);
      var request: LoginModel = ( {
        username: this.loginForm.controls['username'].value,
        password: this.loginForm.controls['password'].value,
      });
      this.apiClient
        .login(request)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: (res: AuthResponse) => {
            this.tokenService.saveToken(res.token);
            this.toggleBlockUI(false);
            this.router.navigate([HOME_URL]);
          },
          error: (error : any) => {
            console.log(error);
            this.notificationService.showError("Đăng nhập không đúng.")
            this.toggleBlockUI(false);
          },
        });
    }

    private toggleBlockUI(enabled: boolean) {
      if (enabled == true) {
        this.blockedPanel = true;
      } else {
        setTimeout(() => {
          this.blockedPanel = false;
        }, 1000);
      }
    }

     ngOnDestroy(): void {
      this.ngUnsubscribe.next();
      this.ngUnsubscribe.complete();
    }
}
