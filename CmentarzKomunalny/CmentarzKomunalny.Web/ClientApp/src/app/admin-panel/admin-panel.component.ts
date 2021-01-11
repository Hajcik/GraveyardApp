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
  IdDeadPerson: number;
  Name: string;
  LodgingId: number;
  DateOfBirth: string;
  DateOfDeath: string;
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

  refreshZmarliList() {
    this.service.getZmarliList().subscribe(res => {
      this.dataSourceZmarli.data = res as DeadPerson[];
    });
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
  selectionAktualnosci = new SelectionModel<Articles>(false, []);

  //TABELA NEKROLOGI
  displayedColumnsNekrologi: string[] = ['select', 'Id', 'Name', 'ObituaryContent', 'DateOfDeath_Obituary'];
  dataSourceNekrologi = new MatTableDataSource<Necrologs>();
  selectionNekrologi = new SelectionModel<Necrologs>(false, []);

  //TABELA Zmarli
  displayedColumnsZmarli: string[] = ['select', 'IdDeadPerson', 'Name', 'LodgingId', 'DateOfBirth', 'DateOfDeath'];
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

    const aktualnoscjson = {
      Title: inputDodajAktualnosciTytul,
      DateOfPublication: inputDodajAktualnosciData,
      NewsContent: inputDodajAktualnosciTresc
    };

    this.service.addAktualnosci(aktualnoscjson).subscribe(akt => this.service.addAktualnosci(aktualnoscjson));
    alert("Dodano aktualność");
    this.refreshAktualnosciList();
    
    //Sprawdzenie
    console.log(inputDodajAktualnosciTytul);
    console.log(inputDodajAktualnosciTresc);
    console.log(inputDodajAktualnosciData);
  }

  //Usun "Aktualnosci"
  GetCheckedUsunAktualnosci() {
    //Wczytanie wszystkich danych do tablicy

    this.aktualnosciDelete = this.selectionAktualnosci.selected.map(x => x.Id);

    if (confirm("Czy jesteś pewny?"))
    {
      if (this.aktualnosciDelete == "") { alert("Nie zaznaczono aktualności, spróbuj ponownie") }
      else {
      this.service.deleteAktualnosci(this.aktualnosciDelete).subscribe();
      this.refreshAktualnosciList();
      alert("Usunięto aktualność")
    //Sprawdzenie
        console.log(this.aktualnosciDelete);
        console.log(this.service.deleteAktualnosci(this.aktualnosciDelete).subscribe());
      }
    }
  }

  inputEdytujAktualnosciTytul: any;
  inputEdytujAktualnosciTresc: any;
  inputEdytujAktualnosciData: any;

  //Edytuj "Aktualności"
  GetDataFromEdytujaktualnosc() {
    //pobierz tytuł
    this.inputEdytujAktualnosciTytul = (document.getElementById("edytujAktualnoscTytul") as HTMLInputElement).value;
    //pobierz treść wiadomości
    this.inputEdytujAktualnosciTresc = (document.getElementById("edytujAktualnoscTresc") as HTMLTextAreaElement).value;
    //pobierz tytuł
    this.inputEdytujAktualnosciData = (document.getElementById("edytujAktualnoscData") as HTMLInputElement).value;

    //Wczytanie wszystkich danych do tablicy
    this.aktualnosciEdit = this.selectionAktualnosci.selected.map(x => x.Id).join("");

    if (this.inputEdytujAktualnosciTytul == "") {
      this.inputEdytujAktualnosciTytul = this.selectionAktualnosci.selected.map(x => x.Title).join("");
    }

    if (this.inputEdytujAktualnosciTresc == "") {
      this.inputEdytujAktualnosciTresc = this.selectionAktualnosci.selected.map(x => x.NewsContent).join("");
    }

    if (this.inputEdytujAktualnosciData == "") {
      this.inputEdytujAktualnosciData = this.selectionAktualnosci.selected.map(x => x.DateOfPublication).join("");
    }

    const aktualnoscjson = {
      Id:this.aktualnosciEdit,
      Title:this.inputEdytujAktualnosciTytul,
      NewsContent:this.inputEdytujAktualnosciTresc,
      DateOfPublication:this.inputEdytujAktualnosciData,
    };
    if (this.aktualnosciEdit == "") { alert("Nie wybrano żadnej aktualności, spróbuj ponownie") }
    else if (confirm) {
      alert("Czy na pewno?")
      this.service.putAktualnosci(aktualnoscjson).subscribe();
    
    this.refreshAktualnosciList();
      //Sprawdzenie
      console.log(this.inputEdytujAktualnosciTytul);
      console.log(this.inputEdytujAktualnosciTresc);
      console.log(this.inputEdytujAktualnosciData);
    }
    this.refreshAktualnosciList();
  }

  //Dodaj "Nekrolog"
  GetInputFromDodajnekrolog() {
   // let inputDodajNekrologImieINazwisko = "";
    //pobierz imię i nazwisko
    let inputDodajNekrologImieINazwisko = (document.getElementById("dodajNekrologImieINazwisko") as HTMLInputElement).value;
    //pobierz treść wiadomości
    let inputDodajNekrologTresc = (document.getElementById("dodajNekrologTresc") as HTMLTextAreaElement).value;
    //pobierz tytuł
    let inputDodajNekrologData = (document.getElementById("dodajNekrologData") as HTMLInputElement).value;

    const nekrologjson = {
      Name: inputDodajNekrologImieINazwisko,
      ObituaryContent: inputDodajNekrologTresc,
      DateOfDeath_Obituary: inputDodajNekrologData
    };

    this.service.addNekrologi(nekrologjson).subscribe(akt => this.service.addNekrologi(nekrologjson));
    alert("Dodano nekrolog");
    this.refreshNekrologiList();
    //Sprawdzenie
    console.log(inputDodajNekrologImieINazwisko);
    console.log(inputDodajNekrologTresc);
    console.log(inputDodajNekrologData);
  }

  //Usun "Nekrolog"
  GetCheckedUsunNekrolog() {
    //Wczytanie wszystkich danych do tablicy
    this.nekrologDelete = this.selectionNekrologi.selected.map(x => x.Id);

    if (this.nekrologDelete == "") {
      alert("Nie wybrano żadnego nekrologu, spróbuj ponownie");
    }
    else if (confirm("Czy jesteś pewny?")) {
      this.service.deleteNekrologi(this.nekrologDelete).subscribe();
      this.refreshNekrologiList();
      alert("Usunięto nekrolog");
      //Sprawdzenie
      console.log(this.nekrologDelete);
    }
  }

  inputEdytujNekrologImieINazwisko: any;
  inputEdytujNekrologTresc: any;
  inputEdytujNekrologData: any;

  //Edytuj "Nekrolog"
  GetDataFromEdytujnekrolog() {
    //pobierz tytuł
    this.inputEdytujNekrologImieINazwisko = (document.getElementById("edytujNekrologImieINazwisko") as HTMLInputElement).value;

    //pobierz treść wiadomości
    this.inputEdytujNekrologTresc = (document.getElementById("edytujNekrologTresc") as HTMLTextAreaElement).value;

    //pobierz date
     this.inputEdytujNekrologData = (document.getElementById("edytujNekrologData") as HTMLInputElement).value;

    
    //Wczytanie wszystkich danych do tablicy
    this.nekrologEdit = this.selectionNekrologi.selected.map(x => x.Id).join("");

    if (this.inputEdytujNekrologData == "") {
      this.inputEdytujNekrologData = this.selectionNekrologi.selected.map(x => x.DateOfDeath_Obituary).join("");
    }

    if (this.inputEdytujNekrologImieINazwisko == "") {
      this.inputEdytujNekrologImieINazwisko = this.selectionNekrologi.selected.map(x => x.Name).join("");
    }

    if (this.inputEdytujNekrologTresc == "") {
      this.inputEdytujNekrologTresc = this.selectionNekrologi.selected.map(x => x.ObituaryContent).join("");
    }

    const nekrologjson = {
      Id:this.nekrologEdit,
      Name: this.inputEdytujNekrologImieINazwisko,
      DateOfDeath_Obituary: this.inputEdytujNekrologData,
      ObituaryContent:this.inputEdytujNekrologTresc,
    };

    if (this.nekrologEdit == "") { alert("Nie wybrano żadnego nekrologu, spróbuj ponownie") }
    else if (confirm) {
      alert("Czy na pewno?")
      this.service.putNekrologi(nekrologjson).subscribe();
    
    
        this.refreshNekrologiList();
        alert("Zaktualizowano pomyślnie")
        
        //Sprawdzenie
        console.log(this.inputEdytujNekrologImieINazwisko);
        console.log(this.inputEdytujNekrologTresc);
        console.log(this.inputEdytujNekrologData);
        console.log(this.nekrologEdit);
        console.log(this.service.putNekrologi(nekrologjson).subscribe(akt => this.service.putNekrologi(nekrologjson), this.nekrologEdit));
    }
    this.refreshNekrologiList();
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



    const zmarlyjson = {
      Name: inputDodajZmarlegoImieINazwisko,
      DateOfBirth: inputDodajZmarlegoDataUrodzenia,
      DateOfDeath: inputDodajZmarlegoDataSmierci,
      LodgingId: inputDodajZmarlegoNumerGrobu
    };

    this.service.addDeadPerson(zmarlyjson).subscribe(akt => this.service.addDeadPerson(zmarlyjson));
    alert("Dodano zmarłą osobę");
    this.refreshZmarliList();
    //Sprawdzenie
    console.log(inputDodajZmarlegoImieINazwisko);
    console.log(inputDodajZmarlegoNumerGrobu);
    console.log(inputDodajZmarlegoDataSmierci);
    console.log(inputDodajZmarlegoDataUrodzenia);
  }

  //Usuń "Zmarłego"
  GetCheckedUsunZmarlego() {
    //Wczytanie wszystkich danych do tablicy
    this.ZmarliTable = this.selectionZmarli.selected.map(x => x.IdDeadPerson);

    if (this.ZmarliTable == "") {
      alert("Nie wybrano żadnej zmarłej osoby, spróbuj ponownie");
    }
    else if (confirm("Czy jesteś pewny?")) {
      for (let item of this.ZmarliTable) {
        this.service.deleteDeadPerson(item).subscribe();
      }

      this.refreshZmarliList();
      alert("Usunięto zmarłego");

      //Sprawdzenie
      console.log(this.ZmarliTable);
    }
  }

  inputEdytujZmarlegoImieINazwisko: any;
  inputEdytujZmarlegoNumerGrobu: any;
  inputEdytujZmarlegoDataSmierci: any;
  inputEdytujZmarlegoDataUrodzenia: any;

  //Edytuj "Zmarly"
  GetDataFromEdytujzmarlego() {
    //pobierz imię i nazwisko
    this.inputEdytujZmarlegoImieINazwisko = (document.getElementById("edytujZmarlegoImieINazwisko") as HTMLInputElement).value;
    //pobierz numer
    this.inputEdytujZmarlegoNumerGrobu = (document.getElementById("edytujZmarlegoNumerGrobu") as HTMLInputElement).value;
    //pobierz datę śmierci
    this.inputEdytujZmarlegoDataSmierci = (document.getElementById("edytujZmarlegoDataUrodzenia") as HTMLInputElement).value;
    //pobierz datę śmierci
    this.inputEdytujZmarlegoDataUrodzenia = (document.getElementById("edytujZmarlegoDataSmierci") as HTMLInputElement).value;

    //Wczytanie wszystkich danych do tablicy
    this.ZmarliEdit = this.selectionZmarli.selected.map(x => x.IdDeadPerson).join("");

    if (this.inputEdytujZmarlegoImieINazwisko == "") {
      this.inputEdytujZmarlegoImieINazwisko = this.selectionZmarli.selected.map(x => x.Name).join("");
    }

    if (this.inputEdytujZmarlegoNumerGrobu == "") {
      this.inputEdytujZmarlegoNumerGrobu = this.selectionZmarli.selected.map(x => x.LodgingId).join("");
    }

    if (this.inputEdytujZmarlegoDataSmierci == "") {
      this.inputEdytujZmarlegoDataSmierci = this.selectionZmarli.selected.map(x => x.DateOfDeath).join("");
    }

    if (this.inputEdytujZmarlegoDataUrodzenia == "") {
      this.inputEdytujZmarlegoDataUrodzenia = this.selectionZmarli.selected.map(x => x.DateOfBirth).join("");
    }


    const zmarlyjson = {
      IdDeadPerson:this.ZmarliEdit,
      Name:this.inputEdytujZmarlegoImieINazwisko,
      DateOfBirth:this.inputEdytujZmarlegoDataUrodzenia,
      DateOfDeath:this.inputEdytujZmarlegoDataSmierci,
      LodgingId:this.inputEdytujZmarlegoNumerGrobu
    };

    if (this.ZmarliEdit == "") { alert("Nie wybrano zmarłego, spróbuj ponownie") }
    else if (confirm) {
      alert("Czy na pewno?")
      this.service.putDeadPerson(zmarlyjson).subscribe();
    
    this.refreshZmarliList();
      alert("Zaktualizowano pomyślnie");
      //Sprawdzenie
      console.log(this.inputEdytujZmarlegoImieINazwisko);
      console.log(this.inputEdytujZmarlegoNumerGrobu);
      console.log(this.inputEdytujZmarlegoDataSmierci);
      console.log(this.inputEdytujZmarlegoDataUrodzenia);

      console.log(this.ZmarliEdit);
    }
    this.refreshZmarliList();
  }

  ngOnInit() {
    this.refreshAktualnosciList();
    this.refreshNekrologiList();
    this.refreshZmarliList();
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
  
  checkboxLabelZmarli(row?: DeadPerson): string {
    if (!row) {
      return `${this.isAllSelectedZmarli() ? 'select' : 'deselect'} all`;
    }
    return `${this.selectionZmarli.isSelected(row) ? 'deselect' : 'select'} row ${row.IdDeadPerson}`;
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
