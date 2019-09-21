import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { Environment } from '../_config/environment';

@Injectable({ providedIn: 'root' })
export class AuthenticationService
{
    constructor(private http: HttpClient, private environment: Environment) {}

    login(username: string, password: string) {
        return this.http.post<any>(this.environment.apiUrl + '/auth/login', { username, password })
            .pipe(map(user => {
                if(user && user.token) {
                    localStorage.setItem('livetogether-user-token', JSON.stringify(user));
                }

                return user;
            }));
    }

    logout() {
        localStorage.removeItem('livetogether-user-token');
    }
}