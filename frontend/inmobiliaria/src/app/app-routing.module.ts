import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductosComponent } from './producto/producto.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { HomeComponent} from './home/home.component'
import { ReservasComponent } from './reservas/reservas.component';
import { NuevoProductoComponent } from './producto/nuevo-producto/nuevo-producto.component';
import { ProductoEliminadoComponent } from './producto/eliminar-producto/producto-eliminado.component';
import { ModificarProductoComponent } from './producto/modificar-producto/modificar-producto.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'productos',
    component: ProductosComponent
  },
  {
    path: 'productos/nuevo',
    component: NuevoProductoComponent
  },
  {
    path: 'usuarios',
    component: UsuariosComponent
  },
  { path: 'reserva/:idReserva', component: ReservasComponent },
  {
    path: 'productos/:codigoAlfanumero',
    component: ProductoEliminadoComponent
  },
  {
    path: 'productos/modificar/:codigoAlfanumero',
    component: ModificarProductoComponent
  
  },
  {
    path: '**',
    redirectTo: 'home'
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
