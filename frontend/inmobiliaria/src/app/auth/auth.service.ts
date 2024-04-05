import { HttpClient } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environment';
import { User, UserLogin, UserRegister } from './interface';
import * as jwt from 'jwt-decode';
import { AuthStatus } from './interface/auth-status.enum';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);
  private router = inject(Router);
  private readonly url = environment.apiUrl

  private _authStatus = signal<AuthStatus>(AuthStatus.checking)
  private _currentUser = signal<User | undefined>(undefined)

  public authStatus = computed(() => this._authStatus())
  public currentUser = computed(() => this._currentUser())



  // constructor(private http: HttpClient) { }

  register(newUser: UserRegister): Observable<any> {
    return this.http.post<any>(`${this.url}/account/register`, newUser)
  }

  login(userLogin: UserLogin): Observable<any> {
    return this.http.post<any>(`${this.url}/account/login`, userLogin)
      .pipe(
        map(({ accessToken }) => {
          this.setAuthentication(accessToken);
          return accessToken;
        })
      )
  }


  setAuthentication(token: string | null) {

    if (token) {

      localStorage.setItem('accessToken', token)

      const userResponse = jwt.jwtDecode(token) as User

      console.log('userResponse: ', userResponse);

      this._authStatus.set(AuthStatus.authenticated)

      this._currentUser.set({
        name: userResponse.name,
        role: userResponse.role,
        exp: userResponse.exp
      })
    }

    console.log('señal computada: ', this.currentUser());

  }


  checkStatus() {
    const token = localStorage.getItem('accessToken');
    console.log('checkStatus', token);

    this.setAuthentication(token)
  }

  logout(){
    localStorage.removeItem('accessToken');
    this.router.navigateByUrl('/auth/login')
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }

}
