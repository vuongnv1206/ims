import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

import { TokenService } from './services/token.service';
import { ACCESS_DENIED, LOGIN_URL } from './constants/url.const';

@Injectable()
export class AuthGuard {
    constructor(private router: Router,
        private tokenService: TokenService) {

    }
    canActivate(activateRoute: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): boolean {
        let requiredPolicy = activateRoute.data["requiredPolicy"] as string;
        var loggedInUser = this.tokenService.getUser();
        if (loggedInUser) {
            var listPermission = JSON.parse(loggedInUser.permissions);
            if (listPermission != null && listPermission != '' && listPermission.filter(x => x == requiredPolicy).length > 0)
                return true;
            else {
                this.router.navigate([ACCESS_DENIED], {
                    queryParams: {
                        returnUrl: routerState.url
                    }
                });
                return false;
            }
        }
        else {
            this.router.navigate([LOGIN_URL], {
                queryParams: {
                    returnUrl: routerState.url
                }
            });
            return false;
        }
    }
}
