import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {FormsModule} from '@angular/forms'
import { Router, RouterLink } from "@angular/router";

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
   formState = {username: "", password: ""};

    constructor(private http: HttpClient, private router: Router) {}

    //todo
    register() {
      console.log(this.formState);

      this.http.post(
        "http://localhost:7186/api/auth/register",
        this.formState
      ).subscribe({
        next: (res: any) => {
          console.log("Register success:", res);
          setTimeout(() => {
            this.router.navigate(['/login'])
          }, 2000)
        },
        error: (err) => {
          console.error("Register failed:", err);
        }
      });
    }

}
