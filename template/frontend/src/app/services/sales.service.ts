import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Sale {
  id: string;
  saleNumber: string;
  saleDate: string;
  branchId: string;
  branchName: string;
  customerId: string;
  customerName: string;
  isCancelled: boolean;
  items: SaleItem[];
}

export interface SaleItem {
  id: string;
  productId: string;
  productName: string;
  unitPrice: number;
  quantity: number;
  discountPercentage: number;
  total: number;
  isCancelled: boolean;
}

export interface CreateSaleRequest {
  saleNumber: string;
  saleDate: string;
  branchId: string;
  branchName: string;
  customerId: string;
  customerName: string;
  items: SaleItemRequest[];
}

export interface SaleItemRequest {
  productId: string;
  productName: string;
  unitPrice: number;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private apiUrl = 'https://localhost:7181/api/Sales';

  constructor(private http: HttpClient) { }

  getSales(): Observable<Sale[]> {
    return this.http.get<Sale[]>(this.apiUrl);
  }

  getSaleById(id: string): Observable<Sale> {
    return this.http.get<Sale>(`${this.apiUrl}/${id}`);
  }

  createSale(sale: CreateSaleRequest): Observable<{ saleId: string }> {
    return this.http.post<{ saleId: string }>(this.apiUrl, sale);
  }

  updateSale(id: string, sale: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, sale);
  }

  cancelSale(id: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/cancel`, {});
  }
}
