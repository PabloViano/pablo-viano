import { Component, OnInit, inject } from '@angular/core';
import { Producto } from './interface/producto';
import { ProductoService } from './producto.service';
import { MatTabChangeEvent } from '@angular/material/tabs';

@Component({
  selector: 'app-producto',
  templateUrl: './producto.component.html',
  styleUrls: ['./producto.component.css']
})
export class ProductosComponent implements OnInit {
  private productoService = inject(ProductoService);

  displayedColumns: string[] = ['urlImagen','barrio', 'descripcion','estado', 'price', 'eliminar','modificar'];
  productos: any[] = [];

  ngOnInit(): void {
    this.productoService.getProductos().subscribe({
      next: (productos) => {
        this.productos = productos;
        console.log(this.productos);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

}
