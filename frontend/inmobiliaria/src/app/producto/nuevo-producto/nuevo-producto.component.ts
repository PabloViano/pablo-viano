import { Component, OnInit, inject } from '@angular/core';
import { NewProducto} from '../interface/newProducto';
import { ProductoService } from '../producto.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-nuevo-producto',
  templateUrl: './nuevo-producto.component.html',
  styleUrls: ['./nuevo-producto.component.css']
})
export class NuevoProductoComponent implements OnInit {

  newProductoForm!: FormGroup;
  productoCreado!: NewProducto;

  constructor(private fb: FormBuilder, private productoService: ProductoService) {}

  ngOnInit(): void {
    this.newProductoForm = this.fb.group({
      barrio: ['', Validators.required],
      descripcion: ['', Validators.required],
      price: ['', Validators.required],
      urlImagen: ['', Validators.required],
    });
  }

  createProducto() {
    const newProducto = this.newProductoForm.value as NewProducto;

    this.productoService.createProducto(newProducto).subscribe({
      next: (productoCreado) => {
        this.productoCreado = productoCreado;
        this.newProductoForm.reset();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
