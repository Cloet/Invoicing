import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditVatComponent } from './add-edit-vat.component';

describe('AddEditVatComponent', () => {
  let component: AddEditVatComponent;
  let fixture: ComponentFixture<AddEditVatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditVatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditVatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
