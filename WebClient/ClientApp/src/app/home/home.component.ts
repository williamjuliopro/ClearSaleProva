import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  Comanda: Comanda[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: String) {
    http.get<Comanda[]>(baseUrl + 'api/Comanda').subscribe(result => {
        this.Comanda = result;
    }, error => console.error(error));
  }
}

interface Comanda {
  Id: Number,
  Valor: number,


}
