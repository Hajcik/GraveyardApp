import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-aktualnosci',
  templateUrl: './aktualnosci.component.html',
  styleUrls: ['./aktualnosci.component.css']
})
export class AktualnosciComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    document.querySelector('body').classList.add('active');
  }

  ngOnDestroy() {
    document.querySelector('body').classList.remove('active');
  }

}
