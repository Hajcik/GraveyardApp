import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { SharedService } from '../../shared.service'

@Component({
  selector: 'app-nekrologi',
  templateUrl: './nekrologi.component.html',
  styleUrls: ['./nekrologi.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class NekrologiComponent implements OnInit {

  nekrologiLists = []; // TABLICA BAZY
  pageSize = 4;
  templateList = [];

  constructor(private service: SharedService) { }

  refreshNekrologiList() {
    this.service.getNekrologiList().subscribe(data => {
      this.nekrologiLists = data;
      console.log("data:");
    });
  }

  ngOnInit() {

    this.refreshNekrologiList();

    this.templateList = this.nekrologiLists.slice(0, this.pageSize);

  }

  onPageChange(e) {
    this.templateList = this.nekrologiLists.slice(e.pageIndex * e.pageSize, (e.pageIndex + 1) * e.pageSize);
  }

}
