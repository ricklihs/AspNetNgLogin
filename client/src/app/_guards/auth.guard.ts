import { Injectable } from '@angular/core';
import { 
    Router,
     CanActivate, 
     ActivatedRouteSnapshot, 
     RouterStateSnapshot 
    } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
    // 1)
    constructor(private router: Router) {}
    // 2)
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        // (1)
        if (localStorage.getItem('currentUser')) {
            // logged in so return true
            return true;
        }

        // (2) not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
