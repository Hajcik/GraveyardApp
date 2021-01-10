import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SharedService } from '../../shared.service'
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

export interface DeadPerson {
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


  displayedColumns: string[] = ['Name', 'DateOfBirth', 'DateOfDeath'];
  selection = new SelectionModel<DeadPerson>(false, []);
  public applyFilter(value: Event) {
    const valueFilter = (event.target as HTMLInputElement).value;
    this.dataSource.filter = valueFilter.trim().toLowerCase();
  }
  constructor(private service: SharedService) { }

  ngOnInit() {
    this.refreshZmarliList();
  }

}
