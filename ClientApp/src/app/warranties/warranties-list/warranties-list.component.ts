import { Component, OnInit, OnDestroy } from '@angular/core';

import { WarrantiesService } from 'src/app/_services/warranties.service';
import { Warranty } from 'src/app/_models/warranty';
import { Subject } from 'rxjs';


@Component({
    selector: 'app-warranties-list',
    templateUrl: './warranties-list.component.html',
    styleUrls: ['./warranties-list.component.css']
})
export class WarrantiesListComponent implements OnInit, OnDestroy
{
    dtOptions: DataTables.Settings = {};
    dtTrigger: Subject<any> = new Subject();

    warranties: Warranty[] = [];
    currentDate: Date;
    nextMonthDate: Date;

    constructor(private _warrantiesService: WarrantiesService) {
        let now: string = new Date().toLocaleDateString();

        this.currentDate = new Date(now);

        this.nextMonthDate = new Date(now);
        this.nextMonthDate.setMonth(this.currentDate.getMonth() + 1);
    }
    

    ngOnInit(): void {
        this.dtOptions = {
            pageLength: 10,
            lengthChange: false,
            dom: 'l<"dataTables_filter_left"f>rtip',
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }]
        };

        this._warrantiesService
            .getWarranties()
            .subscribe((warranties: Warranty[]) => {
                this.warranties = warranties;

                this.dtTrigger.next();
            });
    }

    ngOnDestroy(): void {
        this.dtTrigger.unsubscribe();
    }

    isWarrantyExpired(expirationDate: string): boolean {
        return new Date(expirationDate).getTime() < this.currentDate.getTime();
    }

    isWarrantyDueIn(expirationDate: string): boolean {
        return new Date(expirationDate).getTime() < this.nextMonthDate.getTime();
    }

    calculateWarrantyDueIn(expirationDate: string): string {
        let expDate = new Date(expirationDate);
        let diffDays = Math.round((expDate.getTime() - this.currentDate.getTime())/(1000*60*60*24)) + 1;

        return diffDays + (diffDays === 1 ? ' day' : ' days');
    }
}
