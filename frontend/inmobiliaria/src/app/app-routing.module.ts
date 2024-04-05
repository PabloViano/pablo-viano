import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductosComponent } from './producto/producto.component';
import { ProductoDetalleComponent } from './producto/producto-detalle/producto-detalle.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { HomeComponent} from './home/home.component'
import { ReservasComponent } from './reservas/reservas.component';

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
    path: 'productos/:id',
    component: ProductoDetalleComponent
  },
  {
    path: 'usuarios',
    component: UsuariosComponent
  },
  {
    path: 'reserva',
    component: ReservasComponent
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
