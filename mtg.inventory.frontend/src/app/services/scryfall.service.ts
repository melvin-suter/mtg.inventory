import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ScryfallService {

  baseUrl:string = "https://api.scryfall.com/";

  constructor(private http:HttpClient) { }


  public getCard(id:string){
    return this.http.get<any>(this.baseUrl + "cards/" + id);
  }

}
