import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getProducts(): Observable<any> {
    const url = 'https://localhost:7097/Product';
    return this.http.get(url);
  }

  searchProducts(searchTerm: String): Observable<any> {
    const url = `https://localhost:7097/Product/search?search=${searchTerm}`;
    return this.http.get(url);
  }
}
