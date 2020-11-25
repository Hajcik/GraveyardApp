import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-nekrologi',
  templateUrl: './nekrologi.component.html',
  styleUrls: ['./nekrologi.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class NekrologiComponent implements OnInit {

  list = [];
  pageSize = 5;
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

}
