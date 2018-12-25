import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable, from } from "rxjs";
import { switchMap, mergeMap, toArray } from 'rxjs/operators';

@Injectable()
export class OrdersService {
  constructor(private _http: HttpClient) {}

  public getOrders(): Observable<any[]> {

    const ordersList = this._http.get<any>(environment.ordersApiBaseUrl + "orders")
      .pipe(
        switchMap(orders => from(orders)),
        // TODO: loop through all items
        mergeMap(order => this._http.get<any>(environment.productsApiBaseUrl + "products/" + (<any>order).items[0].id),
          (order, items) => Object.assign(order, {items: [items]})
        ),
        toArray()
      );

      return ordersList;
  }
}
