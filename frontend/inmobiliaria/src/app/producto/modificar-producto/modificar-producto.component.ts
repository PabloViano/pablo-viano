import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductoService } from '../producto.service';
import { Producto } from '../interface/producto';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-modificar-producto',
  templateUrl: './modificar-producto.component.html',
  styleUrls: ['./modificar-producto.component.css']
})
export class ModificarProductoComponent implements OnInit {
  producto: Producto | undefined;
  formProducto: FormGroup | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productoService: ProductoService
  ) { }
  

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const codigoAlfanumero = params.get('codigoAlfanumero');
      if (codigoAlfanumero !== null) {
        this.productoService.obtenerProductoPorCodigo(codigoAlfanumero).subscribe(
          (producto: Producto) => {
            this.producto = producto;
            this.initForm();
          },
          error => {
            console.error('Error al obtener el producto:', error);
          }
        );
      } else {
        console.error('El parámetro codigoAlfanumero es nulo.');
      }
    });
  }

  initForm() {
    if (this.producto) {
      this.formProducto = new FormGroup({
        barrio: new FormControl(this.producto.barrio),
        descripcion: new FormControl(this.producto.descripcion),
        price: new FormControl(this.producto.price),
        urlImagen: new FormControl(this.producto.urlImagen)
      });
    } else {
      console.error('El producto es undefined.');
    }
  }

  modificarProducto() {
    if (!this.producto || !this.formProducto) {
      console.error('El producto o el formulario son undefined.');
      return;
    }
    if (this.formProducto) {
      const productoEdited: Producto = {
        ...this.producto,
        barrio: this.formProducto.value.barrio,
        descripcion: this.formProducto.value.descripcion,
        price: this.formProducto.value.price,
        urlImagen: this.formProducto.value.urlImagen
      };

      this.productoService.modificarProducto(this.producto.codigoAlfanumero, productoEdited).subscribe(
        () => {
          console.log('Producto modificado exitosamente');
          if (this.producto) {
            this.router.navigate(['/detalle-producto', this.producto.codigoAlfanumero]);
          }
        },
        error => {
          console.error('Error al modificar el producto:', error);
        }
      );
    }
  }
}


