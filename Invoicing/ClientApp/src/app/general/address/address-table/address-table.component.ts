import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { Address } from '../../../common/model/address.model';
import { AddressService } from '../../../common/service/address.service';

@Component({
  selector: 'app-address-table',
  templateUrl: './address-table.component.html',
  styleUrls: ['./address-table.component.scss']
})
export class AddressTableComponent extends BaseComponent implements OnInit {

  public addresses: Address[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<Address> = new MatTableDataSource(undefined);

  constructor(
    private _addressService: AddressService,
    private _router: Router,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<Address>(this._addressService);
    this.refresh();
  }

  applyFilter(filterValue: Event) {
    if (filterValue != null) {
      this.dataSource.filter = (filterValue.target as HTMLInputElement).value.trim().toLowerCase();
    }
  }

  refresh() {
    this._addressService.getAddresses$().subscribe(
      response => {
        this.addresses = response;
        this.dataSource = new MatTableDataSource(this.addresses);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }
    )
  }

  onNavigateRow(row: any) {
    this._router.navigate(['/address/edit/' + row.id]);
  }

}
