import { SocialAuthService } from '@abacritt/angularx-social-login';
import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { HOME_URL } from 'src/app/shared/constants/url.const';
import { AuthService } from 'src/app/shared/services/auth.service';
import { TokenService } from 'src/app/shared/services/token.service';
import { environment } from 'src/environments/environment';
import { AuthClient, RegisterModel } from '../../api/api-generate';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;
  registerForm: FormGroup;
  constructor(
    private _ngZone: NgZone,
    private socialAuthService: SocialAuthService,
    private authService: AuthService,
    private tokenService: TokenService,
    private fb: FormBuilder,
    private apiClient: AuthClient,
    private router: Router,
    private notificationService: NotificationService
  ) {
    this.registerForm = this.fb.group({
      username: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  ngOnInit(): void {
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      // @ts-ignore
      google.accounts.id.initialize({
        client_id: environment.clientId,
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true,
      });
      // @ts-ignore
      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById('buttonDiv'),
        { theme: 'outline', size: 'large', width: 400 }
      );
      // @ts-ignore
      google.accounts.id.prompt((notification) => {
        if (notification.isNotDisplayed()) {
          console.log(
            'getNotDisplayedReason: ',
            notification.getNotDisplayedReason()
          );
        }
        if (notification.isSkippedMoment()) {
          console.log('isSkippedMoment: ', notification.getSkippedReason());
        }
        if (notification.isDismissedMoment()) {
          console.log('isDismissedMoment: ', notification.getDismissedReason());
        }
      });
    };
  }

  async handleCredentialResponse(response: CredentialResponse) {
    await this.authService.LoginWithGoogle(response.credential).subscribe(
      (x: any) => {
        this._ngZone.run(() => {
          this.tokenService.saveToken(x.token);
          this.router.navigate([HOME_URL]);
        });
      },
      (error: any) => {
        this.notificationService.showError('Lỗi đăng ký tài khoản');
      }
    );
  }

  register() {
    this.toggleBlockUI(true);
    var request: RegisterModel = {
      username: this.registerForm.controls['username'].value,
      email: this.registerForm.controls['email'].value,
      password: this.registerForm.controls['password'].value,
    };

    this.apiClient
      .register(request)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (any) => {
          this.router.navigate(['/auth/confirm-email']);
        },
        error: (error: any) => {
          this.notificationService.showError(error);
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
}
