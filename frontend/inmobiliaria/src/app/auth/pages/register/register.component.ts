import { Component, effect, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { UserRegister } from '../../interface';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  private authService = inject(AuthService);

  roles: string[] = ['administrador', 'vendedor', 'cliente', 'programador'];

  registerForm!: FormGroup;
  usuarioCreado!: UserRegister;

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      role: ['', Validators.required],
    });
  }

  register() {

    const newUser = this.registerForm.value as UserRegister
    console.log(newUser)

    this.authService.register(newUser).subscribe({
      next: (userCreado) => {
        this.usuarioCreado = userCreado;
        this.registerForm.reset();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
