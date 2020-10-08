import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Saul Shop 115';
  //products: any[];
  //products: IProduct[];

  constructor(){}

  ngOnInit(): void {
  }
}
