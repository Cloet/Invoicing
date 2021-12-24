import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Country } from '../../common/model/country.model';
import { CountryService } from '../../common/service/country.service';
import { ErrorDialogComponent } from '../../error-dialog/error-dialog.component';

@Component({
  selector: 'app-country-selection',
  templateUrl: './country-selection.component.html',
  styleUrls: ['./country-selection.component.scss']
})
export class CountrySelectionComponent implements OnInit {

  public countries: Country[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<Country> = new MatTableDataSource(undefined);
  country!: Country;
  selection!: SelectionModel<Country>;
  isLoading: boolean = true;
  @Output() countryEvent = new EventEmitter<Country>();

  displayedColumns: string[] = ['id', 'country', 'name'];

  constructor(
    private _countryService: CountryService,
    private _router: Router,
    private _dialog: MatDialog,
    private _activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.refresh();

    this._countryService.loadingError$.subscribe(
      err => {
        this.showErrorDialog(err);
      }
    )

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
        this.selection = new SelectionModel<Country>(false, []);
        setTimeout(() => this.dataSource.sort = this.sort);
        setTimeout(() => this.dataSource.paginator = this.paginator);
      }
    );
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

  ondblClick(row: any) {
    this.selection.toggle(row);
    this.onConfirm();
  }

  onConfirm() {
    this.countryEvent.emit(this.selection.selected[0]);
  }
  
}
