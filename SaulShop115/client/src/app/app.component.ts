import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Saul Shop 115';
  //products: any[];
  products: IProduct[];

  constructor(private http: HttpClient){}

  ngOnInit(): void {
   this.http.get('/api/products?pageSize=50').subscribe((response: IPagination) => {
      console.log(response);
      this.products = response.data;
    }, error =>{     
      console.log(error);
    }); 

  }
}
