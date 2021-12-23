import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { City } from '../../../common/model/city.model';
import { CityService } from '../../../common/service/city.service';
import { ErrorDialogComponent } from '../../../error-dialog/error-dialog.component';

@Component({
  selector: 'app-city-table',
  templateUrl: './city-table.component.html',
  styleUrls: ['./city-table.component.scss']
})
export class CityTableComponent implements OnInit {

  public cities: City[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<City> = new MatTableDataSource(undefined);
  city!: City;

  isLoading: boolean = true;

  displayedColumns: string[] = ['id', 'city', 'postal', 'country', 'country name', 'action'];

  constructor(
    private _cityService: CityService
    ,private _router: Router
    ,private _dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.refresh();

    this._cityService.loadingError$.subscribe(
      err => {
        this.showErrorDialog(err);
      }
    )

  }

  showErrorDialog(errors: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      title: 'Error',
      errors
    };
    const dialogRef = this._dialog.open(ErrorDialogComponent, dialogConfig);

    return dialogRef.afterClosed();
  }

  applyFilter(filterValue: Event) {
    if (filterValue != null) {
      this.dataSource.filter = (filterValue.target as HTMLInputElement).value.trim().toLowerCase();
    }
  }

  refresh() {
    this._cityService.getCities$().subscribe();
    this._cityService.getCitiesArray$().subscribe(
      response => {
        this.cities = response;
        this.isLoading = false;
        this.dataSource = new MatTableDataSource(this.cities);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }
    )
  }

  onNavigateRow(row: any) {
    this._router.navigate(['/city/edit-city' + row.id]);
  }

  onClickDeleteCity(row: any) {

  }

  deleteCity() {

  }

}
