import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { SalesService, CreateSaleRequest, Sale, SaleItemRequest } from '../../services/sales.service';

// Opcional: Defina uma interface para atualização que inclua o saleId
export interface UpdateSaleRequest extends CreateSaleRequest {
  saleId: string;
}

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
    MatPaginatorModule
  ]
})
export class SalesComponent implements OnInit {
  sales: Sale[] = [];
  dataSource = new MatTableDataSource<Sale>(this.sales);
  displayedColumns: string[] = [
    'id', 'saleNumber', 'saleDate', 'branchName', 'customerName',
    'totalQuantity', 'saleTotal', 'totalDiscount', 'isCancelled', 'actions'
  ];

  // Dados para criar nova venda
  newSale: CreateSaleRequest = {
    saleNumber: '',
    saleDate: new Date().toISOString(),
    branchId: '',
    branchName: '',
    customerId: '',
    customerName: '',
    items: []
  };

  // Dados para pesquisa e atualização
  searchSaleId: string = '';
  saleToUpdate?: Sale;
  // Inicialmente, updateSaleData não tem saleId; será preenchido na pesquisa
  updateSaleData: UpdateSaleRequest = {
    saleId: '',
    saleNumber: '',
    saleDate: new Date().toISOString(),
    branchId: '',
    branchName: '',
    customerId: '',
    customerName: '',
    items: []
  };

  // Array para simular eventos
  saleEvents: string[] = [];

  pageNumber: number = 1;
  pageSize: number = 10;
  totalSales: number = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private salesService: SalesService) {}

  ngOnInit(): void {
    this.loadSales();
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  loadSales(): void {
    this.salesService.getSales(this.pageNumber, this.pageSize).subscribe(
      res => {
        this.sales = res.items;        
        this.totalSales = res.totalCount; 
        this.dataSource.data = res.items; 
      },
      err => console.error('Erro ao carregar vendas', err)
    );
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadSales();
  }

  addSale(): void {
    if (this.newSale.items.some(item => item.quantity > 20)) {
      alert('Não é permitido vender mais de 20 itens iguais em um mesmo item.');
      return;
    }
    this.salesService.createSale(this.newSale).subscribe(
      res => {
        alert(`Venda criada com ID: ${res.saleId}`);
        this.saleEvents.push(`[Event] SaleCreated - ID: ${res.saleId} - ${new Date().toLocaleString()}`);
        this.loadSales();
        this.resetNewSale();
      },
      err => console.error('Erro ao criar venda', err)
    );
  }

  // Pesquisa uma venda pelo ID e preenche o formulário de atualização, incluindo os itens
  searchSale(): void {
    if (!this.searchSaleId) {
      alert('Informe o ID da venda para pesquisar.');
      return;
    }
    this.salesService.getSaleById(this.searchSaleId).subscribe(
      res => {
        this.saleToUpdate = res;
        // Preenche updateSaleData com os dados da venda encontrada, incluindo o saleId
        this.updateSaleData = {
          saleId: res.id,
          saleNumber: res.saleNumber,
          saleDate: res.saleDate,
          branchId: res.branchId,
          branchName: res.branchName,
          customerId: res.customerId,
          customerName: res.customerName,
          items: res.items.map(item => ({
            productId: item.productId,
            productName: item.productName,
            unitPrice: item.unitPrice,
            quantity: item.quantity
          }))
        };
      },
      err => {
        console.error('Erro ao buscar venda', err);
        alert('Venda não encontrada.');
      }
    );
  }

  // Atualiza a venda usando o saleId na URL e o objeto updateSaleData
  updateSale(): void {
    if (!this.saleToUpdate) {
      alert('Nenhuma venda selecionada para atualização.');
      return;
    }
    // Envia o ID na URL e o updateSaleData (que inclui saleId) no body
    this.salesService.updateSale(this.saleToUpdate.id, this.updateSaleData).subscribe(
      res => {
        alert('Venda atualizada com sucesso.');
        this.saleEvents.push(`[Event] SaleUpdated - ID: ${this.saleToUpdate?.id} - ${new Date().toLocaleString()}`);
        this.saleToUpdate = undefined;
        this.updateSaleData = {
          saleId: '',
          saleNumber: '',
          saleDate: new Date().toISOString(),
          branchId: '',
          branchName: '',
          customerId: '',
          customerName: '',
          items: []
        };
        this.loadSales();
      },
      err => console.error('Erro ao atualizar venda', err)
    );
  }

  cancelSale(id: string): void {
    this.salesService.cancelSale(id).subscribe(
      res => {
        alert('Venda cancelada');
        this.saleEvents.push(`[Event] SaleCancelled - ID: ${id} - ${new Date().toLocaleString()}`);
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

  addItemToUpdate(): void {
    this.updateSaleData.items.push({
      productId: '',
      productName: '',
      unitPrice: 0,
      quantity: 1
    });
  }

  removeItemFromUpdate(index: number): void {
    this.updateSaleData.items.splice(index, 1);
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

  // Métodos para regras de negócio de desconto

  getItemDiscount(item: SaleItemRequest): number {
    if (item.quantity > 20) return -1;
    if (item.quantity >= 10) return 0.20;
    if (item.quantity >= 4) return 0.10;
    return 0;
  }

  getItemTotal(item: SaleItemRequest): number {
    const discount = this.getItemDiscount(item);
    if (discount === -1) return 0;
    const subtotal = item.unitPrice * item.quantity;
    return subtotal - (subtotal * discount);
  }

  getItemDiscountValue(item: SaleItemRequest): number {
    const discount = this.getItemDiscount(item);
    if (discount === -1) return 0;
    return item.unitPrice * item.quantity * discount;
  }

  getTotalQuantity(sale: Sale): number {
    return sale.items.reduce((sum, item) => sum + item.quantity, 0);
  }

  getSaleTotal(sale: Sale): number {
    return sale.items.reduce((sum, item) => sum + this.getItemTotal(item), 0);
  }
  isFormInvalid(): boolean {
    return this.newSale.items.some(i => this.getItemDiscount(i) === -1);
  }

  getTotalDiscount(sale: Sale): number {
    return sale.items.reduce((sum, item) => {
      const discount = this.getItemDiscount(item);
      return sum + (item.unitPrice * item.quantity * (discount >= 0 ? discount : 0));
    }, 0);
  }
}
