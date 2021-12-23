import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DeleteCountryDialogComponent } from './delete-country-dialog.component';

describe('CountryDeleteDialogComponent', () => {
  let component: DeleteCountryDialogComponent;
  let fixture: ComponentFixture<DeleteCountryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteCountryDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteCountryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
