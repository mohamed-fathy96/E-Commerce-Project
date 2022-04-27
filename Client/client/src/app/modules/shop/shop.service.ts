import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs/internal/Observable';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  ApiUrl: string = environment.ApiUrl;

  constructor(private http:HttpClient) { }

  getAllProducts(shopParams:ShopParams) : Observable<IPagination>
  {
    let params = new HttpParams();

    if(shopParams.brandId !== 0){
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if(shopParams.typeId !== 0){
      params = params.append('typeId', shopParams.typeId?.toString());
    }

    if(shopParams.search !== ''){
      params = params.append('search', shopParams.search);
    }

      params = params.append('sort', shopParams.sort);
      params = params.append('pageIndex', shopParams.pageNumber.toString());
      params = params.append('pageSize', shopParams.pageSize.toString());
       
    return this.http.get<IPagination>(`${this.ApiUrl}/Products`, {observe : 'response', params})
    .pipe(
      map(response => {
        return response.body as IPagination;
      })
    )
  }

  getAllBrands() : Observable<IBrand[]> 
  {
    return this.http.get<IBrand[]>(`${this.ApiUrl}/Products/brands`);
  }
  
  getAllTypes() : Observable<IType[]> 
  {
    return this.http.get<IType[]>(`${this.ApiUrl}/Products/types`);
  }
}
