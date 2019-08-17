import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { DataTablesModule } from 'angular-datatables';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';

import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './_layout/pages/pagenotfound/pagenotfound.component';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './_layout/header/header.component';
import { NavigationComponent } from './_layout/navigation/navigation.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { WarrantiesListComponent } from './warranties/warranties-list/warranties-list.component';


@NgModule({
    declarations: [
        AppComponent,
        PageNotFoundComponent,
        LoginComponent,
        HomeComponent,
        HeaderComponent,
        NavigationComponent,
        DashboardComponent,
        WarrantiesListComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        ReactiveFormsModule,
        DataTablesModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
