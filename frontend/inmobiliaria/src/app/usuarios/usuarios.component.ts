import { Component, OnInit, inject } from '@angular/core';
import { UsuariosService } from './usuarios.service';
import { Usuarios } from './interface/usuarios';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  
  private usuariosService = inject(UsuariosService);
  usuarios: Usuarios[] = [];

  constructor() { }

  ngOnInit(): void {
    this.usuariosService.getUsuarios().subscribe({
      next: (usuarios) => {
        this.usuarios = usuarios;
        console.log(this.usuarios);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

}
