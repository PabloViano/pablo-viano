import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ReservaService {

  private readonly url = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) {}

  getDetalleReserva(idReserva: string | null): Observable<any>{
    return this.http.get<any>(`${this.url}/reservas/${idReserva}`);
  }
}
