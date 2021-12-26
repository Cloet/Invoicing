import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteVatDialogComponent } from './delete-vat-dialog.component';

describe('DeleteVatDialogComponent', () => {
  let component: DeleteVatDialogComponent;
  let fixture: ComponentFixture<DeleteVatDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteVatDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteVatDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
