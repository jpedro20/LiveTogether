import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Warranty } from '../_models/warranty';
import { Environment } from '../_config/environment';


@Injectable({ providedIn: 'root' })
export class WarrantiesService
{
    constructor(private http: HttpClient, private environment: Environment) {}

    getWarranties() : Observable<any[]> {
        return this.http.get<Warranty[]>(this.environment.apiUrl + '/warranties');
    }
}