import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { SalesService, CreateSaleRequest, Sale } from '../../services/sales.service';

@Component({
  selector: 'app-sales',
  standalone: true,
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.scss'],
  imports: [
    CommonModule,
    FormsModule,
    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatPaginator
  ]
})
export class SalesComponent implements OnInit {
  sales: Sale[] = [];
  dataSource = new MatTableDataSource<Sale>(this.sales);
  displayedColumns: string[] = ['id', 'saleNumber', 'saleDate', 'branchName', 'customerName', 'totalDiscount',  'isCancelled', 'actions'];
  newSale: CreateSaleRequest = {
    saleNumber: '',
    saleDate: new Date().toISOString(),
    branchId: '',
    branchName: '',
    customerId: '',
    customerName: '',
    items: []
  };

  

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private salesService: SalesService) { }

  ngOnInit(): void {
    this.loadSales();
  }

  loadSales(): void {
    this.salesService.getSales().subscribe(
      res => {
        this.sales = res;
        this.dataSource.data = res;
        // Configure o paginator após os dados serem carregados
        this.dataSource.paginator = this.paginator;
      },
      err => console.error('Erro ao carregar vendas', err)
    );
  }

  addSale(): void {
    this.salesService.createSale(this.newSale).subscribe(
      res => {
        alert(`Venda criada com ID: ${res.saleId}`);
        this.loadSales();
        this.resetNewSale();
      },
      err => console.error('Erro ao criar venda', err)
    );
  }

  getTotalDiscount(sale: Sale): number {
    return sale.items.reduce((sum, item) => {
      return sum + (item.unitPrice * item.quantity * item.discountPercentage);
    }, 0);
  }


  cancelSale(id: string): void {
    this.salesService.cancelSale(id).subscribe(
      res => {
        alert('Venda cancelada');
        this.loadSales();
      },
      err => console.error('Erro ao cancelar venda', err)
    );
  }

  addItem(): void {
    this.newSale.items.push({
      productId: '',
      productName: '',
      unitPrice: 0,
      quantity: 1
    });
  }

  removeItem(index: number): void {
    this.newSale.items.splice(index, 1);
  }

  resetNewSale(): void {
    this.newSale = {
      saleNumber: '',
      saleDate: new Date().toISOString(),
      branchId: '',
      branchName: '',
      customerId: '',
      customerName: '',
      items: []
    };
  }
}
