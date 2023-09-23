
import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import {  AuthClient, AuthResponse, LoginModel } from 'src/app/api/api-generate';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { HOME_URL } from 'src/app/shared/constants/url.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { TokenService } from 'src/app/shared/services/token.service';
import { authCodeFlowConfig } from './authCodeFlowConfig';
import { AuthService } from 'src/app/shared/services/auth.service';;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
    `
      :host ::ng-deep .pi-eye,
      :host ::ng-deep .pi-eye-slash {
        transform: scale(1.6);
        margin-right: 1rem;
        color: var(--primary-color) !important;
      }

      .or-text {
        position: relative;
        z-index: 1;
      }

      .or-text span {
        background: #fff;
        padding: 0 15px;
      }

      .or-text:before {
        border-top: 2px solid #dfdfdf;
        content: '';
        margin: 0 auto;
        position: absolute;
        top: 50%;
        left: 0;
        right: 0;
        bottom: 0;
        width: 95%;
        z-index: -1;
      }
    `,
  ],
})
export class LoginComponent implements OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  
  code: string = '';
  state: string = '';

  loginForm: FormGroup;
  public blockedPanel: boolean = false;

  constructor(
    public layoutService: LayoutService,
    private fb: FormBuilder,
    private router: Router,
    private tokenService: TokenService,
    private route: ActivatedRoute,
    private apiClient: AuthClient,
    private notificationService: NotificationService,
  ) {
    this.loginForm = this.fb.group({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.code = params['code'];
      this.state = params['state'];
      if (this.code !== undefined) {
        this.authenUrl();
      } else {
        localStorage.clear();
      }
    });
  }

  login() {
    this.toggleBlockUI(true);
    var request: LoginModel = {
      username: this.loginForm.controls['username'].value,
      password: this.loginForm.controls['password'].value,
    };
    this.apiClient
      .login(request)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: AuthResponse) => {
          this.tokenService.saveToken(res.token);
          this.toggleBlockUI(false);
          this.router.navigate([HOME_URL]);
        },
        error: (error: any) => {
          this.notificationService.showError('Đăng nhập không đúng.');
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

  generateState() {
    function S4() {
      return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return (
      S4() +
      S4() +
      '-' +
      S4() +
      '-4' +
      S4().substr(0, 3) +
      '-' +
      S4() +
      '-' +
      S4() +
      S4() +
      S4()
    ).toLowerCase();
  }
  async loginGitlab() {
    var state = this.generateState();
    window.location.assign(
      `${authCodeFlowConfig.loginUrl}?client_id=${authCodeFlowConfig.clientId}&redirect_uri=${authCodeFlowConfig.redirectUri}&response_type=code&state=${state}&scope=${authCodeFlowConfig.scope}`
    );
  }

  authenUrl() {
    var query = {
      code: this.code,
    };
  }
}
