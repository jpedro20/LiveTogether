import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WarrantiesListComponent } from './warranties-list.component';

describe('WarrantiesListComponent', () => {
  let component: WarrantiesListComponent;
  let fixture: ComponentFixture<WarrantiesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WarrantiesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarrantiesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
