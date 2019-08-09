import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit
{
    name: string;
    
    constructor(private router: Router, private authService: AuthenticationService) { }
    
    ngOnInit() {
        let jwtToken = localStorage.getItem('livetogether-user-token');

        if(jwtToken) {
            let userData = JSON.parse(jwtToken);
            this.name = userData.name;
        }
    }

    signOut() {
        this.authService.logout();
        this.router.navigate(['/login']);
    }
}
