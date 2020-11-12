import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {MatTableDataSource} from '@angular/material/table';
import {SelectionModel} from '@angular/cdk/collections';

export interface PeriodicElement {
  position: number;
  name: string;
  number: number;
  date: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Jan Kowalski', number: 1, date: '12-12-2020'},
  {position: 2, name: 'Adam Nowak', number: 4, date: '13-12-2020'},
  {position: 3, name: 'Kamil Pawlak', number: 6, date: '14-12-2020'},
  {position: 4, name: 'Andrzej Dudowski', number: 9, date: '15-12-2020'},
  {position: 5, name: 'Janina Nowak', number: 10, date: '16-12-2020'},
  {position: 6, name: 'Karolina Kowalska', number: 12, date: '17-12-2020'},
  {position: 7, name: 'Julia Pawlak', number: 14, date: '18-12-2020'},
  {position: 8, name: 'Krystyna Dudowska', number: 15, date: '19-12-2020'},
];

@Component({
  selector: 'app-wyszukiwarka-grobow',
  templateUrl: './wyszukiwarka-grobow.component.html',
  styleUrls: ['./wyszukiwarka-grobow.component.css']
})
export class WyszukiwarkaGrobowComponent implements OnInit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  isEditable = true;

  displayedColumns: string[] = ['select', 'name', 'number', 'date'];
  dataSource = new MatTableDataSource<PeriodicElement>(ELEMENT_DATA);
selection = new SelectionModel<PeriodicElement>(true, []);
/** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: PeriodicElement): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }
  constructor(private _formBuilder: FormBuilder) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }

}
