import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-country-dialog',
  templateUrl: './delete-country-dialog.component.html',
  styleUrls: ['./delete-country-dialog.component.scss']
})
export class DeleteCountryDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteCountryDialogComponent>) { }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
