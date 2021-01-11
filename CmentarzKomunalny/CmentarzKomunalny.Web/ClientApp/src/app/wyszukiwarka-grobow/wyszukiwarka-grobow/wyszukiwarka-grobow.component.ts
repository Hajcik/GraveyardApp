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
  refreshZmarliList() {
    this.service.getZmarliList().subscribe(res => {
      this.dataSource.data = res as DeadPerson[];
    });
  }

  @ViewChild(MatPaginator, { static: false }) paginator
  @ViewChild(MatSort, { static: false }) sort
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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
    

    console.log(this.selectedPerson);
    if (this.numer <= 20) {
      this.sektor = 1;
    }
    if (this.numer > 20 && this.numer <= 40) {
      this.sektor = 2;
    }
    if (this.numer > 40 && this.numer <= 60) {
      this.sektor = 3;
    }
    if (this.numer > 60 && this.numer <= 80) {
      this.sektor = 4;
    }
    if (this.numer > 80 && this.numer <= 100) {
      this.sektor = 5;
    }
    if (this.numer > 100 && this.numer <= 120) {
      this.sektor = 6;
    }
    if (this.numer > 120 && this.numer <= 140) {
      this.sektor = 7;
    }
    if (this.numer > 140 && this.numer <= 160) {
      this.sektor = 8;
    }
    if (this.numer > 160 && this.numer <= 180) {
      this.sektor = 9;
    }
    if (this.numer > 180 && this.numer <= 200) {
      this.sektor = 10;
    }
    if (this.numer > 200 && this.numer <= 220) {
      this.sektor = 11;
    }
    if (this.numer > 220 && this.numer <= 240) {
      this.sektor = 12;
    }
    if (this.numer > 240 && this.numer <= 290) {
      this.sektor = 13;
    }
    if (this.numer > 290 && this.numer <= 340) {
      this.sektor = 14;
    }

    console.log(this.numer);

  }
}
