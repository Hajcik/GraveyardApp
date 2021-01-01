import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service'

@Component({
  selector: 'app-rocznice',
  templateUrl: './rocznice.component.html',
  styleUrls: ['./rocznice.component.css']
})



export class RoczniceComponent implements OnInit {
  public date: Date = new Date();
  roczniceLists = [];
  aktualnaRocznica = [];
  pageSize = 4;
  templateList = [];

  YYYY(date): string {
    return date.getFullYear()
  }

  DDMM(date): string {
    return this.leftpad(date.getDate(), 2)
      + '/' + this.leftpad(date.getMonth() + 1, 2);
  }

  leftpad(val, resultLength = 2, leftpadChar = '0'): string {
    return (String(leftpadChar).repeat(resultLength)
      + String(val)).slice(String(val).length);
  }

  constructor(private service: SharedService) { }

  refreshRoczniceList() {
    this.service.getZmarliList().subscribe(data => {
      this.roczniceLists = data;
    });
  }


  ngOnInit() {
    //Wyciągamy rok aby obliczyć która aktualnie rocznica wypada w obecnym roku
    const curTimeYear = this.YYYY(new Date());
    console.log(curTimeYear);
    //Wyciągamy DD/MM aby sprawdzić elementy
    const curTimeDayMonth = this.DDMM(new Date());
    console.log(curTimeDayMonth);


    this.refreshRoczniceList();
    this.templateList = this.roczniceLists.slice(0, this.pageSize);

  }

  onPageChange(e) {
    this.templateList = this.roczniceLists.slice(e.pageIndex * e.pageSize, (e.pageIndex + 1) * e.pageSize);
  }

}