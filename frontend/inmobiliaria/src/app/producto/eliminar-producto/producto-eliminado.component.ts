import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductoService } from '../producto.service';

@Component({
  selector: 'app-producto-eliminado',
  templateUrl: './producto-eliminado.component.html',
  styleUrls: ['./producto-eliminado.component.css']
})
export class ProductoEliminadoComponent implements OnInit {
  codigoAlfanumero: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productoService: ProductoService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.codigoAlfanumero = params['codigoAlfanumero'];
      this.eliminarProducto(this.codigoAlfanumero);
    });
  }

  eliminarProducto(codigoAlfanumero: string) {
    this.productoService.deleteProducto(codigoAlfanumero).subscribe({
      next: (productoEliminado) => {
        console.log('Producto eliminado:', productoEliminado);
        this.router.navigate(['/productos']);
      },
      error: (err) => {
        console.log('Error al eliminar producto:', err);
      }
    });
  }
}


