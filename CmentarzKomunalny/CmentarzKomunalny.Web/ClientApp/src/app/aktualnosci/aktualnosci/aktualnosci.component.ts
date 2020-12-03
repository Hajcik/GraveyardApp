import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-aktualnosci',
  templateUrl: './aktualnosci.component.html',
  styleUrls: ['./aktualnosci.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AktualnosciComponent implements OnInit {

  list = [];
  pageSize = 6;
  tempList = [];

  constructor() { }

  ngOnInit() {
    // get your list from api
    for (let i = 1; i < 1000; i++) {
      this.list.push({
        title: "item " + i
      });
    }

    this.tempList = this.list.slice(0, this.pageSize);
  }

  onPageChange(e) {
    this.tempList = this.list.slice(e.pageIndex * e.pageSize, (e.pageIndex + 1) * e.pageSize);
  }
  ngAfterViewInit() {
    document.querySelector('body').classList.add('active');
  }

  ngOnDestroy() {
    document.querySelector('body').classList.remove('active');
  }

}
