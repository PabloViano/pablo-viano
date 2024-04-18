import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth/auth.service';
import { AuthStatus } from './auth/interface/auth-status.enum';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isAuthenticated: boolean = false;
  title: string = 'Inmobiliaria';
  anio: number = 2024;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    console.log('App Component Iniciado');
    this.authService.authStatus.subscribe(status => {
      this.isAuthenticated = status === AuthStatus.authenticated;
    });
    this.authService.checkStatus();
  }

  logout(): void {
    this.authService.logout();
  }
}