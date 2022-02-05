import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { tap, catchError } from "rxjs/operators";
import { ServiceBase } from "./servicebase.service";
import { WeatherEntry } from "../models/weatherEntry";

@Injectable({
  providedIn: "root"
})
export class WeatherService extends ServiceBase {
  private url = "api/weather";  // URL to web api

  constructor(private http: HttpClient) {
    super();
  }

  /** GET invoices from the server */
  getEntriesByDateRange(startDate: Date, endDate: Date): Observable<WeatherEntry[]> {
    return this.http.get<WeatherEntry[]>(`${this.url}/${startDate.toISOString()}/${endDate.toISOString()}`);
  }

  updateEntry(entry: WeatherEntry): Observable<void> {
    return this.http.put<void>(`${this.url}`, entry);
  }

  addEntry(entry: WeatherEntry): Observable<WeatherEntry> {
    return this.http.post<WeatherEntry>(`${this.url}`, entry);
  }
}
