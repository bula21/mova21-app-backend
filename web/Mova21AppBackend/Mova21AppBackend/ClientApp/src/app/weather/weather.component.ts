import { Component, OnInit } from "@angular/core";
import { WeatherService } from "../shared/services/weather.service";
import { WeatherEntry } from "../shared/models/weatherEntry";
import { WeatherType } from "../shared/models/weatherType";
import { Subject } from "rxjs";
import { debounceTime } from "rxjs/operators";
import { MessageService } from "primeng/api";

@Component({
  selector: "app-weather",
  templateUrl: "./weather.component.html",
  styleUrls: ["./weather.component.css"]
})
export class WeatherComponent implements OnInit {
  weatherEntries: WeatherEntry[] = [];
  startDateRange = new Date(Date.now() - 1);
  endDateRange = new Date(Date.now() + 14);
  weatherOptions: any[];
  updateTimeouts: Map<number, Subject<WeatherEntry>>;

  constructor(private weatherService: WeatherService) {
    this.weatherOptions = [
      { value: WeatherType.Cloud, label: "Wolkig" },
      { value: WeatherType.CloudRain, label: "Regnerisch" },
      { value: WeatherType.CloudSun, label: "Wolkig und Sonnig" },
      { value: WeatherType.CloudSunRain, label: "Wolkig und Sonnig mit Regen" },
      { value: WeatherType.Fog, label: "Nebel" },
      { value: WeatherType.Snow, label: "Schnee" },
      { value: WeatherType.Sun, label: "Sonnig" },
      { value: WeatherType.Thunderstorm, label: "Gewitter" }
    ];
    this.updateTimeouts = new Map<number, Subject<WeatherEntry>>();
  }

  ngOnInit(): void {
    this.refreshEntries();
  }

  refreshEntries() {
    this.weatherService.getEntriesByDateRange(this.startDateRange, this.endDateRange).subscribe(weatherEntries => {
      this.weatherEntries = weatherEntries;
      this.weatherEntries.forEach(entry => {
        let updateTimeout = new Subject<WeatherEntry>();
        updateTimeout.pipe(debounceTime(1000)).subscribe(entry => this.updateWeatherEntry(entry));
        this.updateTimeouts.set(entry.id, updateTimeout);
      });
    });
  }

  weatherEntryChanged(entry: WeatherEntry) {
    this.updateTimeouts.get(entry.id)?.next(entry);
  }

  updateWeatherEntry(entry: WeatherEntry) {
    this.weatherService.updateEntry(entry).subscribe(_ => {
      //this.messageService.add({
      //  severity: "success",
      //  summary: "Update erfolgreich",
      //  detail: `${entry.date} - ${entry.dayTime}`
      //}); 
    });
  }

  addWeatherEntry(weather: WeatherEntry) {
    this.weatherService.addEntry(weather).subscribe(newWeather => weather.id = newWeather.id);
  }
}
