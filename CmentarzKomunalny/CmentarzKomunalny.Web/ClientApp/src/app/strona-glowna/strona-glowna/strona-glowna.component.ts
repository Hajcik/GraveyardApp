import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-strona-glowna',
  animations: [
    trigger('thumbState', [
      state('inactive', style({
        opacity: 0, transform: 'scale(0.5)'
      })),
      state('active', style({
        opacity: 1, transform: 'scale(1)'
      })),
      transition('inactive => active', animate('500ms cubic-bezier(0.785, 0.135, 0.15, 0.86)')),
      transition('active => inactive', animate('500ms cubic-bezier(0.785, 0.135, 0.15, 0.86)'))
    ])
  ],
  templateUrl: './strona-glowna.component.html',
  styleUrls: ['./strona-glowna.component.css']
})
export class StronaGlownaComponent implements OnInit {
  @Output() change: EventEmitter<number> = new EventEmitter<number>();
  counter = 0;

  ngAfterContentInit() {
    this.change.emit(0);
  }

  onClickThumb(event) {
    const total = this.images.length - 1;
    this.counter = event.layerX < 150 ? this.dec(total) : this.inc(total);
    this.change.emit(this.counter);
  }

  onNext() {
    const total = this.images.length - 1;
    this.counter = this.inc(total);
    this.change.emit(this.counter);
  }
  onPrevious() {
    const total = this.images.length - 1;
    this.counter = this.dec(total);
    this.change.emit(this.counter);
  }

  inc(total) {
    return (this.counter < total) ? this.counter + 1 : 0;
  }

  dec(total) {
    return (this.counter > 0) ? this.counter - 1 : total;
  }

  index;

  images = [
    'https://placeimg.com/300/300/nature/6',
    'https://placeimg.com/300/300/nature/7',
    'https://placeimg.com/300/300/nature/8',
    'https://placeimg.com/300/300/nature/9',
    'https://placeimg.com/300/300/nature/2',
    'https://placeimg.com/300/300/nature/3',
    'https://placeimg.com/300/300/nature/1',
  ];

  slideItems = [
    { src: 'https://placeimg.com/600/600/any', title: 'Title 1' },
    { src: 'https://placeimg.com/600/600/nature', title: 'Title 2' },
    { src: 'https://placeimg.com/600/600/sepia', title: 'Title 3' },
    { src: 'https://placeimg.com/600/600/people', title: 'Title 4' },
    { src: 'https://placeimg.com/600/600/tech', title: 'Title 5' }
  ];

  onChange(idx) {
    console.log(idx);
    this.index = idx;
  }
  constructor() { }

  ngOnInit() {
  }

}
