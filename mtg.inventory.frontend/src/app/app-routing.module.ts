import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CardsComponent } from './pages/cards/cards.component';
import { HomeComponent } from './pages/home/home.component';

const routes: Routes = [
  {path: 'cards', component: CardsComponent},
  {path: '', component: CardsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
