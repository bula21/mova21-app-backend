import { Injectable } from "@angular/core";
import { HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, of } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class ServiceBase {
  protected baseUrl = "";
  protected defaultHeaders() {
    return { 'Content-Type': "application/json", 'Authorization': `Bearer ${localStorage.getItem("token")}` }
  }
  protected httpOptions() {
    return this.defaultHeaders();
  };

  protected httpOptionsForFormData() {
    let headers = { 'Authorization': `Bearer ${localStorage.getItem("token")}` };
    return {
      headers: new HttpHeaders(headers)
    }
  };

  protected httpOptionsForBinaryDataWithHeaders(): {
    headers?: HttpHeaders | {
                [header: string]: string | string[];
              };
    observe: "response";
    params?: HttpParams | {
               [param: string]: string | string[];
             };
    reportProgress?: boolean;
    responseType: "arraybuffer";
    withCredentials?: boolean;
  } {
    return {
      headers: new HttpHeaders(this.defaultHeaders()),
      observe: "response",
      responseType: "arraybuffer"
    }
  }

  protected httpOptionsForBinaryData(additionalHeaders?: any): {
    headers?: HttpHeaders | {
                [header: string]: string | string[];
              };
    observe?: "body";
    params?: HttpParams | {
               [param: string]: string | string[];
             };
    reportProgress?: boolean;
    responseType: "arraybuffer";
    withCredentials?: boolean;
  } {
    let headers = this.defaultHeaders();
    if (additionalHeaders) {
      headers = { ...headers, ...additionalHeaders }
    }
    return {
      headers: new HttpHeaders(headers),
      responseType: "arraybuffer"
    }
  };

  protected handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      console.log(`failed: ${error.message}`);
      return of(result as T);
    };
  }
}
