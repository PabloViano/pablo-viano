import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReservaService } from './reserva.service';

@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.css']
})
export class ReservasComponent implements OnInit {

  idReserva: string | null = null;
  reservaData: any;

  constructor(private route: ActivatedRoute, private reservaService: ReservaService) {}

  ngOnInit() {
    this.TomarID();
  }

  TomarID(){
    this.idReserva = this.route.snapshot.params['idReserva'];

    this.reservaService.getDetalleReserva(this.idReserva)
      .subscribe({
        next: res => {
          this.reservaData = res;
          console.log('next', res);
        },
        error: err => {
          console.log('error: ', err)
        }
      })
  }
}

