import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  researches: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    // this.getResearches();
  }

  registerToggle(){
    this.registerMode = true;
  }

  // getResearches() {
  //   this.http.get('http://localhost:5000/api/research').subscribe(response => {
  //     this.researches = response;
  //   }, error => {
  //     console.log(error);
  //   });
  // }

  cancelRegisterMode(registerMode: boolean){
      this.registerMode = registerMode;
  }

}
