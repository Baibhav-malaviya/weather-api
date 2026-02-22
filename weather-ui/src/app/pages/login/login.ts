import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
    formState = {username: "", password: ""};

    constructor(private http: HttpClient) {}

    // async login() {
    //   console.log(this.formState);
    //   let res = await this.http.post("https://localhost:5198/api/auth/login", this.formState);
    //   console.log("RES: ", res);
    // }

    //todo
    login() {
      console.log(this.formState);

      this.http.post(
        "http://localhost:7186/api/auth/login",
        this.formState
      ).subscribe({
        next: (res: any) => {
          console.log("Login success:", res);
          localStorage.setItem("token", res.token);
        },
        error: (err) => {
          console.error("Login failed:", err);
        }
      });
    }
}
