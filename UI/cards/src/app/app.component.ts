import { Component, OnInit } from '@angular/core';
import { Card } from './models/card.model';
import { CardsService } from './service/cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'cards';
  cards : Card[] = [];
  card : Card = {
    id: '',
    cardNumber : '',
    cardholderName : '',
    cvc : '',
    expiryMonth : '',
    expiryYear : ''

  }

  constructor (private cardsService : CardsService)
  {

  }
  ngOnInit(): void  {
    this.getAllCards()
  }

  getAllCards(){
    this.cardsService.getAllCards()
    .subscribe (
      response => {
        this.cards = response;
        
      }
    );
  }
  OnSubmit(){

    if(this.card.id === ''){
      this.cardsService.addCard(this.card)
      .subscribe(
        response => {

          this.getAllCards();
          this.card = {
           id: '',
           cardNumber : '',
           cardholderName : '',
           cvc : '',
           expiryMonth : '',
           expiryYear : ''
         };
        }
   
      );

    } else {
      this.updateCard(this.card);

    }
    window.location.reload();
  }

  deleteCard(id : string)
  {
      this.cardsService.deleteCard(id)
      .subscribe(
        response => {
          this.getAllCards();
        }
      )
      window.location.reload();
  }

  populateForm(card: Card)
  {
      this.card = card;
  }

  updateCard(card : Card)
  {
    this.cardsService.updateCard(card)
    .subscribe(
      response => {
        this.getAllCards();
      }
    )
    window.location.reload();
  }
}
