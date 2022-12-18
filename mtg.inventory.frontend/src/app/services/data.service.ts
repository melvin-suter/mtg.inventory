import { Injectable } from '@angular/core';
import { CardModel } from '../models/card.model';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  baseUrl:string = "http://localhost:3000/";

  constructor(private http:HttpClient) { }

  public getAllCards(){
    return this.http.get<CardModel[]>(this.baseUrl + "cards");
  }
}
