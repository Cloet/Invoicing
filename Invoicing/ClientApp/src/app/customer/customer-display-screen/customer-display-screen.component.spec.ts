import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerDisplayScreenComponent } from './customer-display-screen.component';

describe('CustomerDisplayScreenComponent', () => {
  let component: CustomerDisplayScreenComponent;
  let fixture: ComponentFixture<CustomerDisplayScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerDisplayScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerDisplayScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
