import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { CardsComponent } from './pages/cards/cards.component';
import { CardComponent } from './partial/card/card.component';
import { HttpClientModule } from  '@angular/common/http';
import { CardRowComponent } from './partial/card-row/card-row.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CardsComponent,
    CardComponent,
    CardRowComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
