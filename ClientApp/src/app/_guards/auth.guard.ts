import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate
{
    constructor(private router: Router) {}

    canActivate(_route: ActivatedRouteSnapshot, state: RouterStateSnapshot)
    {
        if(localStorage.getItem("livetogether-user-token"))
        {
            return true;
        }

        this.router.navigate(["login"], { queryParams: { returnUrl: state.url}});
        return false;
    }
}