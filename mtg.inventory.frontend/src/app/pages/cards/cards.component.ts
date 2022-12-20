import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { CardModel } from 'src/app/models/card.model';
import { FolderModel } from 'src/app/models/folder.model';
import { CardService } from 'src/app/services/card.service';
import { CollectionService } from 'src/app/services/collection.service';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.scss']
})
export class CardsComponent implements OnInit {

  folder?:FolderModel;
  cards?:CardModel[];

  constructor(private collectionService: CollectionService, private cardService: CardService, private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.folder = collectionService.getFolder(params['id']);
      this.cards = cardService.getCards(params['id']);
    });
  }

  ngOnInit(): void {
  }

}
