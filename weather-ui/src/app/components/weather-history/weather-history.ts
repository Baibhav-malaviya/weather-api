import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface WeatherDto {
  city: string;
  temp: number;
  feelsLike: number;
  tempMin: number;
  tempMax: number;
  pressure: number;
  humidity: number;
  requestedAt: string;
}

interface PagedResponse<T> {
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  data: T[];
}

@Component({
  standalone: true,
  selector: 'app-weather-history',
  imports: [],
  templateUrl: './weather-history.html',
  styleUrl: './weather-history.css',
})
export class WeatherHistory implements OnInit {

  constructor(private http: HttpClient) {}

  history: WeatherDto[] = [];
  page = 1;
  pageSize = 5;
  totalPages = 0;

  ngOnInit(): void {
    this.loadHistory();
  }

  loadHistory() {
    this.http.get<PagedResponse<WeatherDto>>(
      `http://localhost:7186/api/weather/history?page=${this.page}&pageSize=${this.pageSize}`
    ).subscribe(res => {
      this.history = res.data;
      this.totalPages = res.totalPages;
    });
  }

  nextPage() {
    if (this.page < this.totalPages) {
      this.page++;
      this.loadHistory();
    }
  }

  prevPage() {
    if (this.page > 1) {
      this.page--;
      this.loadHistory();
    }
  }
}