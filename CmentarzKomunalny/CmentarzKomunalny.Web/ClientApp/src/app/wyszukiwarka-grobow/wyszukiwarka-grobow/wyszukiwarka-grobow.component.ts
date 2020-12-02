import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

export interface PeriodicElement {
  imie: string;
  nrkwatery: number;
  datazgonu: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  { nrkwatery: 123456, imie: 'Jan Nowak', datazgonu: '12.12.1997' },
  { nrkwatery: 323142, imie: 'Aneta Nowak', datazgonu: '12.12.1999' },
  { nrkwatery: 423512, imie: 'Kamil Grodzki', datazgonu: '12.12.2020' },
  { nrkwatery: 412678, imie: 'Anna Kowalska', datazgonu: '12.10.1993' },
  { nrkwatery: 412678, imie: 'Jan Kowalski', datazgonu: '12.8.2020' },
  { nrkwatery: 936492, imie: 'Krystian Noga', datazgonu: '12.12.2017' },
  { nrkwatery: 712341, imie: 'Piotr Dworski', datazgonu: '12.12.1990' },
  { nrkwatery: 832523, imie: 'Anna Dworska', datazgonu: '2.2.1997' },
  { nrkwatery: 932214, imie: 'Krystian Nowak', datazgonu: '10.12.1997' },
  { nrkwatery: 101231, imie: 'Jan Krojec', datazgonu: '12.12.1998' },
];

@Component({
  selector: 'app-wyszukiwarka-grobow',
  templateUrl: './wyszukiwarka-grobow.component.html',
  styleUrls: ['./wyszukiwarka-grobow.component.css']
})
export class WyszukiwarkaGrobowComponent implements OnInit {
  displayedColumns: string[] = ['nrkwatery', 'imie', 'datazgonu'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  constructor() { }

  ngOnInit() {

  }

}
