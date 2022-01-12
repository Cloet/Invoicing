import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressDisplayScreenComponent } from './address-display-screen.component';

describe('AddressDisplayScreenComponent', () => {
  let component: AddressDisplayScreenComponent;
  let fixture: ComponentFixture<AddressDisplayScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddressDisplayScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressDisplayScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
