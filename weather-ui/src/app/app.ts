import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink, Router } from '@angular/router';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor(private router: Router) {}

  protected readonly title = signal('weather-ui');
  isLoggedIn: string | null = localStorage.getItem('token');

  logout() {
    if (this.isLoggedIn) {
      localStorage.removeItem('token');
      this.router.navigate(['/login'])
    }
  }
}
