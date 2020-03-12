import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    tytul: string;
    wiek: number;
    plec: string;
    utworzony: Date;
    ostatnioAktywny: Date;
    zdjecieUrl: string;
    miasto: string;
    kraj: string;
    zainteresowania?: string;
    wprowadzony?: string;
    kogoSzukasz?: string;
    zdjecia?: Photo[];
}
