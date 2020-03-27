import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Wiadomosci } from '../_models/wiadomosci';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class WiadomosciResolver implements Resolve<Wiadomosci[]> {
    numerStrony = 1;
    rozmiarStrony = 6;
    naglowekWiadomosci = 'Nieprzeczytane';

    constructor(private userService: UserService, private authService: AuthService,
                private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Wiadomosci[]> {
        return this.userService.getWiadomosci(this.authService.decodedToken.nameid,
                this.numerStrony, this.rozmiarStrony, this.naglowekWiadomosci).pipe(
            catchError(error => {
                this.alertify.error('Problem z odbiorem wiadomosci');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
