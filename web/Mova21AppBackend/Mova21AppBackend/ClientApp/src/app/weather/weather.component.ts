import { Component, OnInit } from "@angular/core";
import { WeatherService } from "../shared/services/weather.service";
import { WeatherEntry } from "../shared/models/weatherEntry";


@Component({
  selector: "app-weather",
  templateUrl: "./weather.component.html",
  styleUrls: ["./weather.component.css"]
})
export class WeatherComponent implements OnInit {
  weatherEntries: WeatherEntry[]= [];
  startDate =  new Date(Date.now() - 1);
  endDate = new Date(Date.now() + 14);

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.weatherService.getEntriesByDateRange(this.startDate, this.endDate).subscribe(weatherEntries => {
      this.weatherEntries = weatherEntries;
    });
  }

  updateWeatherEntry(entry: WeatherEntry) {
    this.weatherService.updateEntry(entry).subscribe(_ => { /*some info about successfully updated*/});
  }

  addWeatherEntry(weather: WeatherEntry) {
    this.weatherService.addEntry(weather).subscribe(newWeather => weather.id = newWeather.id);
  }
}
