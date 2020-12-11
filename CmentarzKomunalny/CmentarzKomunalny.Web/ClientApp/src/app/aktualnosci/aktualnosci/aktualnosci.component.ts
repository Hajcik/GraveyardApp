import { Component, OnInit, ViewEncapsulation } from '@angular/core';
/* IMPORT */
import { SharedService } from '../../shared.service'


@Component({
  selector: 'app-aktualnosci',
  templateUrl: './aktualnosci.component.html',
  styleUrls: ['./aktualnosci.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class AktualnosciComponent implements OnInit {

  aktualnosciList: any = []; // BAZA DANYCH W FORMACIE JSON
  pageSize = 6; // ILOŚĆ AKTUALNOŚCI NA STRONIE
  templateList = []; // SEGREGACJA BAZY NA SEGMENTY - PAGINACJA

  constructor(private service: SharedService) { }


  /* WCIĄGANIE BAZY DO TABLICY */
  refreshAktualnosciList() {
    this.service.getAktualnosciList().subscribe(data => {
      this.aktualnosciList = data;
    });
  }




  ngOnInit() {
    /* WCIĄGANIE BAZY DO TABLICY */
    this.service.getAktualnosciList().subscribe(data => {
      this.aktualnosciList = data;
    });

    this.templateList = this.aktualnosciList.slice(0, this.pageSize);
  }

  onPageChange(e) {
    this.templateList = this.aktualnosciList.slice(e.pageIndex * e.pageSize, (e.pageIndex + 1) * e.pageSize);
  }
  ngAfterViewInit() {
    document.querySelector('body').classList.add('active');
  }

  ngOnDestroy() {
    document.querySelector('body').classList.remove('active');
  }

}
