import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './_guards/auth.guard';

import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { WarrantiesListComponent } from './warranties/warranties-list/warranties-list.component';

import { PageNotFoundComponent } from './_layout/pages/pagenotfound/pagenotfound.component';


const routesHome: Routes = [
    { path: 'dashboard', component: DashboardComponent },
    { path: 'warranties', component: WarrantiesListComponent },
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
];


const routes: Routes = [
    { path: '', component: HomeComponent, children: routesHome, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: '**', component: PageNotFoundComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
