import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginacjaResultat } from '../_models/paginacja';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) {}

getUsers(strona?, itemsNaStrone?, userParametry?): Observable<PaginacjaResultat<User[]>> {
  const paginacjaResultat: PaginacjaResultat<User[]> = new PaginacjaResultat<User[]>();

  let params = new HttpParams();

  if (strona != null && itemsNaStrone != null) {
    params = params.append('numerStrony', strona);
    params = params.append('rozmiarStrony', itemsNaStrone);
  }

  if (userParametry != null) {
    params = params.append('minWiek', userParametry.minWiek);
    params = params.append('maxWiek', userParametry.maxWiek);
    params = params.append('plec', userParametry.plec);
    params = params.append('ostatnioByl', userParametry.ostatnioByl);
  }

  return this.http.get<User[]>(this.baseUrl + 'users', { observe: 'response', params})
    .pipe(
      map(response => {
        paginacjaResultat.resultat = response.body;
        if (response.headers.get('Naglowki') != null) {
          paginacjaResultat.paginacja = JSON.parse(response.headers.get('Naglowki'));
        }
        return paginacjaResultat;
      })
    );
}

getUser(id): Observable<User> {
  return this.http.get<User>(this.baseUrl + 'users/' + id);
}

updateUser(id: number, user: User) {
  return this.http.put(this.baseUrl + 'users/' + id, user);
}

ustawGlowneZdjecie(userId: number, id: number) {
  return this.http.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/ustawGlowne', {});
}

usunZdjecie(userId: number, id: number) {
  return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id);
}

}
