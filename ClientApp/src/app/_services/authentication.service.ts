import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthenticationService
{
    constructor(private http: HttpClient) {}

    login(username: string, password: string) {
        return this.http.post<any>('/api/auth/login', { username, password })
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