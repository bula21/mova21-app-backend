import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BikeAvailabilities,  } from "../models/bikeavailabilities";
import { Observable } from "rxjs";
import { tap, catchError } from "rxjs/operators";
import { ServiceBase } from "./servicebase.service";
import { BikeAvailability } from "../models/bikeavailability";
import { ChangeBikeAvailabilityCountModel } from "../models/changeBikeAvailabilityCountModel";

@Injectable({
  providedIn: "root"
})
export class BikeService extends ServiceBase {
  private url = "api/bike";  // URL to web api

  constructor(private http: HttpClient) {
    super();
  }

  /** GET invoices from the server */
  getAvailabilities(): Observable<BikeAvailabilities> {
    return this.http.get<BikeAvailabilities>(`${this.url}`);
  }

  changeCount(delta: ChangeBikeAvailabilityCountModel): Observable<BikeAvailabilities> {
    return this.http.put<BikeAvailabilities>(`${this.url}`, delta);
  }
}
