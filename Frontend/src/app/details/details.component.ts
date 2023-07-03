import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/models/product.model';
import { ApiService } from '../apiService.model';
import { Review } from 'src/models/review.model';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
})
export class DetailsComponent implements OnInit {
  public product: Product | undefined;
  public productId: string | null;
  public reviews: Review[] = [];
  public error: boolean = false;
  public productObserver: Object = {
    next: (res: Product) => {
      res.imageUrl = res.imageUrl.split('m.jpg').join('g.jpg');
      this.product = res;
    },
    error: (err: Error) => console.log(err),
  };
  public reviewObserver: Object = {
    next: (res: Review[]) => {
      this.reviews = res;
    },
    error: (err: Error) => console.log(err),
  };

  constructor(private route: ActivatedRoute, private apiService: ApiService) {
    this.productId = this.route.snapshot.paramMap.get('id');

    if (this.productId !== null) {
      const idAsNumber: number = +this.productId;
      apiService.getProductById(idAsNumber).subscribe(this.productObserver);
      apiService.getProductReviews(idAsNumber).subscribe(this.reviewObserver);
    } else this.error = true;
  }

  ngOnInit(): void {}
}
