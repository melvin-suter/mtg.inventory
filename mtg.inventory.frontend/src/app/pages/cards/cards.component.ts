import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CardModel } from 'src/app/models/card.model';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.scss']
})
export class CardsComponent implements OnInit {

  cards:Observable<CardModel[]>;

  constructor(private data:DataService) { 
    this.cards = this.data.getAllCards();
  }

  ngOnInit(): void {
  }

}
