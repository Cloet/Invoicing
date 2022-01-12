import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-article-dialog',
  templateUrl: './delete-article-dialog.component.html',
  styleUrls: ['./delete-article-dialog.component.scss']
})
export class DeleteArticleDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteArticleDialogComponent>) { }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
