import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VatSelectionComponent } from './vat-selection.component';

describe('VatSelectionComponent', () => {
  let component: VatSelectionComponent;
  let fixture: ComponentFixture<VatSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VatSelectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VatSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
