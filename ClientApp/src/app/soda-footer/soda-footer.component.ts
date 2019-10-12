import { Component, Input } from '@angular/core';

@Component({
  selector: 'soda-footer',
  templateUrl: './soda-footer.component.html',
  styleUrls: ['./soda-footer.component.css']
})
export class SodaFooterComponent {
  @Input() availableMoney: string;
  @Input() commandResult: string;
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
