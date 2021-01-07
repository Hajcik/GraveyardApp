import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';
import { SharedService } from '../shared.service';

export interface Articles {
  Id: number;
  Title: string;
  DateOfPublication: string;
  NewsContent: string;
}

export interface Necrologs {
  Id: number;
  Name: string;
  ObituaryContent: string;
  DateOfDeath_Obituary: string;
}

export interface Workers {
  Id: string;
  UserName: string;
  Email: string;
}

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  // fb: FormBuilder
  constructor(fb: FormBuilder, private service: SharedService) { }

  refreshAktualnosciList() {
    this.service.getAktualnosciList().subscribe(res => {
      this.dataSourceAktualnosci.data = res as Articles[];
    });
  }

  refreshNekrologiList() {
    this.service.getNekrologiList().subscribe(res => {
      this.dataSourceNekrologi.data = res as Necrologs[];
    });
  }

  refreshPracownicyList() {
    this.service.getPracownicyList().subscribe(res => {
      this.dataSourcePracownicy.data = res as Workers[];
    });
  }

  //TABELA AKTUALNOŚCI
  displayedColumnsAktualnosci: string[] = ['select', 'Id', 'Title', 'NewsContent', 'DateOfPublication'];
  dataSourceAktualnosci = new MatTableDataSource<Articles>();
  selectionAktualnosci = new SelectionModel<Articles>(true, []);

  //TABELA NEKROLOGI
  displayedColumnsNekrologi: string[] = ['select', 'Id', 'Name', 'ObituaryContent', 'DateOfDeath_Obituary'];
  dataSourceNekrologi = new MatTableDataSource<Necrologs>();
  selectionNekrologi = new SelectionModel<Necrologs>(true, []);

  //TABELA Pracownicy
  displayedColumnsPracownicy: string[] = ['select', 'Id', 'UserName', 'Email'];
  dataSourcePracownicy = new MatTableDataSource<Workers>();
  selectionPracownicy = new SelectionModel<Workers>(true, []);

  

  /*ModalTitle: string;
  //aktualnosc: any;
  ActivateAddEditAktualnoscComp: boolean = false;

  @Input() aktualnosc: any;
  NewsId: string;
  NewsTitle: string;
  NewsContent: string;
  NewsDateOfPublication: string;

  addClickAktualnosc() {
    this.aktualnosc = {
      Id: 0,
      Title: "",
      NewsContent: "",
      DateOfPublication: "",
    }
    this.ModalTitle = "Dodaj aktualnosc";
    this.ActivateAddEditAktualnoscComp = true;
  }

  closeAddClickAktualnosc() {
    this.ActivateAddEditAktualnoscComp = false;
    this.refreshAktualnosciList();
  }

  delClickAktualnosc() {

  }
  editClickAktualnosc(item) {
    this.aktualnosc = item;
    this.ModalTitle = "Edytuj aktualnosc";
    this.ActivateAddEditAktualnoscComp = true;
  }
  addClickNekrolog() {

  }
  delClickNekrolog() {

  }
  putClickNekrolog() {

  }
  */
  ngOnInit() {
    this.refreshAktualnosciList();
    this.refreshNekrologiList();
    this.refreshPracownicyList();

    // aktualnosc
//    this.NewsId = this.aktualnosc.Id;
//    this.NewsTitle = this.aktualnosc.NewsTitle;
//    this.NewsContent = this.aktualnosc.NewsContent;
//    this.NewsDateOfPublication = this.aktualnosc.NewsDateOfPublication;
  }


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
    return `${this.selectionAktualnosci.isSelected(row) ? 'deselect' : 'select'} row ${row.Id + 1}`;
  }
  checkboxLabelNekrologi(row?: Necrologs): string {
    if (!row) {
      return `${this.isAllSelectedNekrologi() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionNekrologi.isSelected(row) ? 'deselect' : 'select'} row ${row.Id + 1}`;
  }
  checkboxLabelPracownicy(row?: Workers): string {
    if (!row) {
      return `${this.isAllSelectedPracownicy() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionPracownicy.isSelected(row) ? 'deselect' : 'select'} row ${row.Id}`;
  }

}
