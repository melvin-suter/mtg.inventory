import { Component, Input, OnInit } from '@angular/core';
import { ScryfallService } from 'src/app/services/scryfall.service';

@Component({
  selector: 'app-card-row',
  templateUrl: './card-row.component.html',
  styleUrls: ['./card-row.component.scss']
})
export class CardRowComponent implements OnInit {

  cardData?:any;
  @Input('id') id?:string;

  constructor(private scryfall:ScryfallService) { 
  }

  ngOnInit(): void {
    this.scryfall.getCard(this.id!).subscribe((o) => {
      this.cardData = o;
      console.log(this.cardData);
    });
  }

}
 