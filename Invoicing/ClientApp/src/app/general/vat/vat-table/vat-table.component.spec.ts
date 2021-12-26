import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VatTableComponent } from './vat-table.component';

describe('VatTableComponent', () => {
  let component: VatTableComponent;
  let fixture: ComponentFixture<VatTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VatTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VatTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
