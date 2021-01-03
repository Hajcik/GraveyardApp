import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SelectionModel } from '@angular/cdk/collections';
@Component({
  selector: 'app-worker-panel',
  templateUrl: './worker-panel.component.html',
  styleUrls: ['./worker-panel.component.css']
})
export class WorkerPanelComponent implements OnInit {

  constructor(fb: FormBuilder) { }


  ngOnInit() {
  }

}
