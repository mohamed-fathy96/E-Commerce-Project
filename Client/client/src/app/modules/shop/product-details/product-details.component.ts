import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from '../../shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product!: IProduct;

  constructor(private shopService: ShopService, private actiatedRoute: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    this.shopService.getProductById(Number(this.actiatedRoute.snapshot.paramMap.get('id')))
      .subscribe(response => {
        this.product = response;
      })
  }

}
