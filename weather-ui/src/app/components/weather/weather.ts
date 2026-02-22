import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { WeatherHistory } from "../weather-history/weather-history";

interface IWeatherData {
  temp: number;
  temp_min: number;
  temp_max: number;
  pressure: number;
  feels_like: number;
  grnd_level: number;
  sea_level: number;
  humidity: number;
}

@Component({
  selector: 'app-weather',
  imports: [FormsModule, WeatherHistory],
  templateUrl: './weather.html',
  styleUrl: './weather.css',
})
export class Weather {

    constructor(private http: HttpClient) {}

    city:string ='';
    weatherData: IWeatherData | null = null;

   //todo
    fetchWeatherByCity() {
      this.http.get(
        `http://localhost:7186/api/weather/${this.city}`,
      ).subscribe({
        next: (res: any) => {
          console.log("weather fetching success:", res);
          this.weatherData = res.main;
          console.log(this.weatherData);
          
        },
        error: (err) => {
          console.error("Register failed:", err);
        }
      });
    }
}
