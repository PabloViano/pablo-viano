import { BrowserModule } from '@angular/platform-browser';
import { NgModule,NO_ERRORS_SCHEMA,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductosComponent } from './producto/producto.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ProductoDetalleComponent } from './producto/producto-detalle/producto-detalle.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { HomeComponent } from './home/home.component';
import { AuthModule } from './auth/auth.module';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './shared/material/material.module';
import { InformacionComponent } from './standalone/informacion/informacion.component'
import { ReservasComponent } from './reservas/reservas.component';

@NgModule({
  declarations: [						
      AppComponent,
      ProductosComponent,
      ProductoDetalleComponent,
      UsuariosComponent,
      HomeComponent,
      ReservasComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule,
    HttpClientModule,
    MaterialModule,
    InformacionComponent,
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA,
    NO_ERRORS_SCHEMA
  ]
})
export class AppModule { }
