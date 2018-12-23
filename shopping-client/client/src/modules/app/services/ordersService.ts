import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class OrdersService {
  constructor(private _http: HttpClient) {}

  public getOrders(): Observable<any[]> {
    return this._http.get<any>(environment.ordersApiBaseUrl + "orders");
  }
}
