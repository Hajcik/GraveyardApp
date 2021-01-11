import { Component, Input, OnInit, ViewChildren, QueryList } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
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

export interface DeadPerson {
  Id: number;
  Name: string;
  LodgingId: number;
  DateOfBirth: string;
  DateOfDeath: string;
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
  public aktualnosciDelete: any = [];
  public aktualnosciEdit: any = [];
  public nekrologDelete: any = [];
  public nekrologEdit: any = [];
  public pracownikTable: any = [];
  public ZmarliTable: any = [];
  public ZmarliEdit: any = [];


  @ViewChildren(MatPaginator) paginator = new QueryList<MatPaginator>();
  @ViewChildren(MatSort) sort = new QueryList<MatSort>();


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

  refreshZmarliList() {
    this.service.getZmarliList().subscribe(res => {
      this.dataSourceZmarli.data = res as DeadPerson[];
    });
  }

  public applyFilterPracownicy(value: Event) {
    const valueFilterPracownicy = (event.target as HTMLInputElement).value;
    this.dataSourcePracownicy.filter = valueFilterPracownicy.trim().toLowerCase();

    if (this.dataSourcePracownicy.paginator) {
      this.dataSourcePracownicy.paginator.firstPage();
    }
  }
  public applyFilterAktualnosci(value: Event) {
    const valueFilterAktualnosci = (event.target as HTMLInputElement).value;
    this.dataSourceAktualnosci.filter = valueFilterAktualnosci.trim().toLowerCase();

    if (this.dataSourceAktualnosci.paginator) {
      this.dataSourceAktualnosci.paginator.firstPage();
    }
  }
  public applyFilterNekrologi(value: Event) {
    const valueFilterNekrologi = (event.target as HTMLInputElement).value;
    this.dataSourceNekrologi.filter = valueFilterNekrologi.trim().toLowerCase();

    if (this.dataSourceNekrologi.paginator) {
      this.dataSourceNekrologi.paginator.firstPage();
    }
  }
  public applyFilterZmarli(value: Event) {
    const valueFilterZmarli = (event.target as HTMLInputElement).value;
    this.dataSourceZmarli.filter = valueFilterZmarli.trim().toLowerCase();

    if (this.dataSourceZmarli.paginator) {
      this.dataSourceZmarli.paginator.firstPage();
    }
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

  //TABELA Zmarli
  displayedColumnsZmarli: string[] = ['select', 'Id', 'Name', 'LodgingId', 'DateOfBirth', 'DateOfDeath'];
  dataSourceZmarli = new MatTableDataSource<DeadPerson>();
  selectionZmarli = new SelectionModel<DeadPerson>(true, []);

  //Dodaj "Aktualnosci"
  GetInputFromDodajaktualnosc() {
    //pobierz tytuł
    let inputDodajAktualnosciTytul = (document.getElementById("dodajAktualnoscTytul") as HTMLInputElement).value;
    //pobierz treść wiadomości
    let inputDodajAktualnosciTresc = (document.getElementById("dodajAktualnoscTresc") as HTMLTextAreaElement).value;
    //pobierz tytuł
    let inputDodajAktualnosciData = (document.getElementById("dodajAktualnoscData") as HTMLInputElement).value;


    //Sprawdzenie
    console.log(inputDodajAktualnosciTytul);
    console.log(inputDodajAktualnosciTresc);
    console.log(inputDodajAktualnosciData);
  }

  //Usun "Aktualnosci"
  GetCheckedUsunAktualnosci() {
    //Wczytanie wszystkich danych do tablicy
    this.aktualnosciDelete = this.selectionAktualnosci.selected;

    //Sprawdzenie
    console.log(this.aktualnosciDelete);
  }

  //Edytuj "Aktualności"
  GetDataFromEdytujaktualnosc() {
    //pobierz tytuł
    let inputEdytujAktualnosciTytul = (document.getElementById("edytujAktualnoscTytul") as HTMLInputElement).value;
    //pobierz treść wiadomości
    let inputEdytujAktualnosciTresc = (document.getElementById("edytujAktualnoscTresc") as HTMLTextAreaElement).value;
    //pobierz tytuł
    let inputEdytujAktualnosciData = (document.getElementById("edytujAktualnoscData") as HTMLInputElement).value;

    //Wczytanie wszystkich danych do tablicy
    this.aktualnosciEdit = this.selectionAktualnosci.selected;

    //Sprawdzenie
    console.log(inputEdytujAktualnosciTytul);
    console.log(inputEdytujAktualnosciTresc);
    console.log(inputEdytujAktualnosciData);
    console.log(this.aktualnosciDelete);
  }

  //Dodaj "Nekrolog"
  GetInputFromDodajnekrolog() {
    //pobierz imię i nazwisko
    let inputDodajNekrologImieINazwisko = (document.getElementById("dodajNekrologImieINazwisko") as HTMLInputElement).value;
    //pobierz treść wiadomości
    let inputDodajNekrologTresc = (document.getElementById("dodajNekrologTresc") as HTMLTextAreaElement).value;
    //pobierz tytuł
    let inputDodajNekrologData = (document.getElementById("dodajNekrologData") as HTMLInputElement).value;


    //Sprawdzenie
    console.log(inputDodajNekrologImieINazwisko);
    console.log(inputDodajNekrologTresc);
    console.log(inputDodajNekrologData);
  }

  //Usun "Nekrolog"
  GetCheckedUsunNekrolog() {
    //Wczytanie wszystkich danych do tablicy
    this.nekrologDelete = this.selectionNekrologi.selected;

    //Sprawdzenie
    console.log(this.nekrologDelete);
  }

  //Edytuj "Aktualności"
  GetDataFromEdytujnekrolog() {
    //pobierz tytuł
    let inputEdytujNekrologImieINazwisko = (document.getElementById("edytujNekrologImieINazwisko") as HTMLInputElement).value;

    //pobierz treść wiadomości
    let inputEdytujNekrologTresc = (document.getElementById("edytujNekrologTresc") as HTMLTextAreaElement).value;

    //pobierz date
    let inputEdytujNekrologData = (document.getElementById("edytujNekrologData") as HTMLInputElement).value;

    //Wczytanie wszystkich danych do tablicy
    this.nekrologEdit = this.selectionNekrologi.selected;

    //Sprawdzenie
    console.log(inputEdytujNekrologImieINazwisko);
    console.log(inputEdytujNekrologTresc);
    console.log(inputEdytujNekrologData);
    console.log(this.nekrologEdit);
  }

  //"Pracownicy"
  GetCheckedUsunPracownik() {
    //Wczytanie wszystkich danych do tablicy
    this.pracownikTable = this.selectionPracownicy.selected;

    //Sprawdzenie
    console.log(this.pracownikTable);
  }

  //"Pracownicy"
  GetCheckedZablokujPracownik() {
    //Wczytanie wszystkich danych do tablicy
    this.pracownikTable = this.selectionPracownicy.selected;

    //Sprawdzenie
    console.log(this.pracownikTable);
  }

  //Dodaj "Zmarłego"
  GetInputFromDodajzmarlego() {
    //pobierz imię i nazwisko
    let inputDodajZmarlegoImieINazwisko = (document.getElementById("dodajZmarlegoImieINazwisko") as HTMLInputElement).value;
    //pobierz treść wiadomości
    let inputDodajZmarlegoNumerGrobu = (document.getElementById("dodajZmarlegoNumerGrobu") as HTMLInputElement).value;
    //pobierz datę śmierci
    let inputDodajZmarlegoDataSmierci = (document.getElementById("dodajZmarlegoDataSmierci") as HTMLInputElement).value;
    //pobierz datę śmierci
    let inputDodajZmarlegoDataUrodzenia = (document.getElementById("dodajZmarlegoDataUrodzenia") as HTMLInputElement).value;

    //Sprawdzenie
    console.log(inputDodajZmarlegoImieINazwisko);
    console.log(inputDodajZmarlegoNumerGrobu);
    console.log(inputDodajZmarlegoDataSmierci);
    console.log(inputDodajZmarlegoDataUrodzenia);
  }

  //Usuń "Zmarłego"
  GetCheckedUsunZmarlego() {
    //Wczytanie wszystkich danych do tablicy
    this.ZmarliTable = this.selectionZmarli.selected;

    //Sprawdzenie
    console.log(this.ZmarliTable);
  }

  //Edytuj "Aktualności"
  GetDataFromEdytujzmarlego() {
    //pobierz imię i nazwisko
    let inputEdytujZmarlegoImieINazwisko = (document.getElementById("edytujZmarlegoImieINazwisko") as HTMLInputElement).value;
    //pobierz numer
    let inputEdytujZmarlegoNumerGrobu = (document.getElementById("edytujZmarlegoNumerGrobu") as HTMLInputElement).value;
    //pobierz datę śmierci
    let inputEdytujZmarlegoDataSmierci = (document.getElementById("edytujZmarlegoDataUrodzenia") as HTMLInputElement).value;
    //pobierz datę śmierci
    let inputEdytujZmarlegoDataUrodzenia = (document.getElementById("edytujZmarlegoDataSmierci") as HTMLInputElement).value;

    //Wczytanie wszystkich danych do tablicy
    this.ZmarliEdit = this.selectionZmarli.selected;

    //Sprawdzenie
    console.log(inputEdytujZmarlegoImieINazwisko);
    console.log(inputEdytujZmarlegoNumerGrobu);
    console.log(inputEdytujZmarlegoDataSmierci);
    console.log(inputEdytujZmarlegoDataUrodzenia);

    console.log(this.ZmarliEdit);
  }




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
    this.refreshZmarliList();

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
  isAllSelectedZmarli() {
    const numSelectedZmarli = this.selectionZmarli.selected.length;
    const numRowsZmarli = this.dataSourceZmarli.data.length;
    return numSelectedZmarli === numRowsZmarli;
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
  masterToggleZmarli() {
    this.isAllSelectedZmarli() ?
      this.selectionZmarli.clear() :
      this.dataSourceZmarli.data.forEach(row => this.selectionZmarli.select(row));
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
  checkboxLabelZmarli(row?: DeadPerson): string {
    if (!row) {
      return `${this.isAllSelectedZmarli() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionZmarli.isSelected(row) ? 'deselect' : 'select'} row ${row.Id}`;
  }
  ngAfterViewInit() {
    this.dataSourceAktualnosci.paginator = this.paginator.toArray()[0];
    this.dataSourceAktualnosci.sort = this.sort.toArray()[0];

    this.dataSourceNekrologi.paginator = this.paginator.toArray()[1];
    this.dataSourceNekrologi.sort = this.sort.toArray()[1];

    this.dataSourceZmarli.paginator = this.paginator.toArray()[2];
    this.dataSourceZmarli.sort = this.sort.toArray()[2];

  }

}
