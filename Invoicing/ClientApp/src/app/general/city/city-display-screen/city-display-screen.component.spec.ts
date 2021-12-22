import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityDisplayScreenComponent } from './city-display-screen.component';

describe('CityDisplayScreenComponent', () => {
  let component: CityDisplayScreenComponent;
  let fixture: ComponentFixture<CityDisplayScreenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CityDisplayScreenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CityDisplayScreenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
