import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent {
  public values: string[];
  public vehicles: Object[];
  //constructor(private http: HttpClient) {
  //  this.http.get('/api/values').subscribe(result => {
  //    this.values = result as string[];
  //  }, error => console.error(error));
  //}
}
