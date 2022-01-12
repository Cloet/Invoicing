import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BaseComponent } from '../../common/base.component';
import { Customer } from '../../common/model/customer.model';
import { CustomerService } from '../../common/service/customer.service';
import { DeleteCustomerDialogComponent } from '../delete-customer-dialog/delete-customer-dialog.component';

@Component({
  selector: 'app-customer-table',
  templateUrl: './customer-table.component.html',
  styleUrls: ['./customer-table.component.scss']
})
export class CustomerTableComponent extends BaseComponent implements OnInit {

  customers: Customer[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<Customer> = new MatTableDataSource(undefined);
  isLoading: boolean = false;

  displayedColumns: string[] = ['id', 'code', 'name', 'telephone', 'mobile', 'email', 'action']

  constructor(
    private _customerService: CustomerService,
    private _router: Router,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<Customer>(this._customerService);
    this.refresh();
  }

  refresh() {
    this.isLoading = true;
    this._customerService.getCustomers$().subscribe(
      response => {
        this.isLoading = false;
        this.customers = response;
        this.dataSource = new MatTableDataSource(this.customers);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    )
  }

  onNavigateRow(row: any) {
    this._router.navigate(['/customer/edit/' + row.id]);
  }

  onClickDeleteCustomer(row: any) {
    this._customerService.getCustomerForId$(row.id).subscribe(
      val => {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.autoFocus = true;
        const dialogRef = this._dialog.open(DeleteCustomerDialogComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
          result => {
            if (result) {
              this._customerService.deleteCustomer(val.id).subscribe(res => {
                this._snackbar.open("Customer has been deleted.", "ok", { duration: 2000 });
              })
            }
          }
        )
      }
    )
  }

}
