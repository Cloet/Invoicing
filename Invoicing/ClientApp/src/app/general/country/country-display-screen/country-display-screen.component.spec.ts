import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CountryDisplayScreenComponent } from './country-display-screen.component';

describe('CountryDisplayScreenComponent', () => {
  let component: CountryDisplayScreenComponent;
  let fixture: ComponentFixture<CountryDisplayScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CountryDisplayScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryDisplayScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
