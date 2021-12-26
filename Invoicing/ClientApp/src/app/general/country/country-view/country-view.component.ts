import { Component, OnInit, ViewChild } from '@angular/core';
import { Country } from '../../../common/model/country.model';
import { CountryService } from '../../../common/service/country.service';
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../../../error-dialog/error-dialog.component';
import { Router } from '@angular/router';
import { DeleteCountryDialogComponent } from '../delete-country-dialog/delete-country-dialog.component';
import { BaseComponent } from '../../../common/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-country-view',
  templateUrl: './country-view.component.html',
  styleUrls: ['./country-view.component.scss']
})
export class CountryViewComponent extends BaseComponent implements OnInit {

  public countries : Country[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<Country> = new MatTableDataSource(undefined);
  country!: Country;
  isLoading: boolean = true;

  displayedColumns : string[] = ['id', 'country', 'name', 'action'];

  constructor(
    private _countryService: CountryService,
    private _router: Router,
    private _dialog: MatDialog,
    private _snackbar: MatSnackBar
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<Country>(this._countryService);

    this.isLoading = true;
    this.refresh();
  }

  applyFilter(filterValue: Event) {
    if (filterValue != null) {
      this.dataSource.filter = (filterValue.target as HTMLInputElement).value.trim().toLowerCase();
    }
  }

  refresh() {
    this._countryService.getCountries$().subscribe();
    this._countryService.getCountriesArray$().subscribe(
      response => {
        this.isLoading = false;
        this.countries = response;
        this.dataSource = new MatTableDataSource(this.countries);
        setTimeout(() => this.dataSource.sort = this.sort);
        setTimeout(() => this.dataSource.paginator = this.paginator);
      }
    );
  }

  onNavigateRow(row: any) {
    this._router.navigate(['/country/edit-country/' + row.id]);
  }

  onClickDeleteCountry(row: any) {

    this._countryService.getCountryForId$(row.id).subscribe(val => { this.country = val; });

    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    const dialogRef = this._dialog.open(DeleteCountryDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteCountry();
      }
    });
  }

  deleteCountry() {
    this._countryService.deleteCountry(this.country.id).subscribe(chk => {
      this._snackbar.open("Country has been deleted.", 'ok', { duration: 2000 });
    });
  }

}
