import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Warranty } from '../_models/warranty';


@Injectable({ providedIn: 'root' })
export class WarrantiesService
{
    constructor(private http: HttpClient) {}

    getWarranties() : Observable<any[]> {
        return this.http.get<Warranty[]>('/api/warranties');
    }
}