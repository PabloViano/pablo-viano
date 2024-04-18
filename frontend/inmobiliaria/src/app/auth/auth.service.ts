import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import * as jwt from 'jwt-decode';
import { environment } from '../../environments/environment';
import { User, UserLogin, UserRegister } from './interface';
import { AuthStatus } from './interface/auth-status.enum';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly url = environment.apiUrl;
  private _authStatus = new BehaviorSubject<AuthStatus>(AuthStatus.checking);
  private _currentUser = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient, private router: Router) {}

  get authStatus(): Observable<AuthStatus> {
    return this._authStatus.asObservable();
  }

  get currentUser(): Observable<User | undefined> {
    return this._currentUser.asObservable();
  }

  register(newUser: UserRegister): Observable<any> {
    return this.http.post<any>(`${this.url}/account/register`, newUser);
  }

  login(userLogin: UserLogin): Observable<any> {
    return this.http.post<any>(`${this.url}/account/login`, userLogin).pipe(
      map(({ accessToken }) => {
        this.setAuthentication(accessToken);
        return accessToken;
      })
    );
  }

  setAuthentication(token: string | null): void {
    if (token) {
      localStorage.setItem('accessToken', token);
      const userResponse = jwt.jwtDecode(token) as User;
      this._authStatus.next(AuthStatus.authenticated);
      this._currentUser.next({
        name: userResponse.name,
        role: userResponse.role,
        exp: userResponse.exp
      });
    }
  }

  checkStatus(): void {
    const token = localStorage.getItem('accessToken');
    this.setAuthentication(token);
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    this._authStatus.next(AuthStatus.noAuthenticated);
    this.router.navigateByUrl('/auth/login');
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }
}
