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
  getProductById(id: number): Observable<any> {
    const url = `https://localhost:7097/Product/${id}`;
    return this.http.get(url);
  }
  getProductReviews(id: number): Observable<any> {
    const url = `https://localhost:7097/Product/reviews?productId=${id}&quantity=5`;
    return this.http.get(url);
  }
  searchProducts(searchTerm: String): Observable<any> {
    const url = `https://localhost:7097/Product/search?search=${searchTerm}`;
    return this.http.get(url);
  }
}
