import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private http = inject(HttpClient);
  private readonly url = environment.apiUrl;
  private authService = inject(AuthService);
// constructor() { }
  getProductos():Observable<any>{
    // const headers = new HttpHeaders({
    //   'Authorization': `Bearer ${this.authService.getToken()}`
    // });

    return this.http.get<any>(`${this.url}/productos`);
  }
}
