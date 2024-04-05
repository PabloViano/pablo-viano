import { Component, OnInit, inject } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLogin } from '../../interface/user-login';
import { AuthService } from '../../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private fb = inject(FormBuilder)
  private authService = inject(AuthService);
  private router = inject(Router);

  loginForm !: FormGroup;

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    })
  }
  
  login(){
    const newUsuario = this.loginForm.value as UserLogin

    this.authService.login(newUsuario)
    .subscribe({
      next: res => {
        console.log('next', res);
        this.router.navigateByUrl('/')
      },
      error: err => {
        console.log('error: ', err)
      }
    })
  }

}
