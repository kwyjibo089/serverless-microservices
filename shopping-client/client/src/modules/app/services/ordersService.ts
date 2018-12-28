import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable, from, of } from "rxjs";
import { switchMap, mergeMap, toArray, catchError } from 'rxjs/operators';

@Injectable()
export class OrdersService {
  constructor(private _http: HttpClient) {}

  public getOrders(): Observable<any[]> {

    const ordersList = this._http.get<any>(environment.ordersApiBaseUrl + "orders")
      .pipe(
        switchMap(orders => from(orders)),
        // TODO: loop through all items
        mergeMap((order: any) => this._http.get<any>(environment.productsApiBaseUrl + "products/" + order.items[0].id),
          (order, items) => Object.assign(order, {items: [items]})
        ),
        toArray(),
        catchError((err) => {
          console.log(`Inner error: ${err}`);
          // TODO: what should we do in case of errors?
          return of([]);
      }));

      return ordersList;
  }
}
