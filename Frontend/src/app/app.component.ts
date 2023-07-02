import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Product } from 'src/models/product.model';
import { ApiService } from './apiService.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  public title: String = 'NovaVida-Test';
  public form: FormGroup;
  public products: Product[] = [];
  public isLoading: boolean = false;

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.form = this.fb.group({
      search: [''],
    });
  }
  public searchProducts(): void {
    this.isLoading = true;
    const observador = {
      next: (res: Product[]) => (this.products = res),
      error: (err: Error) => console.error(err),
    };
    const searchTerm: String = this.form.controls['search'].value;
    const response: Observable<any> =
      this.apiService.searchProducts(searchTerm);
    response.subscribe(observador);
  }
}
