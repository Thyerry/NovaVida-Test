import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Product } from 'src/models/product.model';
import { ApiService } from '../apiService.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
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
