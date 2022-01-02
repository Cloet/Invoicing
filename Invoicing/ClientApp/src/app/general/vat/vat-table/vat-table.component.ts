import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { VAT } from '../../../common/model/vat.model';
import { VATService } from '../../../common/service/vat.service';
import { DeleteVatDialogComponent } from '../delete-vat-dialog/delete-vat-dialog.component';

@Component({
  selector: 'app-vat-table',
  templateUrl: './vat-table.component.html',
  styleUrls: ['./vat-table.component.scss']
})
export class VatTableComponent extends BaseComponent implements OnInit {

  public vats: VAT[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<VAT> = new MatTableDataSource(undefined);
  vat!: VAT;

  displayedColumns: string[] = ['id', 'code','description', 'percentage', 'action'];

  constructor(
    private _vatService: VATService,
    private _router: Router,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<VAT>(this._vatService);

    this.refresh();
  }

  applyFilter(filterValue: Event) {
    if (filterValue != null) {
      this.dataSource.filter = (filterValue.target as HTMLInputElement).value.trim().toLowerCase();
    }
  }

  refresh() {
    this._vatService.getVAT$().subscribe(
      response => {
        this.vats = response;
        this.dataSource = new MatTableDataSource(this.vats);
        setTimeout(() => this.dataSource.sort = this.sort);
        setTimeout(() => this.dataSource.paginator = this.paginator);
      }
    );
  }

  onNavigateRow(row: any) {
    this._router.navigate(['/vat/edit-vat/' + row.id]);
  }

  onClickDeleteVAT(row: any) {
    this._vatService.getVATForId$(row.id).subscribe(val => {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.autoFocus = true;
      const dialogRef = this._dialog.open(DeleteVatDialogComponent, dialogConfig);

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this._vatService.deleteVAT(val.id).subscribe(() => {
            this._snackbar.open("VAT has been deleted", 'ok', { duration: 2000 });
          });
        }
      });
    });
  }


}
