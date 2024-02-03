import { CdkAccordionItem, CdkAccordionModule } from '@angular/cdk/accordion';
import { Component, ViewChild } from '@angular/core';


@Component({
  selector: 'ribbon',
  templateUrl: './ribbon.component.html',
  styleUrls: ['./ribbon.component.scss'],
  standalone: true,
  imports: [CdkAccordionModule],
  exportAs: 'ribbon',
  host: {
    'class': 'ribbon-wrapper'
  }
})
export class RibbonComponent  {
  @ViewChild('accordionItem', { read: CdkAccordionItem, static: false }) accordionItemRef!: CdkAccordionItem;

  toggle() {
    this.accordionItemRef.toggle();
  }

}
