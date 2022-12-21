import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ScryfallCardModel } from '../models/scryfall-card.model';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScryfallService {

  constructor(private http:HttpClient) { }

  searchCard(query:string){
    return this.http.get<ScryfallCardModel>('https://api.scryfall.com/cards/search?q=' + query);
  }
}
