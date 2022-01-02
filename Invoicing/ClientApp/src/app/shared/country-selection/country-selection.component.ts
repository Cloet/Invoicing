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
import { Location } from '@angular/common';
import { BaseComponent } from '../../common/base.component';

@Component({
  selector: 'app-country-selection',
  templateUrl: './country-selection.component.html',
  styleUrls: ['./country-selection.component.scss']
})
export class CountrySelectionComponent extends BaseComponent implements OnInit {

  public countries: Country[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<Country> = new MatTableDataSource(undefined);
  country!: Country;
  selection!: SelectionModel<Country>;
  @Output() countryEvent = new EventEmitter<Country>();

  displayedColumns: string[] = ['id', 'country', 'name'];

  constructor(
    private _countryService: CountryService,
    private _dialog: MatDialog) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<Country>(this._countryService);
    this.refresh();
  }

  applyFilter(filterValue: Event) {
    if (filterValue != null) {
      this.dataSource.filter = (filterValue.target as HTMLInputElement).value.trim().toLowerCase();
    }
  }

  refresh() {
    this._countryService.getCountries$().subscribe(
      response => {
        this.countries = response;
        this.dataSource = new MatTableDataSource(this.countries);
        this.selection = new SelectionModel<Country>(false, []);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }
    );
  }

  ondblClick(row: any) {
    this.selection.toggle(row);
    this.onConfirm();
  }

  onCancel() {
    this.countryEvent.emit(undefined);
  }

  onConfirm() {
    const country = this.selection.selected[0];
    this.countryEvent.emit(country);
  }
  
}
