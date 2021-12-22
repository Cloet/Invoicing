import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Country } from '../../common/model/country.model';
import { CountryService } from '../../common/service/country.service';
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'

@Component({
  selector: 'app-view-country',
  templateUrl: './view-country.component.html',
  styleUrls: ['./view-country.component.scss']
})
export class ViewCountryComponent implements OnInit {

  public countries : Country[] = [];
  @ViewChild(MatPaginator, { static: false })
    paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false })
    sort!: MatSort;
  dataSource: MatTableDataSource<Country> = new MatTableDataSource(undefined);
  country: Country | undefined;

  displayedColumns : string[] = ['id', 'country', 'name', 'action'];

  constructor(private _countryService: CountryService) { }

  ngOnInit(): void {
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
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }
     )
  }

}
