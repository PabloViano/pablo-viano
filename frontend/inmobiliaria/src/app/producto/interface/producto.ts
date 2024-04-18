export interface Producto{
    barrio: string;
    codigoAlfanumero: string;
    descripcion: string;
    estado: number;
    price: number | undefined;
    urlImagen?: string;
    IDReserva?: string | undefined;
  }
