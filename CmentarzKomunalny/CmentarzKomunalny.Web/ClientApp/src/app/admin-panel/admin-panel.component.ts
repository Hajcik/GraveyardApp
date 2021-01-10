import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';

export interface Articles {
  tytul: string;
  position: number;
  tresc: string;
  data: string;
}

export interface Necrologs {
  tytul: string;
  position: number;
  tresc: string;
  data: string;
}

export interface Workers {
  position: number;
  name: string;
  surname: string;
}

const ARTICLES_DATA: Articles[] = [
  { position: 1, tytul: 'Hydrogen', tresc: "1.0079", data: '12/12/2020' },
  { position: 2, tytul: 'Helium', tresc: "4.0026", data: '12/12/2020' },
  { position: 3, tytul: 'Lithium', tresc: "6.941", data: '12/12/2020' },
  { position: 4, tytul: 'Beryllium', tresc: "9.0122", data: '12/12/2020' },
  { position: 5, tytul: 'Boron', tresc: "10.811", data: '12/12/2020' },
  { position: 6, tytul: 'Carbon', tresc: "12.0107", data: '12/12/2020' },
  { position: 7, tytul: 'Nitrogen', tresc: "14.0067", data: '12/12/2020' },
  { position: 8, tytul: 'Oxygen', tresc: "15.9994", data: '12/12/2020' },
  { position: 9, tytul: 'Fluorine', tresc: "18.9984", data: '12/12/2020' },
  { position: 10, tytul: 'Neon', tresc: "20.1797", data: '12/12/2020' },
];

const NECROLOGS_DATA: Necrologs[] = [
  { position: 1, tytul: 'Hydrogen', tresc: "1.0079", data: '12/12/2020' },
  { position: 2, tytul: 'Helium', tresc: "4.0026", data: '12/12/2020' },
  { position: 3, tytul: 'Lithium', tresc: "6.941", data: '12/12/2020' },
  { position: 4, tytul: 'Beryllium', tresc: "9.0122", data: '12/12/2020' },
  { position: 5, tytul: 'Boron', tresc: "10.811", data: '12/12/2020' },
  { position: 6, tytul: 'Carbon', tresc: "12.0107", data: '12/12/2020' },
  { position: 7, tytul: 'Nitrogen', tresc: "14.0067", data: '12/12/2020' },
  { position: 8, tytul: 'Oxygen', tresc: "15.9994", data: '12/12/2020' },
  { position: 9, tytul: 'Fluorine', tresc: "18.9984", data: '12/12/2020' },
  { position: 10, tytul: 'Neon', tresc: "20.1797", data: '12/12/2020' },
];

const WORKERS_DATA: Workers[] = [
  { position: 1, name: 'Hydrogen', surname: 'H' },
  { position: 2, name: 'Helium', surname: 'He' },
  { position: 3, name: 'Lithium', surname: 'Li' },
  { position: 4, name: 'Beryllium', surname: 'Be' },
  { position: 5, name: 'Boron', surname: 'B' },
  { position: 6, name: 'Carbon', surname: 'C' },
  { position: 7, name: 'Nitrogen', surname: 'N' },
  { position: 8, name: 'Oxygen', surname: 'O' },
  { position: 9, name: 'Fluorine', surname: 'F' },
  { position: 10, name: 'Neon', surname: 'Ne' },
];

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {


  constructor(fb: FormBuilder) { }

  ngOnInit() {
  }

  //TABELA AKTUALNOŚCI
  displayedColumnsAktualnosci: string[] = ['select', 'position', 'tytul', 'tresc', 'data'];
  dataSourceAktualnosci = new MatTableDataSource<Articles>(ARTICLES_DATA);
  selectionAktualnosci = new SelectionModel<Articles>(true, []);

  //TABELA NEKROLOGI
  displayedColumnsNekrologi: string[] = ['select', 'position', 'tytul', 'tresc', 'data'];
  dataSourceNekrologi = new MatTableDataSource<Necrologs>(NECROLOGS_DATA);
  selectionNekrologi = new SelectionModel<Necrologs>(true, []);

  //TABELA NEKROLOGI
  displayedColumnsPracownicy: string[] = ['select', 'position', 'name', 'surname'];
  dataSourcePracownicy = new MatTableDataSource<Workers>(WORKERS_DATA);
  selectionPracownicy = new SelectionModel<Workers>(true, []);

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelectedAktualnosci() {
    const numSelectedAktualnosci = this.selectionAktualnosci.selected.length;
    const numRowsAktualnosci = this.dataSourceAktualnosci.data.length;
    return numSelectedAktualnosci === numRowsAktualnosci;
  }
  isAllSelectedNekrologi() {
    const numSelectedNekrologi = this.selectionNekrologi.selected.length;
    const numRowsNekrologi = this.dataSourceNekrologi.data.length;
    return numSelectedNekrologi === numRowsNekrologi;
  }
  isAllSelectedPracownicy() {
    const numSelectedPracownicy = this.selectionPracownicy.selected.length;
    const numRowsPracownicy = this.dataSourcePracownicy.data.length;
    return numSelectedPracownicy === numRowsPracownicy;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggleAktualnosci() {
    this.isAllSelectedAktualnosci() ?
      this.selectionAktualnosci.clear() :
      this.dataSourceAktualnosci.data.forEach(row => this.selectionAktualnosci.select(row));
  }
  masterToggleNekrologi() {
    this.isAllSelectedNekrologi() ?
      this.selectionNekrologi.clear() :
      this.dataSourceNekrologi.data.forEach(row => this.selectionNekrologi.select(row));
  }
  masterTogglePracownicy() {
    this.isAllSelectedPracownicy() ?
      this.selectionPracownicy.clear() :
      this.dataSourcePracownicy.data.forEach(row => this.selectionPracownicy.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabelAktualnosci(row?: Articles): string {
    if (!row) {
      return `${this.isAllSelectedAktualnosci() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionAktualnosci.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }
  checkboxLabelNekrologi(row?: Necrologs): string {
    if (!row) {
      return `${this.isAllSelectedNekrologi() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionNekrologi.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }
  checkboxLabelPracownicy(row?: Workers): string {
    if (!row) {
      return `${this.isAllSelectedPracownicy() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionPracownicy.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

}
