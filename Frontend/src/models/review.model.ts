import { Client } from './client.model';

export class Review {
  constructor(
    public codigo: Number,
    public data: String,
    public dataProp: Date,
    public titulo: String,
    public opiniaoText: String,
    public avaliacao: String,
    public infoPositivo: String,
    public infoNegativo: String,
    public cliente: Client
  ) {}
}
