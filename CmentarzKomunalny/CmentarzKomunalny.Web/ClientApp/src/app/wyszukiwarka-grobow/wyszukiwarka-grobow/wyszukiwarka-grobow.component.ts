import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SharedService } from '../../shared.service'
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

export interface DeadPerson {
  LodgingId: number,
  Name: string,
  DateOfBrith: string,
  DateOfDeath: string
}

@Component({
  selector: 'app-wyszukiwarka-grobow',
  templateUrl: './wyszukiwarka-grobow.component.html',
  styleUrls: ['./wyszukiwarka-grobow.component.css']
})
export class WyszukiwarkaGrobowComponent implements OnInit {
  panelOpenState = false;
  public selectedPerson: any = [];
  sektor: number = 0;
  numer: any = 0;
  image: string = "../../../assets/1.png";
  refreshZmarliList() {
    this.service.getZmarliList().subscribe(res => {
      this.dataSource.data = res as DeadPerson[];
    });
  }

  @ViewChild(MatPaginator, { static: false }) paginatorZmarli
  @ViewChild(MatSort, { static: false }) sortZmarli
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginatorZmarli;
    this.dataSource.sort = this.sortZmarli;
  }
  public dataSource = new MatTableDataSource<DeadPerson>();


  displayedColumns: string[] = ['LodgingId', 'Name', 'DateOfBirth', 'DateOfDeath'];
  selection = new SelectionModel<DeadPerson>(false, []);
  public applyFilter(value: Event) {
    const valueFilter = (event.target as HTMLInputElement).value;
    this.dataSource.filter = valueFilter.trim().toLowerCase();
  }
  constructor(private service: SharedService) { }

  ngOnInit() {
    this.refreshZmarliList();
  }
  getValue(event: any) {
    let value = (event.target as HTMLInputElement).value;
    console.log("LodgingId: ", value);
  }

  updateCheckedList() {
    this.numer = this.selection.selected.map(x => x.LodgingId);

    console.log(this.numer);
    if (this.numer <= 32) {
      this.sektor = 1;
      this.image = "../../../assets/1.png";
    }
    if (this.numer > 32 && this.numer <= 64) {
      this.sektor = 2;
      this.image = "../../../assets/2.png";
    }
    if (this.numer > 64 && this.numer <= 96) {
      this.sektor = 3;
      this.image = "../../../assets/3.png";
    }
    if (this.numer > 96 && this.numer <= 104) {
      this.sektor = 4;
      this.image = "../../../assets/4.png";
    }
    if (this.numer > 104 && this.numer <= 112) {
      this.sektor = 5;
      this.image = "../../../assets/5.png";
    }
    if (this.numer > 112 && this.numer <= 120) {
      this.sektor = 6;
      this.image = "../../../assets/6.png";
    }
    if (this.numer > 120 && this.numer <= 128) {
      this.sektor = 7;
      this.image = "../../../assets/7.png";
    }
    if (this.numer > 128 && this.numer <= 136) {
      this.sektor = 8;
      this.image = "../../../assets/8.png";
    }
    if (this.numer > 136 && this.numer <= 144) {
      this.sektor = 9;
      this.image = "../../../assets/9.png";
    }
    if (this.numer > 144 && this.numer <= 146) {
      this.sektor = 10;
      this.image = "../../../assets/10.png";
    }
    if (this.numer > 146 && this.numer <= 148) {
      this.sektor = 11;
      this.image = "../../../assets/11.png";
    }
    if (this.numer > 148 && this.numer <= 200) {
      this.sektor = 12;
      this.image = "../../../assets/12.png";
    }
    if (this.numer > 200 && this.numer <= 250) {
      this.sektor = 13;
      this.image = "../../../assets/13.png";
    }


    console.log(this.numer);

  }

}
