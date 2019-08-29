import { Component, OnInit, OnDestroy } from '@angular/core';

import { WarrantiesService } from 'src/app/_services/warranties.service';
import { Warranty } from 'src/app/_models/warranty';
import { Subject } from 'rxjs';

import * as moment from 'moment';


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

    constructor(private _warrantiesService: WarrantiesService) {}
    
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
        return moment(expirationDate).isBefore(moment().startOf('day'));
    }

    isWarrantyDueIn(expirationDate: string): boolean {
        return moment(expirationDate).isSameOrBefore(moment().startOf('day').add(1, 'M'));
    }

    calculateWarrantyDueIn(expirationDate: string): string {
        let expDate = moment(expirationDate);
        let nowDate = moment().startOf('day');

        let diffWeeks = expDate.diff(nowDate, 'weeks') + 1;

        if(diffWeeks > 1) {
            return `${diffWeeks} weeks`;
        }

        let diffDays = expDate.diff(nowDate, 'days') + 1;

        return diffDays + (diffDays === 1 ? ' day' : ' days');
    }
}
