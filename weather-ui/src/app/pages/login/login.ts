import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
    formState = {username: "", password: ""};
    errorMsg: string | undefined;

    constructor(private http: HttpClient, private router: Router) {}

    //todo
    login() {
      console.log(this.formState);
      this.errorMsg = undefined;

      this.http.post(
        "http://localhost:7186/api/auth/login",
        this.formState
      ).subscribe({
        next: (res: any) => {
          console.log("Login success:", res);
          localStorage.setItem("token", res.token);
          setTimeout(() => {
            this.router.navigate(['weather'])
          }, 1500)
        },
        error: (err) => {
          console.error("Login failed:", err);
          this.errorMsg = err.error.message;
        }
      });
    }
}
