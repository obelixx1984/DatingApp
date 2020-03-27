export interface Wiadomosci {
    id: number;
    wyslalId: number;
    wyslalTytul: string;
    wyslalZdjecieUrl: string;
    odbiorcaId: number;
    odbiorcaTytul: string;
    odbiorcaZdjecieUrl: string;
    tresc: string;
    jestCzytana: boolean;
    dataCzytania: Date;
    dataWyslania: Date;
}
