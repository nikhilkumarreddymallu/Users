import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  
constructor(private http: HttpClient) {}
  async ngOnInit(){
   await this.getUsers();
  }
  title = 'Client';

  async getUsers() {
    //var users = this.http.get('https://localhost:7006/api/User').subscribe(data => console.log("data", data));
    var users = await this.http.get('https://localhost:7006/api/User').toPromise();

    console.log("users", users);
  }
}
