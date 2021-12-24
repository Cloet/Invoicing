import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-city-dialog',
  templateUrl: './delete-city-dialog.component.html',
  styleUrls: ['./delete-city-dialog.component.scss']
})
export class DeleteCityDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteCityDialogComponent>) { }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
