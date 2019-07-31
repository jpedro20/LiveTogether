import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from "rxjs/operators";

import { AuthenticationService } from 'src/app/_services/authentication.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit
{
    loginForm: FormGroup;
    submitted: boolean = false;
    loading: boolean = false;
    returnUrl: string;
    error: string;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService
    ) { }
    
    ngOnInit() { 
        this.authenticationService.logout();

        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });

        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    onSubmit() {
        this.submitted = true;

        if(this.loginForm.invalid) {
            return;
        }

        let username = this.loginForm.controls.username.value;
        let password = this.loginForm.controls.password.value;

        this.loading = true;
        this.authenticationService.login(username, password)
            .pipe(first())
            .subscribe(
                () => {
                    this.router.navigate([this.returnUrl]);
                },
                (error: { status: number; }) => {
                    this.loading = false;
                    
                    this.error = error.status === 401 ?
                        "Invalid username and password. Please check your credentials." :
                        "An unexpected error occurred. Please try again.";
                }
            )
    }
}