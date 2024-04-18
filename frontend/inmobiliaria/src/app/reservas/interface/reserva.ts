export interface Reserva {
    id: number;
    estado: number;
    idClienteAsociado: string;
    productoReservado: string;
    fechaDesde: string;
    fechaHasta: string;
}
