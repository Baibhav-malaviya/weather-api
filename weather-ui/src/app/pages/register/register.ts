import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {FormsModule} from '@angular/forms'
import { Router, RouterLink } from "@angular/router";
import { errorContext } from 'rxjs/internal/util/errorContext';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
   formState = {username: "", password: ""};
   errorMsg: string | undefined;

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
            this.router.navigate(['login'])
          }, 1500)
        },
        error: (err) => {
          console.error("Register failed:", err);
          this.errorMsg = err.error.message;
        }
      });
    }

}
