import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { NewProducto } from './interface/newProducto';
import { Producto } from './interface/producto';


@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private readonly url = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getProductos(): Observable<any> {
    // const headers = new HttpHeaders({
    //   'Authorization': `Bearer ${this.authService.getToken()}`
    // });

    return this.http.get<any>(`${this.url}/productos`);
  }

  createProducto(newProducto: NewProducto): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });

    const productoConEstadoDefault: NewProducto = {
      ...newProducto,
      estado: 'Disponible'
    };

    return this.http.post<any>(`${this.url}/productos`, productoConEstadoDefault, { headers });
  }

  deleteProducto(codigoAlfanumero: string): Observable<any>{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });

    return this.http.delete<any>(`${this.url}/productos/${codigoAlfanumero}`, { headers });
  }

  obtenerProductoPorCodigo(codigoAlfanumero: string): Observable<Producto> {

    return this.http.get<Producto>(`${this.url}/productos/${codigoAlfanumero}`);
  }

  modificarProducto(codigoAlfanumero: string, productoEdited: Producto): Observable<any>{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });
    
    return this.http.put<any>(`${this.url}/productos/${codigoAlfanumero}`, productoEdited, { headers });
  }
}  