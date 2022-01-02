import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../common/base.component';
import { City } from '../../../common/model/city.model';
import { CityService } from '../../../common/service/city.service';
import { ErrorDialogComponent } from '../../../error-dialog/error-dialog.component';
import { DeleteCityDialogComponent } from '../delete-city-dialog/delete-city-dialog.component';

@Component({
  selector: 'app-city-table',
  templateUrl: './city-table.component.html',
  styleUrls: ['./city-table.component.scss']
})
export class CityTableComponent extends BaseComponent implements OnInit {

  public cities: City[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<City> = new MatTableDataSource(undefined);
  city!: City;

  displayedColumns: string[] = ['id', 'city', 'postal', 'country', 'country name', 'mainmuncipality', 'action'];

  constructor(
    private _cityService: CityService
    ,private _router: Router
    ,private _dialog: MatDialog,
    private _snackbar: MatSnackBar  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<City>(this._cityService);
    this.refresh();
  }

  applyFilter(filterValue: Event) {
    if (filterValue != null) {
      this.dataSource.filter = (filterValue.target as HTMLInputElement).value.trim().toLowerCase();
    }
  }

  refresh() {
    this._cityService.getCities$().subscribe(
      response => {
        this.cities = response;
        this.dataSource = new MatTableDataSource(this.cities);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.dataSource.filterPredicate = (data: City, filter: string) => {
          return (data.country != null && (data.country.name.toLocaleLowerCase().includes(filter) || data.country.countryCode.toLocaleLowerCase().includes(filter)))
            || data.name.toLocaleLowerCase().includes(filter) || data.postal.toLocaleLowerCase().includes(filter);
        };
      }
    )
  }

  onNavigateRow(row: any) {
    this._router.navigate(['/city/edit-city/' + row.id]);
  }

  onClickDeleteCity(row: any) {
    this._cityService.getCityForId$(row.id).subscribe(
      val => {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.autoFocus = true;
        const dialogRef = this._dialog.open(DeleteCityDialogComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
          result => {
            if (result) {
              this._cityService.deleteCity(val.id).subscribe(res => {
                this._snackbar.open("City has been deleted.", 'ok', { duration: 2000 });
              });
            }
          }
        )
      }
    );
  }

}
