import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { SharedService } from '../../shared.service'

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

  public dataSource = new MatTableDataSource<DeadPerson>();


  displayedColumns: string[] = ['Name', 'DateOfBirth', 'DateOfDeath'];

  public applyFilter(value: Event) {
    const valueFilter = (event.target as HTMLInputElement).value;
    this.dataSource.filter = valueFilter.trim().toLowerCase();
  }
  constructor(private service: SharedService) { }

  ngOnInit() {
    this.refreshZmarliList();
  }

}
