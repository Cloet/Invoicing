import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VatDisplayScreenComponent } from './vat-display-screen.component';

describe('VatDisplayScreenComponent', () => {
  let component: VatDisplayScreenComponent;
  let fixture: ComponentFixture<VatDisplayScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VatDisplayScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VatDisplayScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
