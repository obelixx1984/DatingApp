export interface Paginacja {
    domyslnaStrona: number;
    itemsNaStrone: number;
    caloscItems: number;
    caloscStron: number;
}

export class PaginacjaResultat<T> {
    resultat: T;
    paginacja: Paginacja;
}
