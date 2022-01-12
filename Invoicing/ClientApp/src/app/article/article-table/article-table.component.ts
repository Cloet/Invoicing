import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BaseComponent } from '../../common/base.component';
import { Article } from '../../common/model/article.model';
import { ArticleService } from '../../common/service/article.service';
import { DeleteArticleDialogComponent } from '../delete-article-dialog/delete-article-dialog.component';

@Component({
  selector: 'app-article-table',
  templateUrl: './article-table.component.html',
  styleUrls: ['./article-table.component.scss']
})
export class ArticleTableComponent extends BaseComponent implements OnInit {

  public articles: Article[] = []
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<Article> = new MatTableDataSource(undefined);
  isLoading: boolean = true;

  displayedColumns: string[] = ['id', 'article', 'description', 'unitprice', 'vat','unitpriceInclVat', 'action']

  constructor(
    private _articleService: ArticleService,
    private _router: Router,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar
  ) {
      super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<Article>(this._articleService);
    this.refresh();
  }

  applyFilter(filterValue: Event) {
    if (filterValue != null) {
      this.dataSource.filter = (filterValue.target as HTMLInputElement).value.trim().toLowerCase();
    }
  }

  refresh() {
    this.isLoading = true;
    this._articleService.getArticles$().subscribe(
      response => {
        this.isLoading = false;
        this.articles = response;
        this.dataSource = new MatTableDataSource(this.articles);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }
    );
  }

  onNavigateRow(row: Article) {
    this._router.navigate(['/article/edit/' + row.id]);
  }

 // TEST
  onClickDeleteArticle(row: Article) {
    this._articleService.getArticleForId$(row.id).subscribe(
      val => {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.autoFocus = true;
        const dialogRef = this._dialog.open(DeleteArticleDialogComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this._articleService.deleteArticle(val.id).subscribe(() => {
              this._snackbar.open("Article has been deleted.", 'ok', { duration: 2000 });
            });
          }
        });
      }
    )
  }

}
