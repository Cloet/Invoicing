import { SelectionModel } from '@angular/cdk/collections';
import { EventEmitter, Output, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { BaseComponent } from '../../common/base.component';
import { VAT } from '../../common/model/vat.model';
import { VATService } from '../../common/service/vat.service';

@Component({
  selector: 'app-vat-selection',
  templateUrl: './vat-selection.component.html',
  styleUrls: ['./vat-selection.component.scss']
})
export class VatSelectionComponent extends BaseComponent implements OnInit {

  public vats: VAT[] = [];
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  dataSource: MatTableDataSource<VAT> = new MatTableDataSource(undefined);
  vat!: VAT;
  selection!: SelectionModel<VAT>;
  @Output() vatEvent = new EventEmitter<VAT>();

  displayedColumns: string[] = ['id', 'code', 'description', 'percentage'];

  constructor(
    private _vatService: VATService,
    private _dialog: MatDialog
  ) {
    super(_dialog);
  }

  ngOnInit(): void {
    this.subscribeToErrors<VAT>(this._vatService);
    this.refresh();
  }

  refresh() {
    this._vatService.getVAT$().subscribe(
      response => {
        this.vats = response;
        this.dataSource = new MatTableDataSource(this.vats);
        this.selection = new SelectionModel<VAT>(false, []);
        setTimeout(() => this.dataSource.sort = this.sort);
        setTimeout(() => this.dataSource.paginator = this.paginator);
      }
    )
  }

  ondblClick(row: any) {
    this.selection.toggle(row);
    this.onConfirm();
  }

  onCancel() {
    this.vatEvent.emit(undefined);
  }

  onConfirm() {
    const vat = this.selection.selected[0];
    this.vatEvent.emit(vat);
  }

}
